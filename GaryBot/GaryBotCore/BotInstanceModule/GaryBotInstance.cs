using GaryBotCore.JobModule;

namespace GaryBotCore.BotInstanceModule;

public class GaryBotInstance(BotSettings settings) : IGaryJobHost
{
    public BotSettings Settings { get; } = settings;

    private GaryJob? _currentJob;

    public bool AcceptingNewJob => _currentJob == null || !_currentJob.IsRunning();
    
    public async Task<JobResult> RunJobAsync(GaryJob job)
    {
        if (!AcceptingNewJob)
        {
            return JobResult.FailedToStart;
        }

        try
        {
            _currentJob = job;
            return await job.PerformAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return JobResult.Error;
        }
        finally
        {
            _currentJob = null;
        }
    }
}