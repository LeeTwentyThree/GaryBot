namespace GaryBotCore.ComputerAccessModule.Controllers;

public interface IClipboardController
{
    Task SetClipboard(string text);
    Task<string> GetClipboard(string text);
}