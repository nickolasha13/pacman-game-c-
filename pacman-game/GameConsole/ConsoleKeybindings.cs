using CommonLogic.Core;

namespace GameConsole;

public class ConsoleKeybindings
{
    private readonly Dictionary<InputProvider.Signal, ConsoleKey> _bindings = new();

    public ConsoleKeybindings()
    {
        _bindings.Add(InputProvider.Signal.Up, ConsoleKey.UpArrow);
        _bindings.Add(InputProvider.Signal.Down, ConsoleKey.DownArrow);
        _bindings.Add(InputProvider.Signal.Left, ConsoleKey.LeftArrow);
        _bindings.Add(InputProvider.Signal.Right, ConsoleKey.RightArrow);
        _bindings.Add(InputProvider.Signal.Confirm, ConsoleKey.Enter);
        _bindings.Add(InputProvider.Signal.Back, ConsoleKey.Escape);
    }

    public InputProvider.Signal? Get(ConsoleKey key)
    {
        foreach (var binding in _bindings)
            if (binding.Value == key)
                return binding.Key;

        return null;
    }

    public ConsoleKey Get(InputProvider.Signal signal)
    {
        return _bindings[signal];
    }

    public void Rebind(InputProvider.Signal signal, ConsoleKey key)
    {
        _bindings[signal] = key;
    }

    public bool IsUsed(ConsoleKey key)
    {
        return _bindings.ContainsValue(key);
    }
}