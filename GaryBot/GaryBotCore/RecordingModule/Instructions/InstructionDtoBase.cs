using System.Text.Json.Serialization;
using GaryBotCore.RecordingModule.Instructions.MoveCursor;

namespace GaryBotCore.RecordingModule.Instructions;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "instructionTypeName")]
[JsonDerivedType(typeof(MoveCursorDto),"MoveCursor")]
[JsonDerivedType(typeof(MouseButtonDto),"MouseButton")]
[Serializable]
public abstract class InstructionDtoBase(string instructionTypeName)
{
    public long Timeline { get; set; }
}