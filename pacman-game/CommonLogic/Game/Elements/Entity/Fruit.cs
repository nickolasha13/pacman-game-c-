using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Fruit : EntityElement
{
    public int FruitType;
    
    public Fruit(Engine engine, Vec2 position) : base(engine)
    {
        this.Position = position;
        this.FruitType = new Random().Next(1, 8);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (this.Engine.World!.Pacman.Position.Equals(Position))
        {
            this.Engine.World.RemoveEntity(this);
            this.Engine.World.Lives++;
            this.Engine.World.Score += 500;
            this.Engine.AudioSystem.Play("pacman_eatfruit");
        }
    }
}
