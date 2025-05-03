using GaryBotCore.ComputerAccessModule;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class CloseApplicationJob(string? applicationName) : IGaryJobInstructions
{
    public CloseApplicationJob() : this(null)
    {
    }

    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        if (!string.IsNullOrWhiteSpace(applicationName))
        {
            yield return computerAccess.CloseApplication(applicationName);
        }
        else
        {
            yield return computerAccess.PerformHotkey(ScanCodeShort.F4, HotkeyModifier.Alt);
        }
    }

    public string GetJobDescription()
    {
        if (applicationName != null)
        {
            return $"Closing application {applicationName}";
        }
        return "Closing current application with Alt+F4";
    }
}