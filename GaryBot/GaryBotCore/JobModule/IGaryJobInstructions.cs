using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule;

public interface IGaryJobInstructions
{
    IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess);
}