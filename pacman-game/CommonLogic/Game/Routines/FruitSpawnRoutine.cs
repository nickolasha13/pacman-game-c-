using CommonLogic.Core;
using CommonLogic.Game.Elements.Entity;

namespace CommonLogic.Game.Routines;

public class FruitSpawnRoutine : Routine
{
    float _spawnTime = 15;
    float _timePassed = 0;
    bool _spawned = false;
    
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
            this.Engine.World!.AddEntity(new Fruit(this.Engine, this.Engine.World!.Pacman.InitialPosition));
            _spawned = true;
        }
    }
}
