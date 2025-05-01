using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule.DefaultInstructions;

public class TestInstructions : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.SetMousePosition(new Point(700, 470));
        yield return computerAccess.LeftClick();
        yield return computerAccess.TypeText("Gary");
    }
}