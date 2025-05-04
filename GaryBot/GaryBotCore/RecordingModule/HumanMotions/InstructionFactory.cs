using GaryBotCore.RecordingModule.HumanMotions.MoveCursor;

namespace GaryBotCore.RecordingModule.HumanMotions;

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