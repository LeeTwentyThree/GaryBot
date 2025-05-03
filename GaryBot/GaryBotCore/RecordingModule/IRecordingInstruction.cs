using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.RecordingModule;

public interface IRecordingInstruction
{
    long Timeline { get; }
    
    Task ExecuteAsync(IGaryComputerAccess computerAccess);
}