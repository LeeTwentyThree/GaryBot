using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class ZipFolderJob(string path, string destination) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return computerAccess.ZipFile(path, destination);
    }

    public string GetJobDescription()
    {
        return "Zipping folder at path " + path;
    }
}