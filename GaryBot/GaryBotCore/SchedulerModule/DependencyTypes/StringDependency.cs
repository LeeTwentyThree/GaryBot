using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.SchedulerModule.DependencyTypes;

public class StringDependency(string key, string instructions) : IScheduleDependency
{
    public string Key => key;

    private string? _value;

    public Task Resolve(IGaryComputerAccess computerAccess)
    {
        LoggingUtility.Log(instructions);
        LoggingUtility.Log("Enter text...");
        _value = Console.ReadLine();
        return Task.CompletedTask;
    }

    public object GetValue()
    {
        return _value ?? string.Empty;
    }

    public bool IsResolved => !string.IsNullOrWhiteSpace(_value);
}