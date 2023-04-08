using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public abstract class Blocks
    {
        public Point BlockPosition { get; set; }
        public Blocks(Point blockPosition) 
        {
            BlockPosition = blockPosition;
        }
        public abstract void Create(int count);
    }
}
