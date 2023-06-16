using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Energizer : EntityElement
{
    public Energizer(Engine engine, Vec2 position) : base(engine)
    {
        Position = position;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Engine.World!.Pacman.Position.Equals(Position))
        {
            Engine.World.RemoveEntity(this);
            Engine.World.GhostsFrightenedTime = 7;
            Engine.World.Score += 50;
            if (Engine.World.CheckVictory())
                Engine.GameOver(Engine.World.Score, true);
            Engine.AudioSystem.Play("pacman_eatfruit");
        }
    }
}