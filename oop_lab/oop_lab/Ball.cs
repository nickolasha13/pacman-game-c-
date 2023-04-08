using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Ball : Object
    {
        private readonly Tokens token;
        public const bool IsEnemy = false;
        public Ball(Point position, char symbol, ConsoleColor color, Tokens token): base(position,symbol,color) 
        { 
            this.token = token;
        }
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
                    token.TokenList.Remove(Position);
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
                    token.TokenList.Remove(Position);
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
                    token.TokenList.Remove(Position);
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
                    token.TokenList.Remove(Position);
                }
            }
            PrintObject();
            Program.End();
        }

        public bool TokenCollision()
        {
            foreach (var token in token.TokenList)
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
