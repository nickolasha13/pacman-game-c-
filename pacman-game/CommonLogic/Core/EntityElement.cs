namespace CommonLogic.Core;

public abstract class EntityElement : Element
{
    public Vec2 Position;

    protected EntityElement(Engine engine) : base(engine)
    {
    }

    public abstract void Update(float deltaTime);
}
