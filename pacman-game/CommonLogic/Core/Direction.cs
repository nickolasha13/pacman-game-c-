namespace CommonLogic.Core;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class DirectionHelper
{
    public static Direction GetOpposite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
    
    public static bool IsOpposite(Direction direction1, Direction direction2)
    {
        return GetOpposite(direction1) == direction2;
    }
    
    public static bool IsVertical(Direction direction)
    {
        return direction == Direction.Up || direction == Direction.Down;
    }
    
    public static bool IsHorizontal(Direction direction)
    {
        return direction == Direction.Left || direction == Direction.Right;
    }
}