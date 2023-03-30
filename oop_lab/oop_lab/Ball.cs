using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Ball : Object
    {
        public const bool IsEnemy = false;
        public Ball(Point position, char symbol, ConsoleColor color): base(position,symbol,color) { }
        public void BallMovement(Program.Direction direction, ref Ball ball)
        {
            ball.EraseObject();
            if (Program.Direction.up == direction)
            {
                ball.Position.Y--;
            }
            if (Program.Direction.down == direction)
            {
                ball.Position.Y++;
            }
            if (Program.Direction.left == direction)
            {
                ball.Position.X--;
            }
            if (Program.Direction.right == direction)
            {
                ball.Position.X++;
            }
            ball.PrintObject();
        }
    }
}
