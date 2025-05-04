using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class OpenFileJob(string filePath, string arguments, int timeOutMs = 30000) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.OpenFile(filePath, arguments, timeOutMs);
    }

    public string GetJobDescription()
    {
        return $"Opening file at '{filePath}'";
    }
}