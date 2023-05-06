using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity.Ghosts;

public class Blinky : Ghost
{
    public Blinky(Engine engine, Vec2 position) : base(engine, position)
    {
        this.IsTurnsClockwise = true;
    }

    protected override void UpdateTarget()
    {
        this.Target = this.Engine.World!.Pacman.Position;
    }
}
