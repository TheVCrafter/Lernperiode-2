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
        static SoundPlayer winsound = new SoundPlayer("pacman_AwvgsBv.wav");
        static SoundPlayer lostsound = new SoundPlayer("pacman-die.wav");
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
        static int wallcounter;
        static int direction = 0;
        static int previousdirection;
        static bool otherDirection = false;
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
                    powerPellet();
                    PacManTouchesGhost();
                    IsGameFinished();
                }
                EndGame();
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
        static void Ghosts()
        {
            Random random = new Random();
            Console.OutputEncoding = Encoding.UTF8;
            string Ghost = "Δ";

            if (Counter == 1)
            {
                ghostx = 45;
                ghosty = 2;
            }

            int previousX = ghostx;
            int previousY = ghosty;

            bool[] walls = {
        isThereAWall(ghostx + 1, ghosty),
        isThereAWall(ghostx, ghosty + 1),
        isThereAWall(ghostx - 1, ghosty),
        isThereAWall(ghostx, ghosty - 1)
    };

            List<int> possibleDirections = new List<int>();
            for (int i = 0; i < walls.Length; i++)
            {
                if (!walls[i]) possibleDirections.Add(i + 1);
            }

            if (possibleDirections.Count == 0) return;
            if (possibleDirections.Contains(previousdirection))
            {
                direction = previousdirection;
            }
            else
            {
                direction = possibleDirections[random.Next(possibleDirections.Count)];
            }
            isPacManNearGhost();
            GhostColor = PowerPelletactive ? ConsoleColor.Blue : ConsoleColor.Red;
            switch (direction)
            {
                case 1: ghostx += 1; break;
                case 2: ghosty += 1; break;
                case 3: ghostx -= 1; break;
                case 4: ghosty -= 1; break;
            }
            Console.SetCursorPosition(previousX, previousY);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(previousSymbol(previousX, previousY));

            Console.SetCursorPosition(ghostx, ghosty);
            Console.ForegroundColor = GhostColor;
            Console.Write(Ghost);
            previousdirection = direction;
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
        static void IsGameFinished()
        {
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
        static void powerPellet()
        {
            if (Symbol == PowerPellet)
            {
                changeMapSymbols(pacmanX, pacmanY);
                PowerPelletactive = true;
                PelletDuration = 20;
                Fright.PlayLooping();
            }
            if (PowerPelletactive)
            {
                GhostColor = ConsoleColor.Blue;
                PelletDuration--;
                if (PelletDuration == 0)
                {
                    PowerPelletactive = false;
                    Siren[Sirenstage].PlayLooping();
                }

            }
            else
            {
                GhostColor = ConsoleColor.Red;
            }
        }
        static void EndGame()
        {
             if (lost)
                {
                    Siren[Sirenstage].Stop();
                    Console.Clear();
                    lostsound.Play();
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
                    winsound.Play();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Won);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(PacManforWinscreen);
                    Console.WriteLine();
                    Restart();
                }
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
        static void isPacManNearGhost()
        {
            CheckSameCoordinate(pacmanX, ghostx, pacmanY, ghosty,2,4);
            CheckSameCoordinate(pacmanY, ghosty, pacmanX, ghostx, 1,3);
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
            DeathAnimation();
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
        static void DeathAnimation()
        {
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
        }
        static void CheckSameCoordinate(int pacmancoordinate1, int ghostcoordinate1, int pacmancoordinate2, int ghostcoordinate2, int direction1, int direction2)
        {
            wallcounter = 0;
            if (pacmancoordinate1==ghostcoordinate1)
            {
                if (pacmancoordinate2>ghostcoordinate2)
                {
                    for (int i = ghostcoordinate2; i < pacmancoordinate2; i++)
                    {
                        WallBetween(pacmancoordinate1, i);
                    }
                    if (wallcounter == 0)
                    {
                        direction = direction1;
                    }
                }
                if (pacmancoordinate2<ghostcoordinate2)
                {
                    for (int i = ghostcoordinate2; i > pacmancoordinate2; i--)
                    {
                      WallBetween(pacmancoordinate1, i);
                    }
                    if (wallcounter == 0)
                    {
                        direction=direction2;
                    }

                }
            }
        }
        static void WallBetween(int coordinate1, int coordinate2)
        {
            try
            {
                if (isThereAWall(coordinate1,coordinate2))
                {
                    wallcounter++;
                }
            }
            catch
            {
                if (isThereAWall(coordinate2, coordinate1))
                {
                    wallcounter++;
                }
            }
        }
    }
}


