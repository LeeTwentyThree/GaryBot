namespace GaryBotCore.ComputerAccessModule.Controllers;

public interface IMouseController
{
    Task SetPositionAsync(Point position);
    Task<Point> GetPosition();
    Task UseButtonAsync(MouseButton button);
}