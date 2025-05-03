using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.RecordingModule.Instructions.MoveCursor;

[Serializable]
public class MouseButtonInstruction(MouseButtonDto mouseButtonDto) : IRecordingInstruction
{
    public long Timeline => mouseButtonDto.Timeline;
    
    public async Task ExecuteAsync(IGaryComputerAccess computerAccess)
    {
        await computerAccess.UseMouseButton(mouseButtonDto.Button);
    }
}