using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class GetClipboardTextJob : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        var thread = new Thread(() => JobResult = Clipboard.GetText());
        thread.SetApartmentState(ApartmentState.STA); // Set the thread to STA
        thread.Start();
        yield return Task.Run(thread.Join);
    }

    public string GetJobDescription()
    {
        return "Fetching clipboard text";
    }
    
    public string JobResult { get; private set; }
}