using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace oop_lab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DrawOutborders d= new DrawOutborders('#', new Point(1,1), 10, 10);
            d.Draw();
            DrawInborders fill = new DrawInborders('@', new Point(5, 5), 3, 3);
            fill.Draw();
        }
    }
}