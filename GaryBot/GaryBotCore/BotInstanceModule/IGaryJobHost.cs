using GaryBotCore.JobModule;

namespace GaryBotCore.BotInstanceModule;

public interface IGaryJobHost
{
    Task<JobResult> RunJobAsync(GaryJob job);
}