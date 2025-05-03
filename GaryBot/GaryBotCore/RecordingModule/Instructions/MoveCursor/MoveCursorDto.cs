using System.Text.Json.Serialization;

namespace GaryBotCore.RecordingModule.Instructions.MoveCursor;

[Serializable]
public class MoveCursorDto(int x, int y) : InstructionDtoBase("MoveCursor")
{
    [JsonConstructor]
    public MoveCursorDto() : this(default, default)
    {
    }

    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}