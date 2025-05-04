using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class OpenApplicationJob(string applicationName, int timeOutMs = 30000) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.OpenApplication(applicationName, timeOutMs);
    }

    public string GetJobDescription()
    {
        return $"Opening application '{applicationName}'";
    }
}