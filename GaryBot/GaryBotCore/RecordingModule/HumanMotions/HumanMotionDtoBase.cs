using System.Text.Json.Serialization;
using GaryBotCore.RecordingModule.HumanMotions.MoveCursor;

namespace GaryBotCore.RecordingModule.HumanMotions;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "instructionTypeName")]
[JsonDerivedType(typeof(MoveCursorDto),"MoveCursor")]
[JsonDerivedType(typeof(MouseButtonDto),"MouseButton")]
[Serializable]
public abstract class HumanMotionDtoBase(string instructionTypeName)
{
    public long Timeline { get; set; }
}