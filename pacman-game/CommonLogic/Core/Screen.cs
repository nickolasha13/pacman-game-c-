namespace CommonLogic.Core;

public abstract class Screen
{
    protected Engine Engine;

    protected Screen(Engine engine)
    {
        Engine = engine;
    }

    public abstract void Update(float deltaTime);

    public bool IsUp()
    {
        var isReceived = Engine.Input.IsReceived(InputProvider.Signal.Up) ||
                         Engine.Input.IsReceived(InputProvider.Signal.Left);
        return isReceived;
    }

    public bool IsDown()
    {
        var isReceived = Engine.Input.IsReceived(InputProvider.Signal.Down) ||
                         Engine.Input.IsReceived(InputProvider.Signal.Right);
        return isReceived;
    }
}