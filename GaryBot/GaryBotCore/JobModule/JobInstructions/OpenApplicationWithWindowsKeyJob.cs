using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class OpenApplicationWithWindowsKeyJob(string applicationName) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.PressVirtualKey(VirtualKeyShort.LWIN);  // Windows key
        yield return Task.Delay(3000);
        yield return computerAccess.PasteText(applicationName);
        yield return Task.Delay(1000);
        yield return computerAccess.PressVirtualKey(VirtualKeyShort.RETURN);
    }

    public string GetJobDescription()
    {
        return $"Opening application '{applicationName}' with the Windows key";
    }
}