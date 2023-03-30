using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Tokens
    {
        public char Symbol { get; set; }
        public static List<Point> TokenList = new List<Point>();

        public Tokens(char symbol)
        {
            Symbol = symbol;
        }

        public void CreateToken(Point positionField, int width, int height, int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                TokenList.Add(new Point(random.Next(positionField.X + 1, positionField.X + width - 2), 
                    random.Next(positionField.Y + 1, positionField.Y + height - 2)));
                Console.SetCursorPosition(TokenList[TokenList.Count - 1].X, TokenList[TokenList.Count - 1].Y);
                Console.Write(Symbol);
            }
        }
    }
}
