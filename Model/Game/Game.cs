using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Pacman
{
    public static class Game
    {
        public static GameState State { get; set; }
        public static int LevelNum = 1;
        public static int highScore;
        public static int scoreCounter;

        public static void Init()
        {
            Tile.Size = new Size(StartForm.resolution.Height / 40, StartForm.resolution.Height / 40);

            State = new GameState();

            State.Level  = new Level();

            State.Pacman = new Pacman();
            State.Blinky = new Blinky();
            State.Pinky  = new Pinky();
            State.Inky   = new Inky();
            State.Clyde  = new Clyde();

            State.Ghosts = new List<Ghost>() { State.Blinky, State.Pinky, State.Inky, State.Clyde };
            State.BehaviorChanger = new BehaviorController(LevelNum);
            State.ItemsController = new ItemsController();
        }

        public static void Update()
        {
            State.ItemsController.Update();
            State.Pacman.Update();

            State.Blinky.Update();
            State.Pinky.Update();
            State.Inky.Update();
            State.Clyde.Update();
        }
    }
}