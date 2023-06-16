namespace CommonLogic.Core;

public abstract class Element
{
    protected Engine Engine;

    protected Element(Engine engine)
    {
        Engine = engine;
    }
}