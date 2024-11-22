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
        static int points = 0;
        static bool PowerPelletactive;
        static ConsoleColor GhostColor = ConsoleColor.Yellow;
        static void Main(string[] args)
        {
            while (Gamerunning)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.CursorVisible = false;
                bool gamestarted=false;
                int Counter = 0;
                int previousY = 17;
                int previousX = 45;
                int xspeed = 0;
                int yspeed = 0;
                int lives = 3;
                char Symbol;
                char Point = '·';
                char PowerPellet = '◯';
                char PacmanMouthClosed = Convert.ToChar(Pacman[0]);
                char PacmanMouthOpen = ' ';
                bool lost = false;
                bool won = false;
                bool Gamepaused = false;
                string PacManSubstitutesDisplay = "⊏";
                int PelletDuration=0;
                ConsoleKeyInfo Key;
                readMap = Map;
                Console.ForegroundColor=ConsoleColor.Yellow;
                Console.Write(GameTitel);
                Console.WriteLine();
                Console.Write("Press [Enter] to start the Game or [Esc] to end the Game");
                Console.WriteLine();
                Console.Write(TitelscreenImage);
                Key = Console.ReadKey(intercept:true);
                Console.Clear();
                if (Key.Key==ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Map);
                    gamestarted = true;
                    Console.SetCursorPosition(0, 27);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int i = 0; i<3; i++)
                    {
                        Console.Write(PacManSubstitutesDisplay);
                    }
                }
                if (Key.Key==ConsoleKey.Escape)
                {
                    Gamerunning=false;
                }
                while (gamestarted)
                {
                    if (!Gamepaused)
                        {
                            Console.SetCursorPosition(0, 24);
                            Console.Write(new string(' ', Console.WindowWidth));
                        }
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
                            case ConsoleKey.Escape:
                                Gamepaused = true;
                                break;
                        }
                        if (Gamepaused)
                        {
                            Console.CursorTop = 24;
                            Console.CursorLeft = 30;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Game paused press [Esc] to Continue");
                            while(Gamepaused)
                            {
                                ConsoleKeyInfo Continue = Console.ReadKey();
                                if (Continue.Key == ConsoleKey.Escape)
                                {
                                    Gamepaused = false;
                                }
                            }
                        }
                    }
                    Console.SetCursorPosition(0, 25);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.Write("Punkte: " + points);
                    Console.WriteLine();
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
                        PowerPelletactive=true;
                        PelletDuration = 20;
                    }
                    if (PowerPelletactive)
                    {
                        GhostColor = ConsoleColor.Blue;
                        PelletDuration--;
                        if(PelletDuration==0)
                        {
                            PowerPelletactive = false;
                        }
                    }
                    else
                    {
                        GhostColor = ConsoleColor.Red;
                    }
                    if (((Math.Abs(previousX - ghostx) <= 1 && previousY == ghosty)|| (previousX == ghostx && Math.Abs(previousY - ghosty) <= 1)) && !PowerPelletactive)
                    {
                        lives -= 1;
                        Console.SetCursorPosition(ghostx, ghosty);
                        Console.Write(' ');
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
                        Console.SetCursorPosition(0, 27);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(0, 27);
                        for (int i = 0; i < lives; i++)
                        {
                            Console.Write(PacManSubstitutesDisplay);
                        }
                    }
                    if (lives == 0)
                    {
                        lost = true;
                        gamestarted = false;
                    }
                    if (points == 150)
                    {
                        won = true;
                        gamestarted = false;
                    }
                }
                if (lost)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Lost);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(GhostforLostscreen);
                    Console.WriteLine();
                    Restart();
                }
                if (won)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Won);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(PacManforWinscreen);
                    Console.WriteLine();
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
¦ '---¦         ¦·¦              ¦·¦  ¦ ¦   ¦ ¦  ¦ '------------' ¦¯¯¯¯¯¦ ¦¯¯¯¯¯¦ '-----¦
¦ ·  ·¦    -----' ¦              ¦ ¦  ¦ ¦   ¦ ¦  ¦·  ·  ·  ·  · · ¦     ¦ ¦     ¦       ¦
¦¯¯¯¦ ¦   ¦ ·    ·¦              ¦·¦  ¦ ¦   ¦ ¦  ¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¦ ¦     ¦ ¦     '¯¯¯¯¯¦·¦
¦   ¦·¦   ¦ ¦¯¯¯¯¯¦--------------' '--' '---' '--' '------------' '-----' ¦     ¦-----' ¦
¦   ¦ '---' ¦     ¦               ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·¦     ¦·  ·  ·¦
¦   ¦·  ·  ·¦     ¦ ¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯'     ¦ ¦¯¯¯¯¯¦
¦   '¯¯¯¯¯¦ ¦     ¦ ¦                                                           ¦·¦     ¦
¦         ¦ '-----' '-----------------------------------------------------------' ¦     ¦
¦         ¦ ·  ·  ·  ·  ·  ·  ·  ·  ◯  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·¦     ¦
¦¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¦";
        static string readMap;
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
                ghostx = 46;
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
                if (PowerPelletactive&&((Math.Abs(pacmanx - ghostx) <= 1 && pacmany == ghosty) || (pacmanx == ghostx && Math.Abs(pacmanx - ghosty) <= 1)))
                {
                    ghostx = 45;
                    ghosty = 2;
                    points += 15;
                    PowerPelletactive = false;
                }
                if(isPacManNearGhost(pacmanx, pacmany)!=0)
                {
                    previousdirection = isPacManNearGhost(pacmanx,pacmany);
                }
                int randomint = random.Next(1, 3);
                switch (previousdirection)
                {
                    case 1: if (!xposwall && !moved) { if (!isThereAWall(previousX, previousY - 1) && randomint == 1) { ghosty -= 1; } else if (!isThereAWall(previousX, previousY + 1) && randomint == 2) { ghosty += 1; } else if (!isThereAWall(previousX + 1, previousY)) { ghostx += 1; moved = true; } } break;
                    case 2: if (!yposwall && !moved) { if (!isThereAWall(previousX - 1, previousY) && randomint == 1) ghostx -= 1; else if (!isThereAWall(previousX + 1, previousY) && randomint == 2) ghostx += 1; else if (!isThereAWall(previousX, previousY + 1)) ghosty += 1; moved = true; } break;
                    case 3: if (!xnegwall && !moved) { if (!isThereAWall(previousX, previousY - 1) && randomint == 1) ghosty -= 1; else if (!isThereAWall(previousX, previousY + 1) && randomint == 2) ghosty += 1; else if (!isThereAWall(previousX - 1, previousY)) ghostx -= 1; moved = true; } break;
                    case 4: if (!ynegwall && !moved) { if (!isThereAWall(previousX - 1, previousY) && randomint == 1) ghostx -= 1; else if (!isThereAWall(previousX + 1, previousY) && randomint == 2) ghostx += 1; else if (!isThereAWall(previousX, previousY - 1)) ghosty -= 1; moved = true; } break;
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
            }

            Console.SetCursorPosition(previousX, previousY);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(previousSymbol(previousX, previousY));
            Console.SetCursorPosition(ghostx, ghosty);
            Console.ForegroundColor = GhostColor;
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
            string newMap = "";
            string[] splitedmap = readMap.Split('\n');
            char[] CurrentLine = splitedmap[y].ToCharArray();
            CurrentLine[x] = ' ';
            splitedmap[y] = new string(CurrentLine);
            for (int i = 0; i < splitedmap.Length; i++)
            {
                newMap += splitedmap[i] + "\n";
            }
            readMap = newMap;
        }
        static string GameTitel = @"
