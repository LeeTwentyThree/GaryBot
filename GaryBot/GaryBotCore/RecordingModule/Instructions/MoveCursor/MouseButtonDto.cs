using GaryBotCore.ComputerAccessModule.Controllers;

namespace GaryBotCore.RecordingModule.Instructions.MoveCursor;

[Serializable]
public class MouseButtonDto() : InstructionDtoBase("MouseButton")
{
    public MouseButton Button { get; set; }
}