using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Energizer : EntityElement
{
    public Energizer(Engine engine, Vec2 position) : base(engine)
    {
        this.Position = position;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (this.Engine.World!.Pacman.Position.Equals(Position))
        {
            this.Engine.World.RemoveEntity(this);
            this.Engine.World.GhostsFrightenedTime = 7;
            this.Engine.World.Score += 50;
            if (this.Engine.World.CheckVictory())
                this.Engine.GameOver(this.Engine.World.Score, true);
        }
    }
}
