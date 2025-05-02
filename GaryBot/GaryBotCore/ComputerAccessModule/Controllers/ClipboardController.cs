namespace GaryBotCore.ComputerAccessModule.Controllers;

public class ClipboardController : IClipboardController
{
    public async Task SetClipboard(string text)
    {
        var thread = new Thread(() => Clipboard.SetText(text));
        thread.SetApartmentState(ApartmentState.STA); // Set the thread to STA
        thread.Start();
        await Task.Run(thread.Join);
    }

    public Task<string> GetClipboard(string text)
    {
        return Task.FromResult(Clipboard.GetText());
    }
}