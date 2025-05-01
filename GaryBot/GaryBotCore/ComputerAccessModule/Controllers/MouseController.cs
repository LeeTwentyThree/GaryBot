using System.Runtime.InteropServices;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.ComputerAccessModule.Controllers;

public class MouseController : InputSenderBase, IMouseController
{
    public async Task SetPositionAsync(Point position)
    {
        Cursor.Position = position;
        await GaryUtils.DefaultTimePadding();
    }

    public async Task<Point> GetPosition()
    {
        await GaryUtils.DefaultTimePadding();
        return Cursor.Position;
    }

    public async Task UseButtonAsync(MouseButton button)
    {
        DoClickMouse((int)button);
        await GaryUtils.DefaultTimePadding();
    }

    private static void DoClickMouse(int mouseButton)
    {
        var input = new Input(new MouseInput { dwFlags = mouseButton });
        if (SendInput(1, input, Marshal.SizeOf(input)) == 0) { 
            throw new Exception();
        }
    }
}