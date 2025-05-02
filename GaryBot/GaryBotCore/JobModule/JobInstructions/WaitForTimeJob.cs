using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class WaitForTimeJob(DateTime time) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        while (DateTime.Now < time)
        {
            yield return Task.Delay(100);
        }
    }
}