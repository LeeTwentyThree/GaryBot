using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using mrousavy;

namespace GaryBotProgram;

public class GaryHotkeys : IDisposable
{
    private WindowInteropHelper _windowInteropHelper;
    
    public GaryHotkeys()
    {
        RegisterHotkeys();
    }
    
    private readonly List<HotKey> _hotKeys = new();
    
    private void RegisterHotkeys()
    {
        var thread = new Thread(RegisterHotkeysThread);
        thread.SetApartmentState(ApartmentState.STA); 
        thread.Start();
        thread.Join();
    }

    private void RegisterHotkeysThread()
    {
        _windowInteropHelper = new WindowInteropHelper(new Window());
        var key = new HotKey(
            ModifierKeys.Shift | ModifierKeys.Control,
            Key.H,
            _windowInteropHelper,
            RecordHotkeyPressed
        );
        _hotKeys.Add(key);
    }
    
    private static void RecordHotkeyPressed(HotKey hotKey)
    {
        Console.WriteLine("Hotkey pressed!");
    }

    public void Dispose()
    {
        foreach (var key in _hotKeys)
        {
            key.Dispose();
        }
    }
}