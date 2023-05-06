namespace CommonLogic.Core;

public abstract class Screen
{
    protected Engine Engine;
    
    protected Screen(Engine engine)
    {
        this.Engine = engine;
    }
    
    public abstract void Update(float deltaTime);
}
