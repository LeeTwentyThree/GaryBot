namespace GaryBotCore.ComputerAccessModule.Controllers;

public enum MouseButton : byte
{
    None = 0,
    LeftDown = 0x2,
    LeftUp = 0x4,
    RightDown = 0x8,
    RightUp = 0x16
}