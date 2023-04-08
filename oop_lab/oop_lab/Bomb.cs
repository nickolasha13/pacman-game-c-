using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Bomb : Blocks
    {
        private readonly DrawOutborders drawOutborders;
        private readonly Tokens token;
        public char Symbol { get; set; }
        public List<Point> BombList = new List<Point>();
        public Bomb(Point blockPosition) : base(blockPosition)
        {

        }
        public Bomb(Point blockPosition, char symbol, DrawOutborders drawOutborders, Tokens token) : base(blockPosition)
        {
            Symbol = symbol;
            this.drawOutborders = drawOutborders; 
            this.token = token;
        }
        
        public override void Create(int count)
        {
            Point positionField = drawOutborders.BlockPosition;
            int width = drawOutborders.BorderWidth;
            int height = drawOutborders.BorderHeight;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                Point newBomb = new Point(random.Next(positionField.X + 1, positionField.X + width - 2),
                    random.Next(positionField.Y + 1, positionField.Y + height - 2));
                if (!BombList.Contains(newBomb) && !token.TokenList.Contains(newBomb)) 
                {
                    BombList.Add(newBomb);
                    Console.SetCursorPosition(BombList[BombList.Count - 1].X, BombList[BombList.Count - 1].Y);
                    Console.Write(Symbol);
                }

            }
        }
    }
}
