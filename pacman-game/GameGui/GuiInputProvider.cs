using System.Collections.Concurrent;
using CommonLogic.Core;
using SFML.Window;

namespace GameGui;

public class GuiInputProvider : InputProvider
{
    private readonly GuiKeybindings keybindings;
    private readonly HashSet<Signal> received = new();
    private readonly ConcurrentQueue<Signal> signals = new();

    public GuiInputProvider(GuiKeybindings keybindings)
    {
        this.keybindings = keybindings;
    }

    public void SubmitKey(Keyboard.Key key)
    {
        var awaitingRebind = GetAwaitingRebind();
        if (awaitingRebind != null)
        {
            if (keybindings.IsUsed(key) && keybindings.Get(awaitingRebind.Value) != key)
                return;
            keybindings.Rebind(awaitingRebind.Value, key);
            ResetRebinding();
            return;
        }

        var signal = keybindings.Get(key);
        if (signal != null)
            signals.Enqueue(signal.Value);
    }

    public override void Sync()
    {
        received.Clear();
        while (signals.TryDequeue(out var signal))
            received.Add(signal);
    }

    public override bool IsReceived(Signal signal)
    {
        return received.Contains(signal);
    }

    public override void Dispose()
    {
    }

    public override string GetSignalBindingAsString(Signal signal)
    {
        return keybindings.Get(signal).ToString();
    }
}