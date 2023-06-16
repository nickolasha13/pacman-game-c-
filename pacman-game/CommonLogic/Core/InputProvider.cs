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
        Back
    }

    private Signal? _awaitingRebind;

    private readonly object _rebindLock = new();
    public abstract void Dispose();

    public void Rebind(Signal signal)
    {
        lock (_rebindLock)
        {
            _awaitingRebind = signal;
        }
    }

    public Signal? GetAwaitingRebind()
    {
        lock (_rebindLock)
        {
            return _awaitingRebind;
        }
    }

    protected void ResetRebinding()
    {
        lock (_rebindLock)
        {
            _awaitingRebind = null;
        }
    }

    public abstract void Sync();
    public abstract bool IsReceived(Signal signal);
    public abstract string GetSignalBindingAsString(Signal signal);
}