namespace CommonLogic.Core;

public struct Vec2 : IEquatable<Vec2>
{
    public int X;
    public int Y;

    public Vec2(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vec2 Translate(Direction direction, int distance)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vec2(this.X, this.Y - distance);
            case Direction.Down:
                return new Vec2(this.X, this.Y + distance);
            case Direction.Left:
                return new Vec2(this.X - distance, this.Y);
            case Direction.Right:
                return new Vec2(this.X + distance, this.Y);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public Vec2 WrapBy(Vec2 dimensions)
    {
        var x = this.X;
        var y = this.Y;
        if (this.X < 0) x = dimensions.X + this.X;
        if (this.X >= dimensions.X) x = this.X - dimensions.X;
        if (this.Y < 0) y = dimensions.Y + this.Y;
        if (this.Y >= dimensions.Y) y = this.Y - dimensions.Y;
        var newPos = new Vec2(x, y);
        if (newPos.Equals(this)) return this;
        return newPos.WrapBy(dimensions);
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X + right.X, left.Y + right.Y);
    }
    
    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X - right.X, left.Y - right.Y);
    }
    
    public static Vec2 operator *(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X * right.X, left.Y * right.Y);
    }
    
    public static Vec2 operator /(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X / right.X, left.Y / right.Y);
    }

    public (int, int) toTuple()
    {
        return (this.X, this.Y);
    }
    
    public bool Equals(Vec2 other)
    {
        return this.X == other.X && this.Y == other.Y;
    }

    public override string ToString()
    {
        return $"({this.X}, {this.Y})";
    }
}
