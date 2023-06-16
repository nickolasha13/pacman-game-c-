namespace CommonLogic.Core;

public abstract class Routine
{
    protected Engine Engine;

    protected Routine(Engine engine)
    {
        Engine = engine;
    }

    public abstract void Update(float deltaTime);
}