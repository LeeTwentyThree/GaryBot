using System.Runtime.InteropServices;

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
        if (_hardwareKeys == null)
        {
            BuildHardwareKeyDictionary();
        }

        if (_hardwareKeys.TryGetValue(c, out var value))
        {
            return value;
        }

        Console.WriteLine("Failed to get hardware key code for character: " + c);
        return 0;
    }

    private static void BuildHardwareKeyDictionary()
    {
        _hardwareKeys = new Dictionary<char, short>();
        for (var i = 0; i < 1024; i++)
        {
            var character = (char)i;
            var scan = VkKeyScan(character);
            if (scan == 0) continue;
            _hardwareKeys[character] = scan;
        }
    }
    
    [DllImport("user32.dll")]
    private static extern short VkKeyScan(char ch);

}