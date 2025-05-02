using GaryBotCore.BotInstanceModule;
using GaryBotCore.ComputerAccessModule;
using GaryBotCore.JobModule;
using GaryBotCore.SchedulerModule;

namespace GaryBotProgram;

public static class Program
{
    private static void Main(string[] args)
    {
        var thread = new Thread(MainThread);
        thread.Start();
        thread.Join();
        Console.WriteLine("Press any key to exit");
        Console.Read();
    }

    private static async void MainThread()
    {
        var access = new GaryComputerAccess();
        var bot = new GaryBotInstance(new BotSettings(), access);
        var scheduler = new BotScheduler(bot, BuiltInSchedules.SpamSubnauticaSchedule);
        await scheduler.RunAsync();
    }
}