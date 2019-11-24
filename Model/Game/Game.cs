using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Pacman
{
    public static class Game
    {
        public static Size TileSize = new Size(StartForm.resolution.Height / 40, StartForm.resolution.Height / 40);

        public static GameState State { get; set; }
        public static string MapName;
        public static int LevelNum = 1;
        public static int highScore;
        public static int scoreCounter;

        public static void Init()
        {
            State = new GameState();
            MapName = "Levels\\Pacman.txt";

            State.Level = new Level();
            State.Behaviors = new Behaviors();
            State.GhostPathFinder = new GhostPathFinder(MapName);
            State.ItemsController = new ItemsController();

            InitCharacters();

            State.BehaviorChanger = new BehaviorChanger(LevelNum);
        }

        public static void InitCharacters()
        {
            State.Pacman = new Pacman(new PointF(13.5f, 26), 0.1f);  //убрать сдвиг вначале игры

            State.Blinky = new Ghost(GameResources.Blinky, new PointF(13.5f, 14), 0.12f, State.Behaviors.BlinkyScatterMode);
            State.Pinky = new Ghost(GameResources.Pinky, new PointF(13.5f, 17), 0.12f, State.Behaviors.BlinkyScatterMode);
            State.Inky = new Ghost(GameResources.Inky, new PointF(11.5f, 17), 0.12f, State.Behaviors.BlinkyScatterMode);
            State.Clyde = new Ghost(GameResources.Clyde, new PointF(15.5f, 17), 0.12f, State.Behaviors.BlinkyScatterMode);
        }

        public static void Update()
        {
            State.Pacman.Update();
            State.ItemsController.Update();
    //        State.Blinky.Update();
    //        State.Pinky.Update();
    //        if (Game.State.Pacman._dotsEaten > 50)   ///временно
    //            State.Inky.Update();
    //        if (Game.State.Pacman._dotsEaten > 80)   ///временно
    //            State.Clyde.Update();
        }

        //public static void InitThreads()
        //{
        // //   Thread pacmanThread = new Thread(State.Pacman.Update, );
        ////    Thread itemsThread = new Thread(State.ItemsController.Update);
        //    Thread blinkyThread = new Thread(State.Blinky.Update);
        //    Thread pinkyThread = new Thread(State.Pinky.Update);
        //    Thread inkyThread = new Thread(State.Inky.Update);
        //    Thread clydeThread = new Thread(State.Clyde.Update);

        // //   pacmanThread.Start();
        // //   itemsThread.Start();
        //    blinkyThread.Start();
        //    pinkyThread.Start();
        //    inkyThread.Start();
        //    clydeThread.Start();
        //}
    }
}