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

    public async Task PerformHotkey(ScanCodeShort key, HotkeyModifier modifier)
    {
        var modifierCode = GaryUtils.ConvertHotkeyModifierToScanCode(modifier);
        await TypeKey(modifierCode);
        await GaryUtils.DefaultTimePadding();
        await TypeKey(key);
        await GaryUtils.DefaultTimePadding();
        await ReleaseKey(modifierCode);
    }

    public Task TypeKey(ScanCodeShort key)
    {
        var input = new Input(new KeyboardInput
        {
            wScan = key,
            dwFlags = DwFlags.ScanCode
        });

        SendInput(1, input, Marshal.SizeOf(input));
        return Task.CompletedTask;
    }

    public Task ReleaseKey(ScanCodeShort key)
    {
        var input = new Input(new KeyboardInput
        {
            wScan = key,
            dwFlags = DwFlags.ScanCode | DwFlags.KeyUp
        });

        SendInput(1, input, Marshal.SizeOf(input));
        return Task.CompletedTask;
    }

    public Task EnterCustomKeyboardInput(Input input)
    {
        SendInput(1, input, Marshal.SizeOf(input));
        return Task.CompletedTask;
    }
}