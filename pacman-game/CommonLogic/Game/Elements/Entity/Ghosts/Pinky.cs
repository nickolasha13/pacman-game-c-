using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity.Ghosts;

public class Pinky : Ghost
{
    public Pinky(Engine engine, Vec2 position) : base(engine, position)
    {
        IsTurnsClockwise = false;
    }

    protected override void UpdateTarget()
    {
        Target = RaycastToDecisionPoint(Engine.World!.Pacman.Direction, Engine.World!.Pacman.Position);
    }
}