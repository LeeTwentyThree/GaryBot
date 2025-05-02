using GaryBotCore.BotInstanceModule;
using GaryBotCore.JobModule;

namespace GaryBotCore.SchedulerModule;

public class BotScheduler(GaryBotInstance bot, Func<IEnumerator<GaryJob>> jobFlow)
{
    private bool _running;
    private bool _stopping;

    private GaryJob? _activeJob;
    
    public async Task RunAsync()
    {
        _running = true;
        var flow = jobFlow.Invoke();
        while (!_stopping && flow.MoveNext())
        {
            _activeJob = flow.Current;
            await bot.RunJobAsync(_activeJob);
        }
        Reset();
    }

    public async Task StopAsync()
    {
        _stopping = true;
        if (_activeJob != null)
        {
            await _activeJob.Stop();
        }
        while (_running)
        {
            await Task.Delay(1);
        }
    }

    private void Reset()
    {
        _stopping = false;
        _running = false;
        _activeJob = null;
    }
}