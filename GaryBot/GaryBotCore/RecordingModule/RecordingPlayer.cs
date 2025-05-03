using System.Diagnostics;
using GaryBotCore.ComputerAccessModule;

namespace GaryBotCore.RecordingModule;

public static class RecordingPlayer
{
    public static async Task PlayRecordingAsync(IRecording recording, IGaryComputerAccess computerAccess)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var instructions = recording.GetInstructions() ?? Array.Empty<IRecordingInstruction>();
        var recordingInstructions = new Queue<IRecordingInstruction>(instructions.OrderBy(i => i.Timeline));
        while (recordingInstructions.Count > 0)
        {
            var nextInstruction = recordingInstructions.Dequeue();
            while (stopwatch.ElapsedMilliseconds < nextInstruction.Timeline)
            {
                await Task.Delay(1);
            }

            await nextInstruction.ExecuteAsync(computerAccess);
        }
    }
}