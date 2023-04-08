﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Tokens : Blocks
    {
        private readonly DrawOutborders drawOutborders;
        public char Symbol { get; set; }
        public List<Point> TokenList = new List<Point>();
        public Tokens(Point blockPosition, char symbol, DrawOutborders drawOutborders) : base(blockPosition) 
        {
            Symbol = symbol;
            this.drawOutborders = drawOutborders;
        }

        public override void Create(int count)
        {
            Point positionField = drawOutborders.BlockPosition;
            int width = drawOutborders.BorderWidth;
            int height = drawOutborders.BorderHeight;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                Point newToken = new Point(random.Next(positionField.X + 1, positionField.X + width - 2),
                    random.Next(positionField.Y + 1, positionField.Y + height - 2));
                if (!TokenList.Contains(newToken))
                {
                    TokenList.Add(newToken);
                    Console.SetCursorPosition(TokenList[TokenList.Count - 1].X, TokenList[TokenList.Count - 1].Y);
                    Console.Write(Symbol);
                }

            }
        }
    }
}
