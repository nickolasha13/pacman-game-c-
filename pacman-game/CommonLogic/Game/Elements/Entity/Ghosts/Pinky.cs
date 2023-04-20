using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity.Ghosts;

public class Pinky : Ghost
{
    public Pinky(Engine engine, Vec2 position) : base(engine, position)
    {
        this.IsTurnsClockwise = false;
    }

    protected override void UpdateTarget()
    {
        this.Target = RaycastToDecisionPoint(this.Engine.World!.Pacman.Direction, this.Engine.World!.Pacman.Position);
    }
}
