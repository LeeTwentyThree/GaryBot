using GaryBotCore.RecordingModule.Instructions.MoveCursor;

namespace GaryBotCore.RecordingModule;

public class InstructionFactory : IRecordingInstructionFactory
{
    public IRecordingInstruction Create(object dto)
    {
        switch (dto)
        {
            case MoveCursorDto moveCursor:
                return new MoveCursorInstruction(moveCursor);
            case MouseButtonDto mouseButton:
                return new MouseButtonInstruction(mouseButton);
            default:
                throw new InvalidOperationException("Unknown instruction DTO: " + dto);
        }
    }
}