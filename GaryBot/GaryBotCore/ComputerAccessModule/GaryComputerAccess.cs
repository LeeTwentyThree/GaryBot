using GaryBotCore.ComputerAccessModule.Controllers;

namespace GaryBotCore.ComputerAccessModule;

public class GaryComputerAccess : IGaryComputerAccess
{
    private readonly MouseController _mouseController = new();
    private readonly KeyboardController _keyboardController = new();

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

    public async Task TypeText(string text)
    {
        foreach (var character in text)
        {
            await TypeCharacter(character);
        }
    }

    public Task PasteText(string text)
    {
        throw new NotImplementedException();
    }
}