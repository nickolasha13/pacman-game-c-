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
        if (this.Engine.World!.Pacman.Position.Equals(Position))
        {
            this.Engine.World.RemoveEntity(this);
            this.Engine.World.Score += 10;
        }
    }
}
