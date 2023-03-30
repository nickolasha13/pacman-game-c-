using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class DrawOutborders
    {
        public static List <Point> borderbox = new List <Point> ();
        public char Symbol { get; set; }
        public Point SetPointPosition { get; set; }
        public int BorderWidth { get; set; }
        public int BorderHeight { get; set; }
        public DrawOutborders(char symbol, Point setpointposition, int borderWidth, int borderHeight)
        {
            Symbol = symbol;
            SetPointPosition = setpointposition;
            BorderWidth = borderWidth;
            BorderHeight = borderHeight;
        }
        public void Horizontal(Point start)
        {
            Console.SetCursorPosition(start.X, start.Y);
            for (int i = 0; i < BorderWidth; i++)
            {
                Console.Write(Symbol);
                borderbox.Add(start);
            }
        }
        public void Vertical(Point start)
        {
            int StartY= start.Y;
            for (int i = 0; i < BorderHeight; i++, StartY++)
            {
                Console.SetCursorPosition(start.X, StartY);
                Console.Write(Symbol);
                borderbox.Add(start);
            }
        }
        public void Draw()
        {
            Horizontal(SetPointPosition);
            Horizontal(new Point(SetPointPosition.X, SetPointPosition.Y + BorderHeight - 1));
            Vertical(SetPointPosition);
            Vertical(new Point(SetPointPosition.X + BorderWidth, SetPointPosition.Y));
        }
    }
}
