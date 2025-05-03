using System.Diagnostics;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.ComputerAccessModule.Controllers;

public class ProgramController : IProgramController
{
    public async Task OpenProgram(string programName, long timeOutMs)
    {
        var newProcess = Process.Start(programName);
        var processName = newProcess.ProcessName;
        
        var sw = new Stopwatch();
        sw.Start();

        await Task.Delay((int)Math.Min(timeOutMs - 2000, 5000));
        while (sw.ElapsedMilliseconds < timeOutMs)
        {
            int instances = 0;
            foreach (var _ in Process.GetProcessesByName(processName.ToLower()))
            {
                instances++;
            }

            if (instances > 0)
            {
                break;
            }

            await Task.Delay(100);
        }

        if (sw.ElapsedMilliseconds >= timeOutMs)
        {
            LoggingUtility.LogWarning($"Failed to load program '{programName}'; timed out");
        }
    }

    public async Task CloseProgram(string programName)
    {
        if (programName.EndsWith(".exe"))
        {
            LoggingUtility.LogWarning("It is generally advisable to include .exe in the application name to quit");
        }

        int found = 0;
        foreach (var process in Process.GetProcessesByName(programName.ToLower()))
        {
            process.Kill();
            found++;
        }

        if (found == 0)
        {
            LoggingUtility.LogWarning("Found no program to close!");
        }

        await Task.Delay(1000);
    }
}