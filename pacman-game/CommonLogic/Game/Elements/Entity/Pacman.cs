using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public class Pacman : EntityElement
{
    private float _immobilizedTime = 3;
    private Direction _nextDirection = Direction.Right;

    private float _time;
    private readonly float _timeToMove = 0.2f;
    public Direction Direction = Direction.Right;

    public Vec2 InitialPosition;

    public Pacman(Engine engine, Vec2 position) : base(engine)
    {
        InitialPosition = position;
        Position = position;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Engine.Input.IsReceived(InputProvider.Signal.Up))
            _nextDirection = Direction.Up;
        if (Engine.Input.IsReceived(InputProvider.Signal.Down))
            _nextDirection = Direction.Down;
        if (Engine.Input.IsReceived(InputProvider.Signal.Left))
            _nextDirection = Direction.Left;
        if (Engine.Input.IsReceived(InputProvider.Signal.Right))
            _nextDirection = Direction.Right;

        var positionAfterSwitch = Position.Translate(_nextDirection, 1).WrapBy(Engine.World!.Dimensions);
        if (!IsWall(positionAfterSwitch))
            Direction = _nextDirection;

        if (_immobilizedTime > 0)
        {
            _immobilizedTime -= Math.Min(deltaTime, _immobilizedTime);
            return;
        }

        _time += deltaTime;
        if (_time < _timeToMove)
            return;
        _time -= _timeToMove;

        var nextPosition = Position.Translate(Direction, 1).WrapBy(Engine.World!.Dimensions);
        if (IsWall(nextPosition))
            return;

        Position = nextPosition;
    }

    private bool IsWall(Vec2 position)
    {
        return Engine.World!.Map[position.Y, position.X].IsSolid;
    }

    public void RespawnWithImmobilization(float time)
    {
        Position = InitialPosition;
        _immobilizedTime = time;
        Direction = Direction.Right;
        _nextDirection = Direction.Right;
    }
}