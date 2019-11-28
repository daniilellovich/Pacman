using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Pacman
{
    public abstract class Ghost : Character
    {
        public delegate Point MovingMode();
        protected GhostPathFinder _pathFinder;// = new GhostPathFinder();
        protected MovingMode _movingMode;
        protected Level _level;
        protected Pacman _pacman;
        protected PointF _home;
        protected Point  _destination;
        protected Point  _prevLocation;
        protected Point  _corner, _corner2;
        protected Point  _goal = new Point();
        public bool IsFrightened { get; set; }

        public override void Update()
        {
            Point savedLoc = Location;

            int dx = !LocationF.IsOnX(_destination, 0.06f) ? ((LocationF.X < _destination.X) ? 1 : -1) : 0;
            int dy = !LocationF.IsOnY(_destination, 0.06f) ? ((LocationF.Y < _destination.Y) ? 1 : -1) : 0;

            if (LocationF.IsOnXandY(_destination, 0.06f))
                _destination = _movingMode();

            Move(dx, dy);

            if (LocationF.IsFarFrom(savedLoc, 0.5f))
                _prevLocation = savedLoc;
        }

        public Ghost() : base()
        {
            _pathFinder = new GhostPathFinder(Game.State.Level);
        }

        public void ChangeMode(MovingMode movementDelegate)
            => _movingMode = movementDelegate;

        public Point ScatterMode()
        {
            _goal = (LocationF.IsOnXandY(_corner, 2f)) ? _corner2 : _corner;
            List<Point> path = _pathFinder.FindPath(_prevLocation, Location, _goal);
            return (path.Count == 1) ? Location : path[1];
        }
        public Point ReturningHome()
        {
            //    SoundController.PlaySound("Invincible");
            //    Point blinkyLocation = new Point(Convert.ToInt32(blinkyLocationF.Y), Convert.ToInt32(blinkyLocationF.X));
            //    Point blinkyHome = new Point(Convert.ToInt32(Game.State.Blinky.Home.Y), Convert.ToInt32(Game.State.Blinky.Home.X));
            //    Point prevLoc = new Point();
            //    coord = Game.State.GhostPathFinder.FindPath(blinkyLocation, blinkyHome, prevLoc);
            //    return coord;
            return new Point();
        }   //СПРАЙТ
        public Point FrightenedMode()
        {
            IsFrightened = true;
            return new Point();
        }
        public abstract Point ChaseMode();
        public void ResetPrevLoc()
            => _prevLocation = new Point();

        //Point a;
        //bool flag = true;

        //public override void Update()
        //{
        //    int dx = 0;
        //    int dy = 0;
        //    float speed = Speed;

        //    float LX = Location.Y;
        //    float LY = Location.X;

        //    if (flag == true)
        //    {
        //        a = _movementDelegate(Location);
        //        flag = false;
        //    }

        //    if (Math.Abs(LY - a.Y) > 0.06)
        //    {
        //        if (LY < a.Y) dx = 1;
        //        if (LY > a.Y) dx = -1;
        //    }

        //    if (Math.Abs(LX - a.X) > 0.06)
        //    {
        //        if (LX < a.X) dy = 1;
        //        if (LX > a.X) dy = -1;
        //    }

        //    if ((Math.Abs(LX - a.X) < 0.08) && (Math.Abs(LY - a.Y) < 0.08))
        //        a = _movementDelegate(Location);

        //    Move(dx, dy);
        //}

    }
}