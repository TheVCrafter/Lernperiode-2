﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Media;
using System.Collections;
using System.Security.Policy;
using System.Net.Http.Headers;
using System.IO;

namespace PacMan
{
    internal class Program
    {
        static bool Gamerunning = true;
        static bool gamestarted = false;
        static int ghostx;
        static int ghosty;
        static ConsoleColor GhostColor = ConsoleColor.Yellow;
        static string[] Pacman = { "●", "⊏", "⊐", "⊓", "⊔" };
        static string PacManSubstitutesDisplay = "⊏";
        static int points;
        static bool PowerPelletactive;
        static int Counter;
        static int pacmanY;
        static int pacmanX;
        static int xspeed;
        static int yspeed;
        static int lives;
        static char Symbol;
        static char Point = '·';
        static char PowerPellet = '◯';
        static char PacmanMouthClosed = Convert.ToChar(Pacman[0]);
        static char PacmanMouthOpen = ' ';
        static bool lost = false;
        static bool won = false;
        static bool Gamepaused = false;
        static int PelletDuration = 0;
        static int Sirenstage = 0;
        static SoundPlayer GameMusic = new SoundPlayer("intermission.wav");
        static SoundPlayer GameStart = new SoundPlayer("start.wav");
        static string[] Sirensounds = { "siren0_firstloop.wav", "siren1_firstloop.wav", "siren2_firstloop.wav", "siren3_firstloop.wav", "siren4_firstloop.wav" };
        static SoundPlayer[] Siren = new SoundPlayer[Sirensounds.Length];
        static SoundPlayer Fright = new SoundPlayer("fright_firstloop.wav");
        static string[] Deathsounds = { "death_0.wav", "death_1.wav" };
        static SoundPlayer[] Death = new SoundPlayer[Deathsounds.Length];
        static int Deathsoundindex = 0;
        static string GameTitel = File.ReadAllText("GameTitel.txt");
        static string TitelscreenImage = File.ReadAllText("TitelscreenImage.txt");
        static string Lost = File.ReadAllText("Lost.txt");
        static string GhostforLostscreen = File.ReadAllText("GhostForLostScreen.txt");
        static string Won = File.ReadAllText("Won.txt");
        static string PacManforWinscreen = File.ReadAllText("PacManForWinScreen.txt");
        static string restarting = File.ReadAllText("Restarting.txt");
        static string[] Loading = (File.ReadAllText("Loading.txt")).Split(',');
        static int sirenincrease;
        static string Map = File.ReadAllText("Map.txt");
        static string readMap;
        static void Main(string[] args)
        {
            for (int i = 0; Sirensounds.Length > i; i++)
            {
                Siren[i] = new SoundPlayer(Sirensounds[i]);
            }
            GameMusic.PlayLooping();
            while (Gamerunning)
            {
               Counter = 0;
                pacmanY = 17;
                pacmanX = 45;
                xspeed = 0;
                yspeed = 0;
                lives = 3;
                points = 0;
                sirenincrease = 30;
                Console.OutputEncoding = Encoding.UTF8;
                Console.CursorVisible = false;
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
                    GameStart.Play();
                    Thread.Sleep(4000);
                    gamestarted = true;
                    Console.SetCursorPosition(0, 27);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int i = 0; i<3; i++)
                    {
                        Console.Write(PacManSubstitutesDisplay);
                    }
                    Siren[Sirenstage]= new SoundPlayer(Sirensounds[Sirenstage]);
                    Siren[Sirenstage].PlayLooping();
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
                    if ((Sirenstage < Sirensounds.Length) && (points >= sirenincrease))
                    {
                        Sirenstage++;
                        Siren[Sirenstage].PlayLooping();
                        if (sirenincrease < 150)
                        {
                            sirenincrease += 30;
                        }
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
                            GameMusic.PlayLooping();
                            while (Gamepaused)
                            {
                                ConsoleKeyInfo Continue = Console.ReadKey();
                                if (Continue.Key == ConsoleKey.Escape)
                                {
                                    Gamepaused = false;
                                    if (PowerPelletactive == true)
                                    {
                                        Fright.PlayLooping();
                                    }
                                    else
                                    {
                                        Siren[Sirenstage].PlayLooping();
                                    }
                                }
                            }
                        }
                    }
                    Console.SetCursorPosition(0, 25);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.Write("Punkte: " + points);
                    Console.WriteLine();
                    Counter++;
                    if (isThereAWall(pacmanX + xspeed, pacmanY + yspeed) == false)
                    {
                        Console.SetCursorPosition(pacmanX, pacmanY);
                        Console.Write(' ');
                        Console.SetCursorPosition(pacmanX + xspeed, pacmanY + yspeed);
                        pacmanX = pacmanX + xspeed;
                        pacmanY = pacmanY + yspeed;
                    }
                    else
                    {
                        Console.SetCursorPosition(pacmanX, pacmanY);
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
                    Ghosts();
                    Thread.Sleep(150);
                    Symbol = previousSymbol(pacmanX, pacmanY);
                    if (Symbol == Point)
                    {
                        points++;
                        changeMapSymbols(pacmanX, pacmanY);
                    }
                    if (Symbol == PowerPellet)
                    {
                        changeMapSymbols(pacmanX, pacmanY);
                        PowerPelletactive=true;
                        PelletDuration = 20;
                        Fright.PlayLooping();
                    }
                    if (PowerPelletactive)
                    {
                        GhostColor = ConsoleColor.Blue;
                        PelletDuration--;
                        if(PelletDuration==0)
                        {
                            PowerPelletactive = false;
                            Siren[Sirenstage].PlayLooping();
                        }

                    }
                    else
                    {
                        GhostColor = ConsoleColor.Red;
                    }
                    PacManTouchesGhost();
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
                    Siren[Sirenstage].Stop();
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
                    Siren[Sirenstage].Stop();
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
        static void Ghosts()
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
                PacManTouchesGhost();
                if(isPacManNearGhost()!=0)
                {
                    previousdirection = isPacManNearGhost();
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
        static void Restart()
        {
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
        static int isPacManNearGhost()
        {
            int wallcounter;
            int direction = 0;
            if (pacmanX == ghostx)
            {
                if (pacmanY > ghosty)
                {
                    wallcounter = 0;
                    for (int i = pacmanY; i > ghosty; i--)
                    {
                        if (isThereAWall(pacmanX, i))
                        {
                            wallcounter++;
                        }
                    }
                    if (wallcounter == 0)
                    {
                        direction = 2;
                    }
                }
                if (pacmanY < ghosty)
                {
                    wallcounter = 0;
                    for (int i = ghosty; i > pacmanY; i--)
                    {
                        if (isThereAWall(ghostx, i))
                        {
                            wallcounter++;
                        }
                    }
                    if (wallcounter == 0)
                    {
                        direction = 4;
                    }

                }
            }
            if (pacmanY == ghosty)
            {
                if (pacmanX > ghostx)
                {
                    wallcounter = 0;
                    for (int i = pacmanX; i > ghostx; i--)
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
                if (pacmanX < ghostx)
                {
                    wallcounter = 0;
                    for (int i = ghostx; i > pacmanX; i--)
                    {
                        if (isThereAWall(i, ghosty))
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
        static void PacManTouchesGhost()
        {
            if (((Math.Abs(pacmanX - ghostx) <= 1 && pacmanY == ghosty) || (pacmanX == ghostx && Math.Abs(pacmanY - ghosty) <= 1))&&!PowerPelletactive)
            {
                PacManDeath();
            }
            else if (((Math.Abs(pacmanX - ghostx) <= 1 && pacmanY == ghosty) || (pacmanX == ghostx && Math.Abs(pacmanY - ghosty) <= 1)) && PowerPelletactive)
            {
                GhostDeath();
            }
        }
        static void PacManDeath()
        {
            lives -= 1;
            for (int i = 0;Deathsounds.Length > i; i++)
            {
                Death[i] = new SoundPlayer(Deathsounds[i]);
            }
            Death[Deathsoundindex].Play();
            Deathsoundindex++;
            Console.SetCursorPosition(ghostx, ghosty);
            Console.Write(' ');
            Console.SetCursorPosition(pacmanX, pacmanY);
            Console.Write("⊔");
            Thread.Sleep(100);
            Console.SetCursorPosition(pacmanX, pacmanY);
            Console.Write('_');
            Thread.Sleep(100);
            Console.SetCursorPosition(pacmanX, pacmanY);
            Console.Write(' ');
            Thread.Sleep(800);
            Death[Deathsoundindex].Play();
            Thread.Sleep(200);
            Death[Deathsoundindex].Play();
            Deathsoundindex = 0;
            Thread.Sleep(200);
            pacmanX = 45;
            pacmanY = 17;
            ghostx = 45;
            ghosty = 1;
            Siren[Sirenstage].PlayLooping();
            Console.SetCursorPosition(0, 27);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 27);
            for (int i = 0; i < lives; i++)
            {
                Console.Write(PacManSubstitutesDisplay);
            }
        }
        static void GhostDeath()
        {
            ghostx = 45;
            ghosty = 2;
            points += 15;
            PowerPelletactive = false;
            Siren[Sirenstage].PlayLooping();

        }
    }
}


