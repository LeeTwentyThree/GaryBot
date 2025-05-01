using GaryBotCore.ComputerAccessModule.Controllers;

namespace GaryBotCore.ComputerAccessModule;

public interface IGaryComputerAccess
{
    Task SetMousePosition(Point position);
    Task<Point> GetMousePosition(int button);
    Task UseMouseButton(MouseButton button);
    Task LeftClick();
    Task RightClick();
    Task TypeCharacter(char character);
    Task TypeText(string text);
    Task PasteText(string text);
}