██████╗  █████╗  ██████╗    ███╗   ███╗ █████╗ ███╗   ██╗
██╔══██╗██╔══██╗██╔════╝    ████╗ ████║██╔══██╗████╗  ██║
██████╔╝███████║██║         ██╔████╔██║███████║██╔██╗ ██║
██╔═══╝ ██╔══██║██║         ██║╚██╔╝██║██╔══██║██║╚██╗██║
██║     ██║  ██║╚██████╗    ██║ ╚═╝ ██║██║  ██║██║ ╚████║
╚═╝     ╚═╝  ╚═╝ ╚═════╝    ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝";
        static string TitelscreenImage = @"
⠀⠀⠀⠀⣀⣤⣴⣶⣶⣶⣦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⢿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢀⣾⣿⣿⣿⣿⣿⣿⣿⣅⢀⣽⣿⣿⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀
⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣿⣿⣿⣿⣿⣿⣿⣿⠛⠁⠀⠀⣴⣶⡄⠀⣶⣶⡄⠀⣴⣶⡄
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣀⠀⠙⠋⠁⠀⠉⠋⠁⠀⠙⠋⠀
⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠈⠙⠿⣿⣿⣿⣿⣿⣿⣿⠿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠉⠉⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";
        static string Lost = @"
██╗   ██╗ ██████╗ ██╗   ██╗    ██╗      ██████╗ ███████╗████████╗        ██╗
╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║     ██╔═══██╗██╔════╝╚══██╔══╝    ██╗██╔╝
 ╚████╔╝ ██║   ██║██║   ██║    ██║     ██║   ██║███████╗   ██║       ╚═╝██║ 
  ╚██╔╝  ██║   ██║██║   ██║    ██║     ██║   ██║╚════██║   ██║       ██╗██║ 
   ██║   ╚██████╔╝╚██████╔╝    ███████╗╚██████╔╝███████║   ██║       ╚═╝╚██╗
   ╚═╝    ╚═════╝  ╚═════╝     ╚══════╝ ╚═════╝ ╚══════╝   ╚═╝           ╚═╝
                                                                            
