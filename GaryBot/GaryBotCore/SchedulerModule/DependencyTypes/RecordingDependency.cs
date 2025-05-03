using System.Diagnostics;
using System.Text.Json;
using GaryBotCore.ComputerAccessModule;
using GaryBotCore.ComputerAccessModule.Controllers;
using GaryBotCore.RecordingModule;
using GaryBotCore.RecordingModule.Instructions;
using GaryBotCore.RecordingModule.Instructions.MoveCursor;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.SchedulerModule.DependencyTypes;

public class RecordingDependency(string key, string instructions, string fileNameWithoutExtension) : IScheduleDependency
{
    public string Key => key;

    private IRecording? _recording;

    private string FileName => fileNameWithoutExtension + ".gary";

    private List<InstructionDtoBase> _recordingInstructions = new();

    private bool _doneRecording;
    private Stopwatch _recordingStopwatch;

    private int _recordDuration;

    public async Task Resolve(IGaryComputerAccess computerAccess)
    {
        if (Path.Exists(FileName))
        {
            try
            {
                _recording = JsonSerializer.Deserialize<Recording>(File.ReadAllText(FileName));
                LoggingUtility.Log($"Recording file found: '{FileName}'\nDo you want to re-record (Y)?");
                var response = Console.ReadLine();
                if (response != null && response.ToLower() != "y")
                {
                    return;
                }
            }
            catch (Exception e)
            {
                LoggingUtility.LogError($"Error while loading recording file '{FileName}': {e}");
            }
        }

        LoggingUtility.Log("RECORDING INSTRUCTIONS: " + instructions);
        LoggingUtility.Log(
            "A .gary file needs to be recorded. How many seconds do you need?");
        while (true)
        {
            try
            {
                _recordDuration = int.Parse(Console.ReadLine()) * 1000;
                break;
            }
            catch
            {
                LoggingUtility.LogError("Invalid format! Try again. Insert the record durations in seconds.");
            }
        }

        LoggingUtility.Log("When you are ready, press enter. Recording will begin on the 4th beep.");
        Console.ReadLine();
        for (var i = 0; i < 3; i++)
        {
            await Task.Delay(600);
            Console.Beep(400, 500);
        }

        await Task.Delay(600);
        Console.Beep(1000, 200);
        LoggingUtility.Log("Record now!");
        await Record();
        LoggingUtility.Log("DONE");
        for (var i = 0; i < 3; i++)
        {
            await Task.Delay(150);
            Console.Beep(300, 120);
        }

        _recording = new Recording(_recordingInstructions.ToArray());
        LoggingUtility.Log("Done recording! If you want to save this for the future, enter 'Y'.");
        var line = Console.ReadLine();
        if (line != null && line.ToLower() == "y")
        {
            await File.WriteAllTextAsync(FileName, JsonSerializer.Serialize<Recording>(_recording as Recording));
            LoggingUtility.Log($"Saved recording to {FileName}!");
        }
    }

    private async Task Record()
    {
        _recordingStopwatch = new Stopwatch();
        _recordingStopwatch.Start();
        var cursorThread = new Thread(WatchCursor);
        cursorThread.Start();
        var mouseButtonThread = new Thread(WatchMouseButtons);
        mouseButtonThread.Start();

        await Task.Delay(_recordDuration);
        _doneRecording = true;
    }

    private void WatchCursor()
    {
        var cursorPoint = Cursor.Position;
        while (!_doneRecording)
        {
            if (cursorPoint.X == Cursor.Position.X && cursorPoint.Y == Cursor.Position.Y)
                continue;
            cursorPoint = Cursor.Position;
            _recordingInstructions.Add(new MoveCursorDto(cursorPoint.X, cursorPoint.Y)
                { Timeline = _recordingStopwatch.ElapsedMilliseconds });
            Thread.Sleep(30);
        }
    }

    private void WatchMouseButtons()
    {
        var mouseDown = new Dictionary<MouseButtons, bool>();
        var mouseButtonValues = Enum.GetValues<MouseButtons>();
        foreach (var button in mouseButtonValues)
        {
            mouseDown.Add(button, false);
        }

        while (!_doneRecording)
        {
            foreach (var button in mouseButtonValues)
            {
                var pressed = (Control.MouseButtons & button) != 0;
                if (pressed != mouseDown[button])
                {
                    mouseDown[button] = pressed;
                    var converted = GaryUtils.ConvertMouseButton(button, pressed);
                    if (converted == MouseButton.None) continue;
                    _recordingInstructions.Add(new MouseButtonDto()
                        { Timeline = _recordingStopwatch.ElapsedMilliseconds, Button = converted });
                }
            }

            Thread.Sleep(1);
        }
    }

    public object GetValue()
    {
        return _recording;
    }

    public bool IsResolved => _recording != null;
}