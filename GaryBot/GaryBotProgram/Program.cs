using GaryBotCore.BotInstanceModule;
using GaryBotCore.ComputerAccessModule;
using GaryBotCore.JobModule;
using GaryBotCore.JobModule.DefaultInstructions;

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
        var bot = new GaryBotInstance(new BotSettings());
        var access = new GaryComputerAccess();
        var job = new GaryJob(access,
            new JobSettings(100),
            new TestInstructions());
        await bot.RunJobAsync(job);
    }
}