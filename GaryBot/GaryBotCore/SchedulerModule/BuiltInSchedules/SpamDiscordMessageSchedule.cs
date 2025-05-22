using GaryBotCore.JobModule;
using GaryBotCore.JobModule.JobInstructions;
using GaryBotCore.RecordingModule;
using GaryBotCore.SchedulerModule.DependencyTypes;

namespace GaryBotCore.SchedulerModule.BuiltInSchedules;

public class SpamDiscordMessageSchedule : SelfResolvingBotSchedule
{
    public override IEnumerator<GaryJob> JobSchedule()
    {
        yield return new GaryJob(JobSettings.Default,
            new PerformGaryRecording((Recording)GetDependencyValue("GoToDiscord")));

        for (int i = 0; i < 10; i++)
        {
            yield return new GaryJob(JobSettings.Default,
                new SendMessageJob((string)GetDependencyValue("CustomMessage")));
        }
    }

    public override IEnumerable<IScheduleDependency> ScheduleDependencies { get; } = new IScheduleDependency[]
    {
        new RecordingDependency("GoToDiscord", "Tab into discord", "gotodiscord"),
        new StringDependency("CustomMessage", "What do you want to send?"),
    };
}