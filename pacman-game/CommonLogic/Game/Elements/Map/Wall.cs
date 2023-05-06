using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Map;

public class Wall : MapElement
{
    public Wall(Engine engine) : base(engine)
    {
    }

    public override bool IsSolid => true;
}
