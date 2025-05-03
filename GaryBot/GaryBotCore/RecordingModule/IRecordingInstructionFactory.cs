namespace GaryBotCore.RecordingModule;

public interface IRecordingInstructionFactory
{
    IRecordingInstruction Create(object dto);
}