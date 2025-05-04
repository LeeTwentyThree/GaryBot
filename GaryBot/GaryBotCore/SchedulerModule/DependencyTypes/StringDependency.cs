using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.SchedulerModule.DependencyTypes;

public class StringDependency(string key, string instructions) : IScheduleDependency
{
    private const string FolderName = "StringValues";
    
    public string Key => key;

    private string? _value;

    private string FilePath { get; } = Path.Combine(FolderName, key + ".txt");

    public async Task Resolve(IGaryComputerAccess computerAccess)
    {
        if (!Directory.Exists(FolderName))
        {
            Directory.CreateDirectory(FolderName);
        }

        if (Path.Exists(FilePath))
        {
            try
            {
                _value = await File.ReadAllTextAsync(FilePath);
                if (BotScheduler.EnableRerecordMode)
                {
                    LoggingUtility.Log(
                        $"String value file found: '{FilePath}'\nDo you want to override (Y)? Press enter to ignore.");
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
                LoggingUtility.LogError($"Error while loading recording file '{FilePath}': {e}");
            }
        }

        while (string.IsNullOrWhiteSpace(_value))
        {
            LoggingUtility.Log("REQUIRED INFORMATION: " + instructions);
            LoggingUtility.Log("Enter text...");
            _value = Console.ReadLine();
        }

        LoggingUtility.Log("If you want to save this value for the future, enter 'Y'.");
        var line = Console.ReadLine();
        if (line != null && line.ToLower() == "y")
        {
            await File.WriteAllTextAsync(FilePath, _value);
        }
    }

    public object GetValue()
    {
        return _value ?? string.Empty;
    }

    public bool IsResolved => !string.IsNullOrWhiteSpace(_value);
}