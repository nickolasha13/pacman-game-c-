using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Map;

public class Floor : MapElement
{
    public Floor(Engine engine) : base(engine)
    {
    }

    public override bool IsSolid => false;
}