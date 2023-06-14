namespace CommonLogic.Core;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public abstract class DirectionHelper
{
    public static Direction GetOpposite(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    // public static bool IsOpposite(Direction direction1, Direction direction2)
    // {
    //     return GetOpposite(direction1) == direction2;
    // }
    //
    // public static bool IsVertical(Direction direction)
    // {
    //     return direction == Direction.Up || direction == Direction.Down;
    // }
    //
    // public static bool IsHorizontal(Direction direction)
    // {
    //     return direction == Direction.Left || direction == Direction.Right;
    // }
}