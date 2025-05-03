using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class OpenApplicationJob(string applicationName, int timeOutMs = 30000) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.OpenApplication(applicationName, timeOutMs);
        yield break;
        yield return computerAccess.PressVirtualKey(VirtualKeyShort.LWIN);  // Windows key
        yield return Task.Delay(3000);
        yield return computerAccess.PasteText(applicationName);
        yield return Task.Delay(1000);
        yield return computerAccess.PressVirtualKey(VirtualKeyShort.RETURN);
    }

    public string GetJobDescription()
    {
        return $"Opening application '{applicationName}'";
    }
}