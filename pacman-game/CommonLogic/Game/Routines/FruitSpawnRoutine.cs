using CommonLogic.Core;
using CommonLogic.Game.Elements.Entity;

namespace CommonLogic.Game.Routines;

public class FruitSpawnRoutine : Routine
{
    private bool _spawned;
    private readonly float _spawnTime = 15;
    private float _timePassed;

    public FruitSpawnRoutine(Engine engine) : base(engine)
    {
    }

    public override void Update(float deltaTime)
    {
        if (_spawned)
            return;
        if (_timePassed < _spawnTime)
        {
            _timePassed += deltaTime;
        }
        else
        {
            Engine.World!.AddEntity(new Fruit(Engine, Engine.World!.Pacman.InitialPosition));
            _spawned = true;
        }
    }
}