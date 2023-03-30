using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab
{
    public interface IDraw
    {
        public char Symbol { get; set; }
        public Point SetPointPosition { get; set; }
        public int BorderWidth { get; set; }
        public int BorderHeight { get; set; }
        public void Draw()
        {

        }
    }
}
