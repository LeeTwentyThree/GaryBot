using GaryBotCore.JobModule;
using GaryBotCore.JobModule.JobInstructions;
using GaryBotCore.SchedulerModule.DependencyTypes;

namespace GaryBotCore.SchedulerModule.BuiltInSchedules;

public class LaunchSubnauticaSchedule : SelfResolvingBotSchedule
{
    public override IEnumerator<GaryJob> JobSchedule()
    {
        yield return new GaryJob(new JobSettings(-1), new WaitForTimeJob(DateTime.Now + TimeSpan.FromSeconds(1)));
        yield return new GaryJob(new JobSettings(-1), new OpenApplicationJob((string)GetDependencyValue("SubnauticaPath")));
        yield return new GaryJob(new JobSettings(-1), new WaitDelayJob(5000));
        yield return new GaryJob(new JobSettings(-1), new CloseApplicationJob("Subnautica"));
    }

    public override IEnumerable<IScheduleDependency> ScheduleDependencies { get; } = new IScheduleDependency[]
    {
        new StringDependency("SubnauticaPath", "Please insert the file path of Subnautica.exe.")
    };
}