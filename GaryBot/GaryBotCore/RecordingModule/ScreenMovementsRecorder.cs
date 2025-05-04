using System.Diagnostics;
using GaryBotCore.ComputerAccessModule.Controllers;
using GaryBotCore.RecordingModule.HumanMotions;
using GaryBotCore.RecordingModule.HumanMotions.MoveCursor;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.RecordingModule;

public class ScreenMovementsRecorder : IScreenMovementsRecorder
{
    private bool _doneRecording;
    private Stopwatch? _recordingStopwatch;

    private int _recordDuration;
    
    private List<HumanMotionDtoBase> _recordingInstructions = new();

    public async Task<IRecording> RecordScreenMovementsAsync()
    {
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
        
        // EXECUTE MAIN RECORD LOGIC
        await Record();
        
        LoggingUtility.Log("DONE");
        for (var i = 0; i < 3; i++)
        {
            await Task.Delay(150);
            Console.Beep(300, 120);
        }

        return new Recording(_recordingInstructions.ToArray());
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
}