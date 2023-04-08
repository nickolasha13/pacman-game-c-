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
        public Point BlockPosition { get; set; }
        public int BorderWidth { get; set; }
        public int BorderHeight { get; set; }
        public DrawOutborders(char symbol, Point blockPosition, int borderWidth, int borderHeight)
        {
            Symbol = symbol;
            BlockPosition = blockPosition;
            BorderWidth = borderWidth;
            BorderHeight = borderHeight;
        }
        public void Horizontal(Point start)
        {
            int startX = start.X;
            for (int i = 0; i < BorderWidth; i++, startX++)
            {
                Console.SetCursorPosition(startX, start.Y);
                Console.Write(Symbol);
                borderbox.Add(new Point(startX,start.Y));
            }
        }
        public void Vertical(Point start)
        {
            int StartY= start.Y;
            for (int i = 0; i < BorderHeight; i++, StartY++)
            {
                Console.SetCursorPosition(start.X, StartY);
                Console.Write(Symbol);
                borderbox.Add(new Point(start.X, StartY));
            }
        }
        public void Draw()
        {
            Horizontal(BlockPosition);
            Horizontal(new Point(BlockPosition.X, BlockPosition.Y + BorderHeight - 1));
            Vertical(BlockPosition);
            Vertical(new Point(BlockPosition.X + BorderWidth, BlockPosition.Y));
        }
    }
}
