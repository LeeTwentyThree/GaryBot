using System.Text.Json;
using GaryBotCore.ComputerAccessModule;
using GaryBotCore.RecordingModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.SchedulerModule.DependencyTypes;

public class RecordingDependency(string key, string instructions, string fileNameWithoutExtension) : IScheduleDependency
{
    private const string FolderName = "Recordings";

    public string Key => key;

    private IRecording? _recording;

    private string FileName => Path.Combine(FolderName, fileNameWithoutExtension + ".gary");
    
    public async Task Resolve(IGaryComputerAccess computerAccess)
    {
        if (!Directory.Exists(FolderName))
        {
            Directory.CreateDirectory(FolderName);
        }
        if (Path.Exists(FileName))
        {
            try
            {
                _recording = JsonSerializer.Deserialize<Recording>(File.ReadAllText(FileName));
                if (BotScheduler.EnableRerecordMode)
                {
                    LoggingUtility.Log($"Recording file found: '{FileName}'\nDo you want to re-record (Y)? Press enter to ignore.");
                    var response = Console.ReadLine();
                    if (response != null && response.ToLower() != "y")
                    {
                        return;
                    }   
                }
                else
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
        var recorder = new ScreenMovementsRecorder();
        _recording = await recorder.RecordScreenMovementsAsync();
        
        LoggingUtility.Log("Done recording! If you want to save this for the future, enter 'Y'.");
        var line = Console.ReadLine();
        if (line != null && line.ToLower() == "y")
        {
            await File.WriteAllTextAsync(FileName, JsonSerializer.Serialize(_recording as Recording));
            LoggingUtility.Log($"Saved recording to {FileName}!");
        }
    }

    public object GetValue()
    {
        return _recording;
    }

    public bool IsResolved => _recording != null;
}