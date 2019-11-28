using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Pacman
{//показывать механизм каждого на нажатии b p c i
    public static class Game
    {
        public static GameState State { get; set; }
        public static int LevelNum = 1;
        public static int highScore;
        public static int scoreCounter;

        public static void Init()
        {
            Tile.Size = new Size(StartForm.resolution.Height / 40, StartForm.resolution.Height / 40);

            State        = new GameState();

            State.Level  = new Level();

            State.Pacman = new Pacman(0.1f, State.Level);  //убрать сдвиг вначале игры
            State.Blinky = new Blinky(State.Pacman, State.Level);
            State.Pinky  = new Pinky(State.Pacman, State.Level);
            State.Inky   = new Inky(State.Pacman, State.Level);
            State.Clyde  = new Clyde(State.Pacman, State.Level);

            List<Ghost> ghosts = new List<Ghost>() { State.Blinky, State.Pinky, State.Inky, State.Clyde };
            State.BehaviorChanger = new BehaviorChanger(LevelNum, ghosts, State.Pacman);
      //    State.ItemsController = new ItemsController();
        }

        public static void Update()
        {
        //  State.ItemsController.Update();
            State.Pacman.Update();

        //    State.Blinky.Update();
        //     State.Pinky.Update();
        //    State.Inky.Update();
            State.Clyde.Update();
        }
    }
}