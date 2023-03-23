using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Enemy: Object
    {
        public const bool IsEnemy = true;
        public Enemy(Point position, char symbol, ConsoleColor color) : base(position, symbol, color) { }
    }
}
