namespace CommonLogic.Core;

public abstract class EntityElement : Element
{
    public Vec2 Position;
    public Vec2 PreUpdatePosition;

    protected EntityElement(Engine engine) : base(engine)
    {
    }

    public virtual void Update(float deltaTime)
    {
        PreUpdatePosition = Position;
    }
}