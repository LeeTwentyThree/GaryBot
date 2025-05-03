using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class WaitDelayJob(int milliseconds) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return Task.Delay(milliseconds);
    }

    public string GetJobDescription()
    {
        return $"Waiting {milliseconds / 1000f:0.000} seconds";
    }
}