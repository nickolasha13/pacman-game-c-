using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Fruit : EntityElement
{
    public int FruitType;

    public Fruit(Engine engine, Vec2 position) : base(engine)
    {
        Position = position;
        FruitType = new Random().Next(1, 8);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Engine.World!.Pacman.Position.Equals(Position))
        {
            Engine.World.RemoveEntity(this);
            Engine.World.Lives++;
            Engine.World.Score += 500;
            Engine.AudioSystem.Play("pacman_eatfruit");
        }
    }
}