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
        static bool Gamerunning = true;
        static int ghostx;
        static int ghosty;
        static void Main(string[] args)
        {
            while (Gamerunning)
            {
                Console.CursorVisible = false;
                int Counter = 0;
                int previousY = 17;
                int previousX = 45;
                int xspeed = 0;
                int yspeed = 0;
                int lives = 3;
                int points = 0;
                char Symbol;
                char Point = '·';
                char PowerPellet = '◯';
                char PacmanMouthClosed = Convert.ToChar(Pacman[0]);
                char PacmanMouthOpen = ' ';
                bool lost = false;
                bool won = false;
                Console.OutputEncoding = Encoding.UTF8;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Map);
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
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
                    Console.SetCursorPosition(0, 25);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.Write("Punkte: " + points + "/135");
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
                    Thread.Sleep(150);
                    Symbol = previousSymbol(previousX, previousY);
                    if (Symbol == Point)
                    {
                        points++;
                        changeMapSymbols(previousX, previousY);
                    }
                    if (Symbol == PowerPellet)
                    {
                        changeMapSymbols(previousX, previousY);
                    }
                    if (previousX == ghostx && previousY == ghosty)
                    {
                        lives -= 1;
                        Console.SetCursorPosition(previousX, previousY);
                        Console.Write("⊔");
                        Thread.Sleep(100);
                        Console.SetCursorPosition(previousX, previousY);
                        Console.Write('_');
                        Thread.Sleep(100);
                        Console.SetCursorPosition(previousX, previousY);
                        Console.Write(' ');
                        Thread.Sleep(500);
                        previousX = 45;
                        previousY = 17;
                        ghostx = 45;
                        ghosty = 1;
                    }
                    if (lives == 0)
                    {
                        lost = true;
                        break;
                    }
                }
                if (lost)
                {
                    Console.Clear();
                    Console.WriteLine(Lost);
                    Restart();
                }
            }
        }
        static string[] Pacman = { "●", "⊏", "⊐", "⊓", "⊔" };
    

static string Map = @"¦---------------------------------------------------------------------------------------¦
¦ ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·   ·                                          ¦
¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦·¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦
¦ '-----------------------------------------' '---------------------------------------' ¦
¦ ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ◯  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·   ·      ¦
¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦¯¯¯¯¯¦
¦                 ¦·¦                           ¦ ¦                             ¦·¦     ¦
¦-----------------' '---------------------------' '-----------------------------' ¦     ¦
¦◯  ·  ·  ·  ·  ·  ·                               ◯  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·¦     ¦
¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦     ¦
¦·¦             ¦ '--------------' ¦  ¦-------¦  ¦·¦            ¦·¦             ¦ ¦     ¦
¦ ¦             ¦◯  ·  ·  ·  · ·  ·¦  ¦       ¦  ¦ ¦            ¦ '-------------' ¦     ¦
¦·¦             ¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦  ¦ ¦¯¯¯¦ ¦  ¦·¦            ¦·  ·  ·  ·  ·    ¦     ¦
¦ '---¦         ¦·¦              ¦·¦  ¦ ¦   ¦ ¦  ¦ '------------' ¦¯¯¯¯¯¦ ¦¯¯¯¯¯¦ ¦-----¦
¦ ·  ·¦    -----' ¦              ¦ ¦  ¦ ¦   ¦ ¦  ¦·  ·  ·  ·  · · ¦     ¦ ¦     ¦       ¦
¦¯¯¯¦ ¦   ¦ ·    ·¦              ¦·¦  ¦ ¦   ¦ ¦  ¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦     ¦ ¦     '¯¯¯¯¯¦·¦
¦   ¦·¦   ¦ ¦¯¯¯¯¯¦--------------' '--' '---' '--' '------------' '-----' ¦     ¦-----' ¦
¦   ¦ '---' ¦     ¦               ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·¦     ¦·  ·  ·¦
¦   ¦·  ·  ·¦     ¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯'     ¦ ¦¯¯¯¯¯¦
¦   '¯¯¯¯¯¦ ¦     ¦ ¦                                                           ¦·¦     ¦
¦         ¦ '-----' '-----------------------------------------------------------' ¦     ¦
¦         ¦ ·  ·  ·  ·  ·  ·  ·  ·  ◯  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·¦     ¦
¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦";
        static string readMap = Map;
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
            string Ghost = "Δ";

            if (Counter == 1)
            {
                ghostx = 45;
                ghosty = 2;
            }
            bool xposwall = isThereAWall(ghostx + 1, ghosty);
            bool yposwall = isThereAWall(ghostx, ghosty + 1);

            bool xnegwall = isThereAWall(ghostx, ghosty);
            bool ynegwall;
            try
            {
                ynegwall = isThereAWall(ghostx, ghosty - 1);
            }
            catch { ynegwall = true; }
            bool moved = false;
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
                int randomint = random.Next(1,3);
                switch (previousdirection)
                {
                    case 1: if (!xposwall && !moved) { if (!isThereAWall(previousX, previousY - 1) && randomint == 1) { ghosty -= 1; } else if (!isThereAWall(previousX, previousY + 1) && randomint == 2) { ghosty += 1; } else if (!isThereAWall(previousX + 1, previousY)) { ghostx += 1; moved = true; } } break;
                    case 2: if (!yposwall && !moved) { if (!isThereAWall(previousX -1, previousY) && randomint==1) ghostx -= 1; else if (!isThereAWall(previousX +1,previousY) && randomint == 2) ghostx += 1; else if (!isThereAWall(previousX, previousY + 1)) ghosty += 1; moved = true; } break;
                    case 3: if (!xnegwall && !moved) { if (!isThereAWall(previousX, previousY - 1) && randomint == 1) ghosty -= 1; else if (!isThereAWall(previousX, previousY + 1) && randomint==2) ghosty += 1; else if (!isThereAWall(previousX -1, previousY)) ghostx -= 1; moved = true; } break;
                    case 4: if (!ynegwall && !moved) { if (!isThereAWall(previousX -1, previousY) && randomint == 1) ghostx -= 1; else if (!isThereAWall(previousX+1, previousY) && randomint == 2) ghostx += 1; else if (!isThereAWall(previousX, previousY - 1)) ghosty -= 1; moved = true; } break;
                }
            }
            if (!moved)
            {
                int randomDirection = random.Next(1, 5);

                switch (randomDirection)
                {
                    case 1:
                        if (!xposwall && !isThereAWall(ghostx + 1, ghosty))
                        {
                            ghostx = ghostx + 1;
                        }
                        break;

                    case 2:
                        if (!yposwall && !isThereAWall(ghostx, ghosty + 1))
                            ghosty += 1;
                        else { ghosty = previousY; }
                        break;

                    case 3:
                        if (!xnegwall && !isThereAWall(ghostx - 1, ghosty))
                            ghostx -= 1;
                        else { ghostx = previousX; }
                        break;

                    case 4:
                        if (!ynegwall && !isThereAWall(ghostx, ghosty - 1))
                            ghosty -= 1;
                        else { ghosty = previousY; }
                        break;
                }
            }
            Console.SetCursorPosition(previousX, previousY);
            Console.ForegroundColor = ConsoleColor.Blue;
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
            string[] splitedmap = readMap.Split('\n');
            char[] CurrentLine = splitedmap[y].ToCharArray();
            char previouschar = CurrentLine[x];
            return previouschar;
        }
            static int Distance(int ghostx, int ghosty, int pacmanx, int pacmany)
        {
            int xdistance = 0;
            return xdistance;
        }
        static void changeMapSymbols(int x, int y)
        {
            string newMap="";
            string[] splitedmap = readMap.Split('\n');
            char[] CurrentLine = splitedmap[y].ToCharArray();
            CurrentLine[x] = ' ';
            splitedmap[y] = new string(CurrentLine);
            for (int i = 0; i < splitedmap.Length; i++)
            {
                newMap += splitedmap[i]+ "\n";
            }
            readMap=newMap;
        }
        static string GameTitel = @"
