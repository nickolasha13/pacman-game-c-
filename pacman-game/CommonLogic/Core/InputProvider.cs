namespace CommonLogic.Core;

public abstract class InputProvider : IDisposable
{
    public enum Signal
    {
        Up,
        Down,
        Left,
        Right,
        Confirm,
        Back,
    }

    private Object rebindLock = new();
    private Signal? awaitingRebind;

    public void Rebind(Signal signal)
    {
        lock (rebindLock)
        {
            awaitingRebind = signal;
        }
    }

    public Signal? GetAwaitingRebind()
    {
        lock (rebindLock)
        {
            return awaitingRebind;
        }
    }
    
    public abstract void Sync();
    public abstract bool IsReceived(Signal signal);
    public abstract void Dispose();
}
