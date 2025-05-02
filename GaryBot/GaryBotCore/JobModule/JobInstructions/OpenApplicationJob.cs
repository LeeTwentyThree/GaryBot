using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class OpenApplicationJob(string applicationName) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.PressVirtualKey(VirtualKeyShort.LWIN);  // Windows key
        yield return Task.Delay(1000);
        yield return computerAccess.PasteText(applicationName);
        yield return computerAccess.PressVirtualKey(VirtualKeyShort.RETURN);
    }
}