██████╗  █████╗  ██████╗    ███╗   ███╗ █████╗ ███╗   ██╗
██╔══██╗██╔══██╗██╔════╝    ████╗ ████║██╔══██╗████╗  ██║
██████╔╝███████║██║         ██╔████╔██║███████║██╔██╗ ██║
██╔═══╝ ██╔══██║██║         ██║╚██╔╝██║██╔══██║██║╚██╗██║
██║     ██║  ██║╚██████╗    ██║ ╚═╝ ██║██║  ██║██║ ╚████║
╚═╝     ╚═╝  ╚═╝ ╚═════╝    ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝";
        static string Lost = @"
 __   __            _              _        __
 \ \ / /__  _   _  | |    ___  ___| |_   _ / /
  \ V / _ \| | | | | |   / _ \/ __| __| (_) | 
   | | (_) | |_| | | |__| (_) \__ \ |_   _| | 
   |_|\___/ \__,_| |_____\___/|___/\__| (_) | 
                                           \_\";
        static string restarting = @"
                _             _   _             
  _ __ ___  ___| |_ __ _ _ __| |_(_)_ __   __ _ 
 | '__/ _ \/ __| __/ _` | '__| __| | '_ \ / _` |
 | | |  __/\__ \ || (_| | |  | |_| | | | | (_| |
 |_|  \___||___/\__\__,_|_|   \__|_|_| |_|\__, |
                                          |___/ ";
        static string[] Loading = { @"
        
        
  _____ 
 |_____|
        
        
", @"
              
              
  _____ _____ 
 |_____|_____|
              
              
", @"
                    
                    
  _____ _____ _____ 
 |_____|_____|_____|
                    
                    
", @"
                          
                          
  _____ _____ _____ _____ 
 |_____|_____|_____|_____|
                          
                          
" };
        static void Restart()
        {
            Console.WriteLine();
            Console.Write("Do you want to restart the Game? [Y/N]: ");
            ConsoleKeyInfo input = Console.ReadKey(intercept: true);
            while (true)
            {
                if (input.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    Console.WriteLine(restarting);
                    int cursortop = Console.CursorTop;
                    for (int i = 0; i < Loading.Length; i++)
                    {
                        Console.CursorTop = cursortop;
                        Console.Write(Loading[i]);
                        Thread.Sleep(200);
                        Console.Clear();
                    }
                    break;
                }
                if (input.Key == ConsoleKey.N)
                {
                    Gamerunning = false;
                    break;
                }
            }
        }
    }
}

