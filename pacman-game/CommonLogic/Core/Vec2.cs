namespace CommonLogic.Core;

public struct Vec2 : IEquatable<Vec2>
{
    public int X;
    public int Y;

    public Vec2(int x, int y)
    {
        X = x;
        Y = y;
    }

    private static readonly Dictionary<Direction, Func<Vec2, int, Vec2>> _translate = new();

    static Vec2()
    {
        _translate.Add(Direction.Up, (vec, distance) => new Vec2(vec.X, vec.Y - distance));
        _translate.Add(Direction.Down, (vec, distance) => new Vec2(vec.X, vec.Y + distance));
        _translate.Add(Direction.Left, (vec, distance) => new Vec2(vec.X - distance, vec.Y));
        _translate.Add(Direction.Right, (vec, distance) => new Vec2(vec.X + distance, vec.Y));
    }

    public Vec2 Translate(Direction direction, int distance)
    {
        return _translate[direction](this, distance);
    }

    public Vec2 WrapBy(Vec2 dimensions)
    {
        var x = X;
        var y = Y;
        if (X < 0)
            x = dimensions.X + X;
        if (X >= dimensions.X)
            x = X - dimensions.X;
        if (Y < 0)
            y = dimensions.Y + Y;
        if (Y >= dimensions.Y)
            y = Y - dimensions.Y;
        var newPos = new Vec2(x, y);
        if (newPos.Equals(this))
            return this;
        return newPos.WrapBy(dimensions);
    }

    public Vec2 TranslateWrapped(Direction direction, int distance, Vec2 dimensions)
    {
        return Translate(direction, distance).WrapBy(dimensions);
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

    public bool Equals(Vec2 other)
    {
        return X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}