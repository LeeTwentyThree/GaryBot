namespace GaryBotCore.RecordingModule;

public interface IRecording
{
    IReadOnlyList<IRecordingInstruction>? GetInstructions();
}