";
        static string GhostforLostscreen = @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀
⠀⠀⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⠀⠀
⠀⠀⣿⣿⣿⣿⡟⠛⠛⠛⠛⣿⣿⣿⣿⣿⣿⣿⣿⠛⠛⠛⠛⢻⣿⣿⣿⣿⠀⠀
⠀⠀⣿⣿⡟⠛⠃⠀⠀⠀⠀⠛⠛⣿⣿⣿⣿⠛⠛⠀⠀⠀⠀⠘⠛⢻⣿⣿⠀⠀
⣶⣶⣿⣿⡇⠀⠀⠀⢸⣿⣷⣶⠀⣿⣿⣿⣿⠀⠀⠀⠀⣿⣿⣶⡆⢸⣿⣿⣶⣶
⣿⣿⣿⣿⡇⠀⠀⠀⢸⣿⠿⠿⠀⣿⣿⣿⣿⠀⠀⠀⠀⣿⣿⠿⠇⢸⣿⣿⣿⣿
⣿⣿⣿⣿⣧⣤⡄⠀⠀⠀⠀⣤⣤⣿⣿⣿⣿⣤⣤⠀⠀⠀⠀⢠⣤⣼⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣧⣤⣤⣤⣤⣿⣿⣿⣿⣿⣿⣿⣿⣤⣤⣤⣤⣼⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⠀⢹⣿⣿⣿⣿⣿⣿⡇⠀⠀⠘⣿⣿⣿⣿⣿⣿⡇⠀⣿⣿⣿⣿⣿
⣿⣿⡏⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⠁⠀⠀⠀⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⢻⣿⣿";
        static string Won = @"
██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗ ██████╗ ███╗   ██╗       ██╗ 
╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██╔═══██╗████╗  ██║    ██╗╚██╗
 ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║   ██║██╔██╗ ██║    ╚═╝ ██║
  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║   ██║██║╚██╗██║    ██╗ ██║
   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝╚██████╔╝██║ ╚████║    ╚═╝██╔╝
   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═══╝       ╚═╝ 
                                                                       
