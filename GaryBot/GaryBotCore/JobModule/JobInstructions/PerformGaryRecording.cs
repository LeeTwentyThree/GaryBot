using GaryBotCore.ComputerAccessModule;
using GaryBotCore.RecordingModule;

namespace GaryBotCore.JobModule.JobInstructions;

public class PerformGaryRecording(Recording recording) : IGaryJobInstructions
{
    public IEnumerator<Task> GetJobInstructions(IGaryComputerAccess computerAccess)
    {
        yield return RecordingPlayer.PlayRecordingAsync(recording, computerAccess);
    }

    public string GetJobDescription()
    {
        return "Performing recording: " + recording;
    }
}