using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public class Ball : Object
    {
        public const bool IsEnemy = false;
        public Ball(Point position, char symbol, ConsoleColor color): base(position,symbol,color) { }

    }
}
