using GaryBotCore.JobModule;
using GaryBotCore.JobModule.JobInstructions;

namespace GaryBotCore.SchedulerModule;

public static class BuiltInSchedules
{
    public static IEnumerator<GaryJob> SpamSubnauticaSchedule()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new GaryJob(new JobSettings(-1), new OpenApplicationJob("Subnautica"));
            yield return new GaryJob(new JobSettings(-1), new WaitDelayJob(5000));
            yield return new GaryJob(new JobSettings(-1), new CloseApplicationJob());
        }
    }
}