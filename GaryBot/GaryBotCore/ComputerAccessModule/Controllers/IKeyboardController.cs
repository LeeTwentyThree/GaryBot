using GaryBotCore.UtilityModule;

namespace GaryBotCore.ComputerAccessModule.Controllers;

public interface IKeyboardController
{
    Task TypeCharacterAsync(char c);
    Task PerformHotkey(ScanCodeShort key, HotkeyModifier modifier);
    Task TypeKey(ScanCodeShort key);
    Task ReleaseKey(ScanCodeShort key);
    Task EnterCustomKeyboardInput(InputSenderBase.Input keyboardInput);
}