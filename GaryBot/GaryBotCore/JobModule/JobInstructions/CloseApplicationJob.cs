using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class CloseApplicationJob : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.PerformHotkey(ScanCodeShort.F4, HotkeyModifier.Alt);
    }
}