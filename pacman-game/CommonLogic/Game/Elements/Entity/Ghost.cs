using CommonLogic.Core;

namespace CommonLogic.Game.Elements.Entity;

public abstract class Ghost : EntityElement
{
    protected Ghost(Engine engine, Vec2 position) : base(engine)
    {
        this.InitialPosition = position;
        this.Position = position;
        this.Target = this.Position;
        this._checkPoint = this.Position;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        this.IsReverse = this.Engine.World!.IsGhostsFrightened;
        if (this.Engine.World.IsGhostsFrightened)
            this.Target = this.Engine.World.Pacman.Position;

        this.Move(deltaTime);

        if (this.Position.Equals(this.Engine.World.Pacman.Position))
        {
            if (this.Engine.World.IsGhostsFrightened)
            {
                this.RespawnWithImmobilization(3);
                this.Engine.World.Score += 100;
            }
            else
            {
                this.Engine.World.Pacman.RespawnWithImmobilization(3);
                foreach (var ghost in this.Engine.World.Ghosts)
                    ghost.RespawnWithImmobilization(3);
                this.Engine.World.Lives--;
                if (this.Engine.World.Lives == 0)
                {
                    this.Engine.GameOver(this.Engine.World.Score, false);
                    return;
                }
            }
        }
    }

    protected Vec2 InitialPosition;
    protected Vec2 Target;
    private Vec2 _checkPoint;
    protected Direction Direction = Direction.Right;
    protected bool IsReverse = false;
    protected bool IsTurnsClockwise = false;

    protected abstract void UpdateTarget();
    
    private float _time = 0;
    private float _timeToMove = 0.2f;
    
    private float _immobilizedTime = 3;

    private void Move(float deltaTime)
    {
        if (_immobilizedTime > 0)
        {
            _immobilizedTime -= Math.Min(deltaTime, _immobilizedTime);
            return;
        }
        
        var multiplier = 1f;
        if (this.Engine.World!.IsGhostsFrightened) multiplier = 1.5f;
        this._time += deltaTime;
        if (this._time < this._timeToMove * multiplier) return;
        this._time -= this._timeToMove * multiplier;
        
        if (this.Position.Equals(this._checkPoint))
        {
            if (!this.Engine.World!.IsGhostsFrightened) UpdateTarget();
            this._checkPoint = GetNextCheckpoint();
        }

        var nextPosition = this.Position.Translate(this.Direction, 1).WrapBy(Engine.World!.Dimensions);
        if (IsWall(nextPosition)) return;
        this.Position = nextPosition;
    }

    public void RespawnWithImmobilization(float time)
    {
        this.Position = this.InitialPosition;
        this._immobilizedTime = time;
        this.Target = this.Position;
        this._checkPoint = this.Position;
        this.Direction = Direction.Right;
    }
    
    private Direction[] GetAvailableDirections(Vec2 pos)
    {
        var directions = new List<Direction>();
        if (!IsWall(pos.Translate(Direction.Up, 1).WrapBy(Engine.World!.Dimensions))) directions.Add(Direction.Up);
        if (!IsWall(pos.Translate(Direction.Left, 1).WrapBy(Engine.World!.Dimensions))) directions.Add(Direction.Left);
        if (!IsWall(pos.Translate(Direction.Down, 1).WrapBy(Engine.World!.Dimensions))) directions.Add(Direction.Down);
        if (!IsWall(pos.Translate(Direction.Right, 1).WrapBy(Engine.World!.Dimensions))) directions.Add(Direction.Right);
        if (IsTurnsClockwise) directions.Reverse();
        return directions.ToArray();
    }

    private int GetDirectionHeat(Direction direction)
    {
        var heat = 0;
        switch (direction)
        {
            case Direction.Left:
                heat = this.Position.X - this.Target.X;
                break;
            case Direction.Right:
                heat = this.Target.X - this.Position.X;
                break;
            case Direction.Up:
                heat = this.Position.Y - this.Target.Y;
                break;
            case Direction.Down:
                heat = this.Target.Y - this.Position.Y;
                break;
        }

        if (IsReverse) heat *= -1;
        return heat;
    }

    protected Vec2 RaycastToDecisionPoint(Direction direction, Vec2 from)
    {
        var position = from;
        var first = true;
        while (true)
        {
            var prevPosition = position;
            position = position.Translate(direction, 1).WrapBy(Engine.World!.Dimensions);
            if (IsWall(position)) return prevPosition;
            if (!first && GetAvailableDirections(position).Length > 2) return position;
            if (!first && from.Equals(position)) return position;
            first = false;
        }
    }

    private Vec2 GetNextCheckpoint()
    {
        var directions = GetAvailableDirections(this.Position);
        if (directions.Length == 1) return RaycastToDecisionPoint(directions[0], this.Position);

        var lastDirectionIndex = Array.IndexOf(directions, this.Direction);
        if (lastDirectionIndex != -1)
        {
            for (var i = lastDirectionIndex; i < directions.Length - 1; i++)
                directions[i] = directions[i + 1];
            directions[^1] = this.Direction;
        }

        foreach (var direction in directions)
        {
            if (GetDirectionHeat(direction) > 0 && direction != DirectionHelper.GetOpposite(this.Direction))
            {
                this.Direction = direction;
                return RaycastToDecisionPoint(direction, this.Position);
            }
        }

        foreach (var direction in directions)
        {
            if (direction != DirectionHelper.GetOpposite(this.Direction))
            {
                this.Direction = direction;
                return RaycastToDecisionPoint(direction, this.Position);
            }
        }

        return this.Position;
    }

    protected bool IsWall(Vec2 position)
    {
        return this.Engine.World!.Map[position.Y, position.X].IsSolid;
    }
}
