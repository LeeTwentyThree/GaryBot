using GaryBotCore.ComputerAccessModule;
using GaryBotCore.JobModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.SchedulerModule;

public abstract class SelfResolvingBotSchedule : IBotSchedule
{
    public async Task ResolveDependencies(IGaryComputerAccess computerAccess)
    {
        bool isResolving = false;
        foreach (var dependency in ScheduleDependencies)
        {
            if (dependency.IsResolved)
                continue;
            if (!isResolving)
            {
                LoggingUtility.LogWarning("Schedule cannot execute until all dependencies are resolved!");
                isResolving = true;   
            }
            await dependency.Resolve(computerAccess);
        }
        LoggingUtility.Log("All dependencies resolved!");
    }

    public abstract IEnumerator<GaryJob> JobSchedule();

    public abstract IEnumerable<IScheduleDependency> ScheduleDependencies { get; }
    public object GetDependencyValue(string key)
    {
        foreach (var dependency in ScheduleDependencies)
        {
            if (dependency.IsResolved && dependency.Key == key)
            {
                return dependency.GetValue();
            }
        }

        throw new InvalidOperationException($"No dependency exists with the key '{key}'");
    }
}