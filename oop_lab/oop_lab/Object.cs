using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Object
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
        public Object(Point position, char symbol, ConsoleColor color)
        {
            Position = position;
            Symbol = symbol;
            Color = color;
        }
    }
}
