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
        public void BallMovement(Program.Direction direction, ref int Score)
        {
            EraseObject();
            if (Program.Direction.up == direction)
            {
                Position.Y--;
                if (Collision())
                {
                    Position.Y++;
                }
                else if (TokenCollision()) 
                {
                    Score++;
                }
            }
            if (Program.Direction.down == direction)
            {
                Position.Y++;
                if (Collision())
                {
                    Position.Y--;
                }
                else if (TokenCollision())
                {
                    Score++;
                }
            }
            if (Program.Direction.left == direction)
            {
                Position.X--;
                if (Collision())
                {
                    Position.X++;
                }
                else if (TokenCollision())
                {
                    Score++;
                }
            }
            if (Program.Direction.right == direction)
            {
                Position.X++;
                if (Collision())
                {
                    Position.X--;
                }
                else if (TokenCollision())
                {
                    Score++;
                }
            }
            PrintObject();
        }
        public bool Collision()
        {
            List<Point> blocks = DrawOutborders.borderbox.Union(DrawInborders.extraborders).ToList();
            foreach (var block in blocks)
            {
                if (block.Equals(Position)) 
                {
                    return true;
                }
            }
            return false;
        }
        public bool TokenCollision()
        {
            foreach (var token in Tokens.TokenList)
            {
                if (token.Equals(Position))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
