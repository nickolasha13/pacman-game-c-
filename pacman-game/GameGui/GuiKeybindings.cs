using CommonLogic.Core;
using SFML.Window;

namespace GameGui;

public class GuiKeybindings
{
    private Dictionary<InputProvider.Signal, Keyboard.Key> _bindings = new();
    
    public GuiKeybindings()
    {
        _bindings.Add(InputProvider.Signal.Up, Keyboard.Key.Up);
        _bindings.Add(InputProvider.Signal.Down, Keyboard.Key.Down);
        _bindings.Add(InputProvider.Signal.Left, Keyboard.Key.Left);
        _bindings.Add(InputProvider.Signal.Right, Keyboard.Key.Right);
        _bindings.Add(InputProvider.Signal.Confirm, Keyboard.Key.Enter);
        _bindings.Add(InputProvider.Signal.Back, Keyboard.Key.Escape);
    }

    public InputProvider.Signal? Get(Keyboard.Key key)
    {
        foreach (var binding in _bindings)
            if (binding.Value == key)
                return binding.Key;

        return null;
    }
    
    public Keyboard.Key Get(InputProvider.Signal signal)
    {
        return _bindings[signal];
    }
    
    public void Rebind(InputProvider.Signal signal, Keyboard.Key key)
    {
        _bindings[signal] = key;
    }
    
    public bool IsUsed(Keyboard.Key key)
    {
        return _bindings.ContainsValue(key);
    }
}
