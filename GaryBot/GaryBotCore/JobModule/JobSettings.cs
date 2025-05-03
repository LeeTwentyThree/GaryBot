namespace GaryBotCore.JobModule;

public record JobSettings(int TimeOutDuration)
{
    public static JobSettings Default => new JobSettings(-1);
}