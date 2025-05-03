using GaryBotCore.JobModule;
using GaryBotCore.JobModule.JobInstructions;
using GaryBotCore.RecordingModule;
using GaryBotCore.SchedulerModule.DependencyTypes;

namespace GaryBotCore.SchedulerModule.BuiltInSchedules;

public class HitWindowsKeySchedule : SelfResolvingBotSchedule
{
    public override IEnumerator<GaryJob> JobSchedule()
    {
        yield return new GaryJob(JobSettings.Default,
            new PerformGaryRecording((Recording)GetDependencyValue("HitWindowsKey")));
    }

    public override IEnumerable<IScheduleDependency> ScheduleDependencies { get; } = new IScheduleDependency[]
    {
        new RecordingDependency("HitWindowsKey", "Hit the windows key", "windowskey"),
    };
}