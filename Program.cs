using System;
using System.Collections.Generic;
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
            Console.SetCursorPosition(15, 15);
            string[] Wall = { "¦", "-", "¯" };
            while (true)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
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
            char[] MapSymbols = { '¦','\'','-','¯'};
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
    }
}

