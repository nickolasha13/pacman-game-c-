using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity.Ghosts;

public class Inky : Ghost
{
    public Inky(Engine engine, Vec2 position) : base(engine, position)
    {
        IsTurnsClockwise = true;
    }

    protected override void UpdateTarget()
    {
        Target = GetRandomFloorTile();
    }

    private Vec2 GetRandomFloorTile()
    {
        var random = new Random();
        while (true)
        {
            var pos = new Vec2(random.Next(0, Engine.World!.Map.GetLength(1)),
                random.Next(0, Engine.World.Map.GetLength(0)));
            if (!IsWall(pos))
                return pos;
        }
    }
}