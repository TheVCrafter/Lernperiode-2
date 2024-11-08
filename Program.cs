using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PacMan
{
    internal class Program
    {
        static int ghostx = 45;
        static int ghosty = 1;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int Counter = 0;
            int previousY = 17;
            int previousX = 45;
            int xspeed = 0;
            int yspeed = 0;
            Thread.Sleep(50);
            char PacmanMouthClosed = Convert.ToChar(Pacman[0]);
            char PacmanMouthOpen = ' ';
            Console.WriteLine(Map);
            while (true)
            {
                Console.OutputEncoding = Encoding.UTF8;
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: false);

                    // Aktion basierend auf der gedrückten Taste
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[1]);
                            xspeed = 1;
                            yspeed = 0;
                            break;
                        case ConsoleKey.LeftArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[2]);
                            xspeed = -1;
                            yspeed = 0;
                            break;
                        case ConsoleKey.UpArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[4]);
                            xspeed = 0;
                            yspeed = -1;
                            break;
                        case ConsoleKey.DownArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[3]);
                            xspeed = 0;
                            yspeed = 1;
                            break;
                    }
                }
                Counter++;
                if (isThereAWall(previousX + xspeed, previousY + yspeed) == false)
                {
                    Console.SetCursorPosition(previousX, previousY);
                    Console.Write(' ');
                    Console.SetCursorPosition(previousX + xspeed, previousY + yspeed);
                    previousX = previousX + xspeed;
                    previousY = previousY + yspeed;
                }
                else
                {
                    Console.SetCursorPosition(previousX, previousY);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (Counter % 2 == 0)
                {
                    Console.Write(PacmanMouthClosed);
                }
                else if (Counter % 2 != 0)
                {
                    Console.Write(PacmanMouthOpen);
                }
                Ghosts(Counter, previousX, previousY);
                Thread.Sleep(300);
            }
        }
        static string[] Pacman = { "●", "⊏", "⊐", "⊓", "⊔" };
    

