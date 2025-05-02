using GaryBotCore.ComputerAccessModule.Controllers;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.ComputerAccessModule;

public class GaryComputerAccess : IGaryComputerAccess
{
    private readonly MouseController _mouseController = new();
    private readonly KeyboardController _keyboardController = new();
    private readonly ClipboardController _clipboardController = new();

    public async Task SetMousePosition(Point position)
    {
        await _mouseController.SetPositionAsync(position);
    }

    public async Task<Point> GetMousePosition(int button)
    {
        return await _mouseController.GetPosition();
    }

    public async Task UseMouseButton(MouseButton button)
    {
        await _mouseController.UseButtonAsync(button);
    }

    public async Task LeftClick()
    {
        await UseMouseButton(MouseButton.LeftDown);
        await UseMouseButton(MouseButton.LeftUp);
    }

    public async Task RightClick()
    {
        await UseMouseButton(MouseButton.RightDown);
        await UseMouseButton(MouseButton.RightUp);
    }

    public async Task TypeCharacter(char character)
    {
        await _keyboardController.TypeCharacterAsync(character);
    }

    public async Task TypeKey(ScanCodeShort key)
    {
        await _keyboardController.TypeKey(key);
    }

    public async Task ReleaseKey(ScanCodeShort key)
    {
        await _keyboardController.ReleaseKey(key);
    }

    public async Task TypeText(string text)
    {
        foreach (var character in text)
        {
            await TypeCharacter(character);
        }
    }

    public async Task PerformHotkey(ScanCodeShort key, HotkeyModifier modifier)
    {
        await _keyboardController.PerformHotkey(key, modifier);
    }

    public async Task PasteText(string text)
    {
        await _clipboardController.SetClipboard(text);
        await _keyboardController.PerformHotkey(ScanCodeShort.KEY_V, HotkeyModifier.Control);
    }

    public async Task PressVirtualKey(VirtualKeyShort virtualKey)
    {
        await _keyboardController.EnterCustomKeyboardInput(new InputSenderBase.Input(new InputSenderBase.KeyboardInput
        {
            wVk = virtualKey,
        }));
        await _keyboardController.EnterCustomKeyboardInput(new InputSenderBase.Input(new InputSenderBase.KeyboardInput
        {
            wVk = virtualKey,
            dwFlags = InputSenderBase.DwFlags.KeyUp
        }));
    }
}