using System.Collections.Concurrent;
using CommonLogic.Core;

namespace GameConsole;

public class ConsoleInputProvider : InputProvider
{
    private Thread worker;
    private ConcurrentQueue<Signal> signals = new();
    private ConsoleKeybindings keybindings;
    private HashSet<Signal> received = new();

    public ConsoleInputProvider(ConsoleKeybindings keybindings)
    {
        this.keybindings = keybindings;
        worker = new Thread(Worker);
        worker.Start();
    }

    private void Worker()
    {
        while (true)
        {
            var key = Console.ReadKey(true);
            var awaitingRebind = GetAwaitingRebind();
            if (awaitingRebind != null)
            {
                if (keybindings.IsUsed(key.Key)) continue;
                keybindings.Rebind(awaitingRebind.Value, key.Key);
                continue;
            }
            
            var signal = keybindings.Get(key.Key);
            if (signal != null)
                signals.Enqueue(signal.Value);
        }
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
        worker.Interrupt();
    }
}
