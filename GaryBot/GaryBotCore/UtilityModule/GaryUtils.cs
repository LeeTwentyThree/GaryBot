using System.Runtime.InteropServices;
using GaryBotCore.ComputerAccessModule.Controllers;

namespace GaryBotCore.UtilityModule;

public static class GaryUtils
{
    private static Dictionary<char, short>? _hardwareKeys;
    
    public static Task DefaultTimePadding()
    {
        return Task.Delay(1);
    }

    public static short ConvertCharacterToHardwareKey(char c)
    {
        throw new NotImplementedException();
    }
    
    public static ScanCodeShort ConvertHotkeyModifierToScanCode(HotkeyModifier modifier)
    {
        return modifier switch
        {
            HotkeyModifier.Control => ScanCodeShort.CONTROL,
            HotkeyModifier.Shift => ScanCodeShort.SHIFT,
            HotkeyModifier.Alt => ScanCodeShort.LMENU,
            _ => throw new NotImplementedException("Hotkey modifier not implemented: " + modifier)
        };
    }
    
    [DllImport("user32.dll")]
    private static extern short VkKeyScan(char ch);

    public static MouseButton ConvertMouseButton(MouseButtons windowsVersion, bool down)
    {
        switch (windowsVersion)
        {
            case MouseButtons.Left:
                return down ? MouseButton.LeftDown : MouseButton.LeftUp;
            case MouseButtons.Right:
                return down ? MouseButton.RightDown : MouseButton.RightUp;
            default:
                return MouseButton.None;
        }
    }
}