";
        static string PacManforWinscreen = @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣤⣤⣤⣤⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⢀⣠⣶⡾⠿⠛⠋⠉⠉⠉⠉⠉⠙⠛⠿⢷⣦⣄⡀⠀⠀
⠀⠀⠀⠀⠀⣠⣶⠿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⢿⣦⣀
⠀⠀⠀⢠⣾⠟⠁⠀⠀⠀⠀⠀⠀⣴⡿⠿⣶⡀⠀⠀⠀⠀⠀⠀⠀⣠⣿⠟
⠀⠀⣰⡿⠋⠀⠀⠀⠀⠀⠀⠀⠘⣿⣇⣀⣿⠇⠀⠀⠀⠀⢀⣠⣾⠟⠁⠀
⠀⣰⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠛⠋⠀⠀⠀⢀⣴⡿⠛⠁⠀⠀⠀
⢠⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣶⡿⠋⠀⠀⠀⠀⠀⠀
⣸⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀
⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⢿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢹⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀
⠘⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⣷⣄⠀⠀⠀⠀⠀⠀
⠀⠹⣷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣷⣤⡀⠀⠀⠀
⠀⠀⠹⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⢿⣦⡀⠀
⠀⠀⠀⠘⢿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣿⣦
⠀⠀⠀⠀⠀⠙⠿⣶⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⣾⠟⠉
⠀⠀⠀⠀⠀⠀⠀⠈⠙⠿⢷⣶⣤⣄⣀⣀⣀⣀⣀⣠⣤⣶⡾⠟⠋⠁⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠛⠛⠛⠛⠛⠉⠉⠀⠀⠀⠀⠀⠀⠀";
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
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press [Y] to Restart or [N] to end the Game");
            ConsoleKeyInfo input = Console.ReadKey(intercept: true);
            while (true)
            {
                if (input.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    Console.WriteLine(restarting);
                    Console.WriteLine();
                    int cursortop = Console.CursorTop;
                    Console.ForegroundColor=ConsoleColor.Yellow;
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
        static int isPacManNearGhost(int pacmanX, int pacmanY)
        {
            int ghostX = ghostx;
            int ghostY = ghosty;
            int wallcounter;
            int direction = 0;
            if (pacmanX == ghostX)
            {
                if (pacmanY > ghostY)
                {
                    wallcounter = 0;
                    for (int i = pacmanY; i > ghostY; i--)
                    {
                        if (isThereAWall(pacmanX, i))
                        {
                            wallcounter++;
                        }
                    }
                    if (wallcounter == 0)
                    {
                        direction = 4;
                    }
                }
                if (pacmanY < ghostY)
                {
                    wallcounter = 0;
                    for (int i = ghostY; i > pacmanY; i--)
                    {
                        if (isThereAWall(ghostX, i))
                        {
                            wallcounter++;
                        }
                    }
                    if (wallcounter == 0)
                    {
                        direction = 2;
                    }

                }
            }
            else if (pacmanY == ghostY)
            {
                if (pacmanX > ghostX)
                {
                    wallcounter = 0;
                    for (int i = pacmanX; i > ghostX; i--)
                    {
                        if (isThereAWall(i, pacmanY))
                        {
                            wallcounter++;
                        }
                    }
                    if (wallcounter == 0)
                    {
                        direction = 1;
                    }
                }
                if (pacmanX < ghostX)
                {
                    wallcounter = 0;
                    for (int i = ghostX; i > pacmanX; i--)
                    {
                        if (isThereAWall(i, ghostY))
                        {
                            wallcounter++;
                        }
                    }
                    if (wallcounter == 0)
                    {
                        direction = 3;
                    }

                }
            }

            return direction;        
        }
    }
}

