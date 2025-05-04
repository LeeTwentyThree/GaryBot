namespace GaryBotCore.RecordingModule;

public interface IScreenMovementsRecorder
{
    Task<IRecording> RecordScreenMovementsAsync();
}