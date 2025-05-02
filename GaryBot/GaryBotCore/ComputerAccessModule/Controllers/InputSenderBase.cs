using System.Runtime.InteropServices;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.ComputerAccessModule.Controllers;

public class InputSenderBase
{
    public const int DwTypeMouse = 0;
    public const int DwTypeKeyboard = 1;
    public const int DwTypeHardware = 2;

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public UIntPtr dwExtraInfo;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput {
        // The virtual-key code: https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        public VirtualKeyShort wVk;
        public ScanCodeShort wScan;
        public DwFlags dwFlags;
        public int time;
        public UIntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        public uint dwType;
        public InputUnion u;

        public Input(MouseInput mouseInput)
        {
            dwType = DwTypeMouse;
            u = new InputUnion { mi = mouseInput };
        }
        
        public Input(KeyboardInput keyboardInput)
        {
            dwType = DwTypeKeyboard;
            u = new InputUnion { ki = keyboardInput };
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)]
        internal MouseInput mi;
        [FieldOffset(0)]
        internal KeyboardInput ki;
    }
    
    [Flags]
    public enum DwFlags : uint
    {
        None,
        ExtendedKey = 0x0001,
        KeyUp = 0x0002,
        ScanCode = 0x0008,
        Unicode = 0x0004,
    }
    
    [DllImport("user32.dll", SetLastError=true)]
    protected static extern uint SendInput(uint cInputs, Input input, int size);
    
    // https://stackoverflow.com/questions/20482338/simulate-keyboard-input-in-c-sharp
}