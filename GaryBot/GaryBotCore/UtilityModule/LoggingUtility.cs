namespace GaryBotCore.UtilityModule;

public static class LoggingUtility
{
    public static string GetTimestamp()
    {
        return DateTime.Now.ToString("T");
    }

    public static void Log(string message)
    {
        Console.WriteLine($"[{GetTimestamp()}] {message}");
    }
    
    public static void LogWarning(string message)
    {
        Console.WriteLine($"[{GetTimestamp()}] {message}");
    }
    
    public static void LogError(string message)
    {
        Console.WriteLine($"[{GetTimestamp()}] {message}");
    }
}