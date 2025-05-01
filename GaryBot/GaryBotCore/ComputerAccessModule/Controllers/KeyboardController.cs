using System.Runtime.InteropServices;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.ComputerAccessModule.Controllers;

public class KeyboardController : InputSenderBase, IKeyboardController
{
    public async Task TypeCharacterAsync(char c)
    {
        var scanned = GaryUtils.ConvertCharacterToHardwareKey(c);
        
        var input = new Input(new KeyboardInput
        {
            wScan = (ScanCodeShort)scanned,
            dwFlags = DwFlags.ScanCode
        });

        if (SendInput(1, input, Marshal.SizeOf(input)) == 0)
        {
            throw new Exception();
        }

        await GaryUtils.DefaultTimePadding();
    }
}