namespace GaryBotCore.ComputerAccessModule.Controllers;

public interface IProgramController
{
    Task OpenProgram(string programName, long timeOutMs);
    // Exclude the .exe suffix
    Task CloseProgram(string programName);
}