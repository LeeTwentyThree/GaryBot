using GaryBotCore.ComputerAccessModule.Controllers;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.ComputerAccessModule;

public interface IGaryComputerAccess
{
    Task SetMousePosition(Point position);
    Task<Point> GetMousePosition(int button);
    Task UseMouseButton(MouseButton button);
    Task LeftClick();
    Task RightClick();
    Task TypeCharacter(char character);
    Task TypeKey(ScanCodeShort key);
    Task ReleaseKey(ScanCodeShort key);
    Task TypeText(string text);
    Task PerformHotkey(ScanCodeShort key, HotkeyModifier modifier);
    Task OpenApplication(string name, long timeOutMs);
    Task CloseApplication(string name);
    Task PasteText(string text);
    Task PressVirtualKey(VirtualKeyShort virtualKey);
}