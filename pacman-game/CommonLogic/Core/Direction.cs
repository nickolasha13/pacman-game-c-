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
}