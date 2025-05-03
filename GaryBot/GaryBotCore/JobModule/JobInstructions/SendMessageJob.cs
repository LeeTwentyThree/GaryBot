using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class SendMessageJob(string message) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.PasteText(message);
        yield return computerAccess.TypeKey(ScanCodeShort.RETURN);
        yield return Task.Delay(1000);
    }

    public string GetJobDescription()
    {
        return $"Sending message {message}'";
    }
}