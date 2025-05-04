using System.Text.Json.Serialization;

namespace GaryBotCore.RecordingModule.HumanMotions.MoveCursor;

[Serializable]
public class MoveCursorDto(int x, int y) : HumanMotionDtoBase("MoveCursor")
{
    [JsonConstructor]
    public MoveCursorDto() : this(default, default)
    {
    }

    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}