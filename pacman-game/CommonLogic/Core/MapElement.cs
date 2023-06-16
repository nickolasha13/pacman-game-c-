namespace CommonLogic.Core;

public abstract class MapElement : Element
{
    protected MapElement(Engine engine) : base(engine)
    {
    }

    public abstract bool IsSolid { get; }
}