using GaryBotCore.BotInstanceModule;
using GaryBotCore.ComputerAccessModule;
using GaryBotCore.JobModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.SchedulerModule;

public class BotScheduler(GaryBotInstance bot, IBotSchedule schedule, IGaryComputerAccess computerAccess)
{
    private bool _running;
    private bool _stopping;

    private GaryJob? _activeJob;
    
    public static bool EnableRerecordMode { get; private set; }
    
    public async Task RunAsync()
    {
        _running = true;
        Console.WriteLine("Enter 'Y' if you want to enter re-record mode. Otherwise, press Enter to continue.");
        var result = Console.ReadLine();
        EnableRerecordMode = result != null && result.Equals("y", StringComparison.CurrentCultureIgnoreCase);
        await schedule.ResolveDependencies(computerAccess);
        LoggingUtility.Log("Press ENTER to begin the schedule!");
        Console.ReadLine();
        var flow = schedule.JobSchedule();
        while (!_stopping && flow.MoveNext())
        {
            _activeJob = flow.Current;
            LoggingUtility.Log($"RUNNING JOB: {_activeJob}");
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