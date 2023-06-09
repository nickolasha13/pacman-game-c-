using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Pacman : EntityElement
{
    public Pacman(Engine engine, Vec2 position) : base(engine)
    {
        this.InitialPosition = position;
        this.Position = position;
    }
    
    public Vec2 InitialPosition;
    public Direction Direction = Direction.Right;
    private Direction _nextDirection = Direction.Right;

    private float _time = 0;
    private float _timeToMove = 0.2f;
    
    private float _immobilizedTime = 3;
    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Up))
            this._nextDirection = Direction.Up;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Down))
            this._nextDirection = Direction.Down;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Left))
            this._nextDirection = Direction.Left;
        if (this.Engine.Input.IsReceived(InputProvider.Signal.Right))
            this._nextDirection = Direction.Right;

        var positionAfterSwitch = this.Position.Translate(this._nextDirection, 1).WrapBy(Engine.World!.Dimensions);
        if (!IsWall(positionAfterSwitch)) this.Direction = this._nextDirection;
        
        if (_immobilizedTime > 0)
        {
            _immobilizedTime -= Math.Min(deltaTime, _immobilizedTime);
            return;
        }
        
        this._time += deltaTime;
        if (this._time < this._timeToMove) return;
        this._time -= this._timeToMove;
        
        var nextPosition = this.Position.Translate(this.Direction, 1).WrapBy(Engine.World!.Dimensions);
        if (IsWall(nextPosition)) return;
        
        this.Position = nextPosition;
    }
    
    private bool IsWall(Vec2 position)
    {
        return this.Engine.World!.Map[position.Y, position.X].IsSolid;
    }
    
    public void RespawnWithImmobilization(float time)
    {
        this.Position = this.InitialPosition;
        this._immobilizedTime = time;
        this.Direction = Direction.Right;
        this._nextDirection = Direction.Right;
    }
}
