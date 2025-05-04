using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.RecordingModule.HumanMotions.MoveCursor;

[Serializable]
public class MoveCursorInstruction(MoveCursorDto moveCursorDto) : IRecordingInstruction
{
    public long Timeline => moveCursorDto.Timeline;
    
    public async Task ExecuteAsync(IGaryComputerAccess computerAccess)
    {
        await computerAccess.SetMousePosition(new Point(moveCursorDto.X, moveCursorDto.Y));
    }
}