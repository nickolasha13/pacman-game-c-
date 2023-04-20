using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity.Ghosts;

public class Clyde : Ghost
{
    public Clyde(Engine engine, Vec2 position) : base(engine, position)
    {
        this.IsTurnsClockwise = false;
    }

    protected override void UpdateTarget()
    {
        
    }
}
