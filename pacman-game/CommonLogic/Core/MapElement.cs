namespace CommonLogic.Core;

public abstract class MapElement: Element
{
    public abstract bool IsSolid { get; }
    protected MapElement(Engine engine): base(engine)
    {
    }
}
