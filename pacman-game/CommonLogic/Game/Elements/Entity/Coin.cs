using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Coin : EntityElement
{
    public Coin(Engine engine, Vec2 position) : base(engine)
    {
        this.Position = position;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (this.Engine.World!.Pacman.Position.Equals(Position))
        {
            this.Engine.World.RemoveEntity(this);
            this.Engine.World.Score += 10;
            if (this.Engine.World.CheckVictory())
                this.Engine.GameOver(this.Engine.World.Score, true);
            this.Engine.AudioSystem.Play("pacman_chomp");
        }
    }
}
