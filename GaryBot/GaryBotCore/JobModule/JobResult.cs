namespace GaryBotCore.JobModule;

public enum JobResult : byte
{
    Error,
    Success,
    Stopped,
    TimedOut,
    FailedToStart,
    ErrorWhileStopping,
    AlreadyStopping
}