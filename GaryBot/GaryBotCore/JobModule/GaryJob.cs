using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule;

public class GaryJob(GaryComputerAccess computerAccess, JobSettings settings, IGaryJobInstructions instructions)
{
    private readonly IGaryComputerAccess _computerAccess = computerAccess;

    private bool _stopping;
    private bool _running;

    private Task? _currentTask;

    public async Task<JobResult> PerformAsync()
    {
        _running = true;
        
        var instructions1 = instructions.GetJobInstructions(computerAccess);
        while (instructions1.MoveNext())
        {
            try
            {
                _currentTask = instructions1.Current;
                await _currentTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CleanUp();
                return JobResult.Error;
            }

            _currentTask = null;
        }

        CleanUp();
        return JobResult.Success;
    }

    public async Task<JobResult> Stop()
    {
        if (_stopping)
        {
            return JobResult.AlreadyStopping;
        }
        _stopping = true;
        try
        {
            var timeOutTask = Task.Delay(settings.TimeOutDuration);
            var lastTask = _currentTask ?? Task.Delay(10);
            await Task.WhenAny(timeOutTask, lastTask);
            return timeOutTask.IsCompleted ? JobResult.TimedOut : JobResult.Stopped;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return JobResult.ErrorWhileStopping;
        }
        finally
        {
            CleanUp();
        }
    }

    public bool IsRunning()
    {
        return _running;
    }

    public bool IsStopping()
    {
        return _stopping;
    }

    private void CleanUp()
    {
        _stopping = false;
        _running = false;
        _currentTask = null;
    }
}