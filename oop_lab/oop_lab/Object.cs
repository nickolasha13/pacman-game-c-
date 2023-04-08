using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public abstract class Object
    {
        public Point Position { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }
        public void PrintObject() 
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.ForegroundColor= Color;
            Console.Write(Symbol);
        }
        public void EraseObject()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(" ");
        }
        public Object(Point position, char symbol, ConsoleColor color)
        {
            Position = position;
            Symbol = symbol;
            Color = color;
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
    }
}
