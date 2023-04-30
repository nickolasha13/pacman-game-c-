using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity.Ghosts;

public class Clyde : Ghost
{
    public Clyde(Engine engine, Vec2 position) : base(engine, position)
    {
        this.IsTurnsClockwise = false;
    }

    private int _quadrant;
    
    protected override void UpdateTarget()
    {
        _quadrant++;
        if (_quadrant > 4) _quadrant = 1;
        var (levelWidth, levelHeight) = (this.Engine.World!.Map.GetLength(1), this.Engine.World.Map.GetLength(0));
        var (topLeft, bottomRight) = _quadrant switch {
            1 => (new Vec2(0, 0), new Vec2(levelWidth / 2, levelHeight / 2)),
            2 => (new Vec2(levelWidth / 2, 0), new Vec2(levelWidth, levelHeight / 2)),
            3 => (new Vec2(0, levelHeight / 2), new Vec2(levelWidth / 2, levelHeight)),
            4 => (new Vec2(levelWidth / 2, levelHeight / 2), new Vec2(levelWidth, levelHeight)),
            _ => throw new Exception("Invalid quadrant")
        };
        this.Target = GetRandomFloorTileInQuadrant(topLeft, bottomRight);
    }
    
    private Vec2 GetRandomFloorTileInQuadrant(Vec2 topLeft, Vec2 bottomRight)
    {
        var random = new Random();
        while (true)
        {
            var pos = new Vec2(random.Next(topLeft.X, bottomRight.X), random.Next(topLeft.Y, bottomRight.Y));
            if (!IsWall(pos)) return pos;
        }
    }
}
