﻿using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace oop_lab
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.SetWindowSize(51, 51);
            Console.CursorVisible= false;
            DrawOutborders d= new DrawOutborders('#', new Point(1,1), 40, 20);
            d.Draw();
            //DrawInborders fill = new DrawInborders('@', new Point(5, 5), 3, 3);
            //fill.Draw();
            Tokens token = new Tokens(new Point(0, 0), '@', d);
            token.Create(30);
            Ball ball= new Ball(new Point(4,4), 'o', ConsoleColor.Yellow, token);
            ball.PrintObject();
            int Score = 0;
            Bomb bomb = new Bomb(new Point(0, 0), '*', d, token);
            bomb.Create(10);
            Point scorePoint = new Point(45, 10);
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                    if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
                    {
                        ball.BallMovement(Direction.right, ref Score);
                    }
                    if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        ball.BallMovement(Direction.left, ref Score);
                    }
                    if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
                    {
                        ball.BallMovement(Direction.up, ref Score);
                    }
                    if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
                    {
                        ball.BallMovement(Direction.down, ref Score);
                    }
                }
                SetScore(Score, scorePoint);
            }
            End();
        }
        public static void End()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SetScore(int Score, Point point)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write(Score);
        }
        public enum Direction 
        { 
            down, up, left, right
        }
    } 

}