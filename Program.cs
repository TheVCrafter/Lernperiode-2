using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cursorleft = -1;
            int cursortop = 0;
            int cursorleftsave = 0;
            int cursortopsave = 0;
            int Counter = 0;
            Thread.Sleep(50);
            char PacmanMouthClosed = Convert.ToChar(Pacman[0]);
            char PacmanMouthOpen = ' ';
            while (true)
            {
                Console.CursorLeft = cursorleftsave;
                Console.CursorTop = cursortopsave;
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    // Aktion basierend auf der gedrückten Taste
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[1]);
                            cursorleft = 0;
                            cursortop = 0;
                            break;
                        case ConsoleKey.LeftArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[2]);
                            cursorleft = -2;
                            cursortop = 0;
                            break;
                        case ConsoleKey.UpArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[4]);
                            cursorleft = -1;
                            cursortop = -1;
                            break;
                        case ConsoleKey.DownArrow:
                            PacmanMouthOpen = Convert.ToChar(Pacman[3]);
                            cursorleft = -1;
                            cursortop = 1;
                            break;
                    }
                }
                if (Counter % 2 == 0)
                {
                    Console.Write(PacmanMouthClosed);
                }
                else if (Counter % 2 != 0)
                {
                    Console.Write(PacmanMouthOpen);
                }
                Counter++;
                Thread.Sleep(200);
                cursorleftsave = Console.CursorLeft + cursorleft;
                cursortopsave = Console.CursorTop + cursortop;
                Console.Clear();
            }
        }
        static string[] Pacman = {"□","⊏","⊐","⊓","⊔"};
    }
}
