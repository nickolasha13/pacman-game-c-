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
            Console.CursorVisible= false;
            DrawOutborders d= new DrawOutborders('#', new Point(1,1), 10, 10);
            d.Draw();
            DrawInborders fill = new DrawInborders('@', new Point(5, 5), 3, 3);
            fill.Draw();
            Ball ball= new Ball(new Point(4,4), 'o', ConsoleColor.Magenta );
            ball.PrintObject();
            End();
        }
        public static void End()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(20, 20);
        }
    } 

}