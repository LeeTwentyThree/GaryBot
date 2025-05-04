using System.Diagnostics;
using GaryBotCore.BotInstanceModule;
using GaryBotCore.ComputerAccessModule;
using GaryBotCore.SchedulerModule;
using GaryBotCore.SchedulerModule.BuiltInSchedules;

namespace GaryBotProgram;

public static class Program
{
    private static async Task Main(string[] args)
    {
        await MainThread();
    }

    private static async Task MainThread()
    {
        var access = new GaryComputerAccess();
        var bot = new GaryBotInstance(new BotSettings(), access);
        var scheduler = new BotScheduler(bot, new AutoBuildModSchedule("TheRedPlague"), access);
        await scheduler.RunAsync();
    }
}