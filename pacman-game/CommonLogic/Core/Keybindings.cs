namespace CommonLogic.Core;

public abstract class Keybindings<TKey> where TKey: IConvertible
{
    public abstract InputProvider.Signal? Get(TKey key);
}
