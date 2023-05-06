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

    private Object _rebindLock = new();
    private Signal? _awaitingRebind;

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
    public abstract void Dispose();
    public abstract String GetSignalBindingAsString(Signal signal);
}
