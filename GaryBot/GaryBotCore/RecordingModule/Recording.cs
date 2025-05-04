using System.Text.Json.Serialization;
using GaryBotCore.RecordingModule.HumanMotions;

namespace GaryBotCore.RecordingModule;

[Serializable]
public class Recording : IRecording, IJsonOnDeserialized
{
    [JsonInclude]
    private HumanMotionDtoBase[] _instructionsData;
    
    private IRecordingInstruction[]? _instructions;

    private InstructionFactory _instructionFactory = new();

    public Recording(HumanMotionDtoBase[] instructionsData)
    {
        _instructionsData = instructionsData;
        OnDeserialized();
    }
    
    [JsonConstructor]
    public Recording()
    {
    }

    public void OnDeserialized()
    {
        _instructions = new IRecordingInstruction[_instructionsData.Length];
        for (var i = 0; i < _instructionsData.Length; i++)
        {
            _instructions[i] = _instructionFactory.Create(_instructionsData[i]);
        }
    }

    public IReadOnlyList<IRecordingInstruction>? GetInstructions()
    {
        return _instructions;
    }
}