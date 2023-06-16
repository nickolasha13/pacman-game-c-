using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public abstract class Ghost : EntityElement
{
    private Vec2 _checkPoint;

    private float _immobilizedTime = 3;

    private float _time;
    private readonly float _timeToMove = 0.2f;
    protected Direction Direction = Direction.Right;

    protected Vec2 InitialPosition;
    protected bool IsReverse;
    protected bool IsTurnsClockwise = false;
    protected Vec2 Target;

    protected Ghost(Engine engine, Vec2 position) : base(engine)
    {
        InitialPosition = position;
        Position = position;
        Target = Position;
        _checkPoint = Position;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        IsReverse = Engine.World!.IsGhostsFrightened;
        if (Engine.World.IsGhostsFrightened)
            Target = Engine.World.Pacman.Position;

        Move(deltaTime);

        if (Position.Equals(Engine.World.Pacman.Position))
        {
            if (Engine.World.IsGhostsFrightened)
            {
                RespawnWithImmobilization(3);
                Engine.World.Score += 100;
                Engine.AudioSystem.Play("pacman_eatghost");
            }
            else
            {
                Engine.World.Pacman.RespawnWithImmobilization(3);
                foreach (var ghost in Engine.World.Ghosts)
                    ghost.RespawnWithImmobilization(3);
                Engine.World.Lives--;
                if (Engine.World.Lives == 0)
                {
                    Engine.GameOver(Engine.World.Score, false);
                    return;
                }

                Engine.AudioSystem.Play("pacman_death");
            }
        }
    }

    protected abstract void UpdateTarget();

    private void Move(float deltaTime)
    {
        if (_immobilizedTime > 0)
        {
            _immobilizedTime -= Math.Min(deltaTime, _immobilizedTime);
            return;
        }

        var multiplier = 1f;
        if (Engine.World!.IsGhostsFrightened)
            multiplier = 1.5f;
        _time += deltaTime;
        if (_time < _timeToMove * multiplier)
            return;
        _time -= _timeToMove * multiplier;

        if (Position.Equals(_checkPoint))
        {
            if (!Engine.World!.IsGhostsFrightened)
                UpdateTarget();
            _checkPoint = GetNextMovementPoint();
        }

        var nextPosition = Position.TranslateWrapped(Direction, 1, Engine.World!.Dimensions);
        if (IsWall(nextPosition))
            return;
        Position = nextPosition;
    }

    public void RespawnWithImmobilization(float time)
    {
        Position = InitialPosition;
        _immobilizedTime = time;
        Target = Position;
        _checkPoint = Position;
        Direction = Direction.Right;
    }

    private Direction[] GetAvailableDirections(Vec2 pos)
    {
        var directions = new List<Direction>();
        foreach (var direction in Enum.GetValues<Direction>())
            if (!IsWall(pos.TranslateWrapped(direction, 1, Engine.World!.Dimensions)))
                directions.Add(direction);
        if (IsTurnsClockwise)
            directions.Reverse();
        return directions.ToArray();
    }

    private int GetDirectionHeat(Direction direction)
    {
        var heat = 0;
        switch (direction)
        {
            case Direction.Left:
                heat = Position.X - Target.X;
                break;
            case Direction.Right:
                heat = Target.X - Position.X;
                break;
            case Direction.Up:
                heat = Position.Y - Target.Y;
                break;
            case Direction.Down:
                heat = Target.Y - Position.Y;
                break;
        }

        if (IsReverse)
            heat *= -1;
        return heat;
    }

    protected Vec2 RaycastToDecisionPoint(Direction direction, Vec2 from)
    {
        var position = from;
        while (true)
        {
            var prevPosition = position;
            position = position.TranslateWrapped(direction, 1, Engine.World!.Dimensions);
            if (IsWall(position))
                return prevPosition;
            if (GetAvailableDirections(position).Length > 2)
                return position;
            if (from.Equals(position))
                return position;
        }
    }

    private Vec2 GetNextMovementPoint()
    {
        var directions = GetAvailableDirections(Position);
        if (directions.Length == 1)
            return RaycastToDecisionPoint(directions[0], Position);

        var lastDirectionIndex = Array.IndexOf(directions, Direction);
        if (lastDirectionIndex != -1)
        {
            for (var i = lastDirectionIndex; i < directions.Length - 1; i++)
                directions[i] = directions[i + 1];
            directions[^1] = Direction;
        }

        foreach (var direction in directions)
            if (GetDirectionHeat(direction) > 0 && direction != DirectionHelper.GetOpposite(Direction))
            {
                Direction = direction;
                return RaycastToDecisionPoint(direction, Position);
            }

        foreach (var direction in directions)
            if (direction != DirectionHelper.GetOpposite(Direction))
            {
                Direction = direction;
                return RaycastToDecisionPoint(direction, Position);
            }

        return Position;
    }

    protected bool IsWall(Vec2 position)
    {
        return Engine.World!.Map[position.Y, position.X].IsSolid;
    }
}