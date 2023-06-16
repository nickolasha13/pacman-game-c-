using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Coin : EntityElement
{
    public Coin(Engine engine, Vec2 position) : base(engine)
    {
        Position = position;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Engine.World!.Pacman.Position.Equals(Position))
        {
            Engine.World.RemoveEntity(this);
            Engine.World.Score += 10;
            if (Engine.World.CheckVictory())
                Engine.GameOver(Engine.World.Score, true);
            Engine.AudioSystem.Play("pacman_chomp");
        }
    }
}