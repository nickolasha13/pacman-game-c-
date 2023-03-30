using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class DrawInborders
    {
        public char Symbol { get; set; }
        public Point SetPointPosition { get; set; }
        public int InBorderWidth { get; set; }
        public int InBorderHeight { get; set; }
        public DrawInborders(char symbol, Point setpointposition, int inBorderWidth, int inBorderHeight)
        {
            Symbol = symbol;
            SetPointPosition = setpointposition;
            InBorderWidth = inBorderWidth;
            InBorderHeight = inBorderHeight;
        }
        public void Horizontal(Point start)
        {
            Console.SetCursorPosition(start.X, start.Y);
            for (int i = 0; i < InBorderWidth; i++)
            {
                Console.Write(Symbol);
            }
        }
        public void Vertical(Point start)
        {
            int StartY = start.Y;
            for (int i = 0; i < InBorderHeight; i++, StartY++)
            {
                Console.SetCursorPosition(start.X, StartY);
                Console.Write(Symbol);
            }
        }
        public void Draw()
        {
            int StartY = SetPointPosition.Y;
            for (int i = 0; i < InBorderHeight; i++, StartY++)
            {
                Console.SetCursorPosition(SetPointPosition.X, StartY);
                for (int j = 0; j < InBorderWidth; j++)
                {
                    Console.Write(Symbol);
                }
            }
        }
    }
}