static string Map = @" -----------------------------------------------------------------------------------------------
¦ ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·    ·                                              ¦
¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦
¦   '------------------------------------------'   '----------------------------------------'   ¦
¦ ·  ·  ·  ·  ·  ·  ·                            ·  ◯  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  · ¦
¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦
¦                 ¦ · ¦                           ¦   ¦                                     ¦ · ¦
¦-----------------'   '---------------------------'   ¦    ---------------------------------'   ¦
¦ ·  ·  ·  ·  ·  ·  ·                                 ¦    ¦ ◯  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  · ¦
¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯     ¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦
¦ · ¦             ¦   '--------------'   ¦   -----------   ¦ · ¦            ¦ · ¦               ¦
¦   ¦             ¦ ◯   ·  ·  ·  ·  ·  · ¦  ¦           ¦  ¦   ¦            ¦   '---------------¦ 
¦ · ¦             ¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦  ¦   ¦¯¯¯¦   ¦  ¦ · ¦            ¦ ·   ·  ·  ·  ·    ¦
¦   '---¦         ¦ · ¦              ¦ · ¦  ¦   ¦   ¦   ¦  ¦   ¦------------'   ¦¯¯¯¯¯¯¯¯¯¯¯¦ · ¦
¦ ·   · ¦    -----'   ¦              ¦   ¦  ¦   ¦   ¦   ¦  ¦ ·  ·  ·  ·  ·    · ¦           ¦   ¦
¦¯¯¯¦   ¦   ¦ ·     · ¦              ¦ · ¦  ¦   ¦   ¦   ¦  ¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¦   ¦           ¦ · ¦
¦   ¦ · ¦   ¦   ¦¯¯¯¯¯¦--------------'   '--'   '---'   '--'   '------------'   ¦     ¦-----'   ¦
¦   ¦   '---'   ¦     ¦                ·  ·  ·  ·  ·  ·  ·   ·   ·  ·  ·  ·   · ¦     ¦   ·   · ¦
¦   ¦ ·   ·   · ¦     ¦   ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯'     ¦ · ¦¯¯¯¯¯¦
¦   '¯¯¯¯¯¯¯¦   ¦     ¦   ¦                                                           ¦   ¦     ¦
¦           ¦   '¯¯¯¯¯'   '-----------------------------------------------------------' · ¦     ¦
¦           ¦ ·  ·  ·   ·    ·  ·  ·  ·  ◯  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·   ¦     ¦
 ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
        static bool isThereAWall(int currentX, int currentY)
        {
            bool TouchingWall = false;
            string[] splitedmap = Map.Split('\n');
            char[] MapSymbols = { '¦', '\'', '-', '¯' };
            char[] CurrentLine = splitedmap[currentY].ToCharArray();
            for (int i = 0; i < MapSymbols.Length; i++)
            {
                if (CurrentLine[currentX] == MapSymbols[i])
                {
                    TouchingWall = true;
                }
            }
            return TouchingWall;

        }
        static int previousdirection = 0;
        static void Ghosts(int Counter, int pacmanx, int pacmany)
        {
            int previousX = ghostx;
            int previousY = ghosty;
            Random random = new Random();
            Console.OutputEncoding = Encoding.UTF8;
            string Ghost = "👻";

            if (Counter == 1)
            {
                ghostx = 45;
                ghosty = 1;
                Console.SetCursorPosition(ghostx, ghosty);
                Console.Write(Ghost);
            }
            bool xposwall = isThereAWall(ghostx + 2, ghosty);
            bool yposwall = isThereAWall(ghostx, ghosty + 1);
            bool xnegwall = isThereAWall(ghostx - 2, ghosty);
            bool ynegwall = isThereAWall(ghostx, ghosty - 1);
            bool bewegt = false;
            if (Counter == 1)
            {
                int randomDirection = random.Next(2);
                if (randomDirection == 0)
                {
                    randomDirection = -1;
                }
                ghostx += randomDirection;
            }
            else
            {
                switch (previousdirection)
                {
                    case 1: if (!xposwall && !bewegt) { if (!isThereAWall(ghostx, ghosty - 1) && Counter % 2 == 0) ghosty -= 1; else if (!isThereAWall(ghostx, ghosty + 1) && Counter % 2 == 0) ghosty += 1; else if (!isThereAWall(ghostx + 2, ghosty) && Counter % 2 == 0) ghostx += 1; bewegt = true; } break;
                    case 2: if (!yposwall && !bewegt) { if (!isThereAWall(ghostx - 2, ghosty) && Counter % 2 == 0) ghostx -= 1; else if (!isThereAWall(ghostx + 2, ghosty) && Counter % 2 == 0) ghostx += 1; else if (!isThereAWall(ghostx, ghosty + 1) && Counter % 2 == 0) ghosty += 1; bewegt = true; } break;
                    case 3: if (!xnegwall && !bewegt) { if (!isThereAWall(ghostx, ghosty - 1) && Counter % 2 == 0) ghosty -= 1; else if (!isThereAWall(ghostx, ghosty + 1) && Counter % 2 == 0) ghosty += 1; else if (!isThereAWall(ghostx - 2, ghosty) && Counter % 2 == 0) ghostx -= 1; bewegt = true; } break;
                    case 4: if (!ynegwall && !bewegt) { if (!isThereAWall(ghostx - 2, ghosty) && Counter % 2 == 0) ghostx -= 1; else if (!isThereAWall(ghostx + 2, ghosty) && Counter % 2 == 0) ghostx += 1; else if (!isThereAWall(ghostx, ghosty - 1) && Counter % 2 == 0) ghosty -= 1; bewegt = true; } break;
                }
            }
            if (!bewegt)
            {
                int randomDirection = random.Next(1, 5);

                switch (randomDirection)
                {
                    case 1:
                        if (!xposwall && !isThereAWall(ghostx + 2, ghosty))
                            ghostx += 1;
                        break;

                    case 2:
                        if (!yposwall && !isThereAWall(ghostx, ghosty + 1))
                            ghosty += 1;
                        break;

                    case 3:
                        if (!xnegwall && !isThereAWall(ghostx - 2, ghosty))
                            ghostx -= 1;
                        break;

                    case 4:
                        if (!ynegwall && !isThereAWall(ghostx, ghosty - 1))
                            ghosty -= 1;
                        break;
                }
            }

            Console.SetCursorPosition(previousX, previousY);
                Console.Write(previousSymbol(previousX, previousY));
                Console.SetCursorPosition(ghostx, ghosty);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Ghost);
                if (ghostx > previousX) previousdirection = 1;
                if (ghosty > previousY) previousdirection = 2;
                if (ghostx < previousX) previousdirection = 3;
                if (ghosty < previousY) previousdirection = 4;
        }
            static char previousSymbol(int x, int y)
        {
            string[] splitedmap = Map.Split('\n');
            char[] CurrentLine = splitedmap[y].ToCharArray();
            char previouschar = CurrentLine[x];
            return previouschar;
        }
    }
}

