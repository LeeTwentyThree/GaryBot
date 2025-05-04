using GaryBotCore.ComputerAccessModule.Controllers;

namespace GaryBotCore.RecordingModule.HumanMotions.MoveCursor;

[Serializable]
public class MouseButtonDto() : HumanMotionDtoBase("MouseButton")
{
    public MouseButton Button { get; set; }
}