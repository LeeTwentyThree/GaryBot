using GaryBotCore.JobModule;
using GaryBotCore.JobModule.JobInstructions;
using GaryBotCore.SchedulerModule.DependencyTypes;

namespace GaryBotCore.SchedulerModule.BuiltInSchedules;

// This would really annoy your Steam friends
public class SpamLaunchGameSchedule : SelfResolvingBotSchedule
{
    public override IEnumerator<GaryJob> JobSchedule()
    {
        var gamePath = (string)GetDependencyValue("LaunchGamePath");
        var gameProcessName = (string)GetDependencyValue("GameProcessName");
        var delay = int.Parse((string)GetDependencyValue("LaunchGameDelay")) * 1000;

        while (true)
        {
            yield return new GaryJob(JobSettings.Default, new OpenApplicationJob(gamePath));
            yield return new GaryJob(JobSettings.Default, new WaitDelayJob(5000));
            yield return new GaryJob(JobSettings.Default, new CloseApplicationJob(gameProcessName));
            yield return new GaryJob(JobSettings.Default, new WaitDelayJob(delay));
        }
    }

    public override IEnumerable<IScheduleDependency> ScheduleDependencies { get; } =
    [
        new StringDependency("LaunchGamePath", "Please enter the file path of the game's exe."),
        new StringDependency("GameProcessName",
            "Enter the name of the game's process (As seen in Task Manager)."),
        new StringDependency("LaunchGameDelay",
        "Enter the number of seconds between game launches.")
    ];
}