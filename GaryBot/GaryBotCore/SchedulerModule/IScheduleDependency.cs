using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.SchedulerModule;

public interface IScheduleDependency
{
    string Key { get; }
    Task Resolve(IGaryComputerAccess computerAccess);
    object GetValue();
    bool IsResolved { get; }
}