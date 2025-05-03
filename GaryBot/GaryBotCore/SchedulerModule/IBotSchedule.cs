using GaryBotCore.ComputerAccessModule;
using GaryBotCore.JobModule;

namespace GaryBotCore.SchedulerModule;

public interface IBotSchedule
{
    Task ResolveDependencies(IGaryComputerAccess computerAccess);
    IEnumerator<GaryJob> JobSchedule();
    IEnumerable<IScheduleDependency> ScheduleDependencies { get; }
    object GetDependencyValue(string key);
}