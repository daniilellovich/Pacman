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
        protected Level  _level;
        protected Pacman _pacman;
        protected PointF _home;
        protected Point  _destination;
        protected Point  _prevLocation;
        protected Point  _corner, _corner2;
        protected Point  _goal = new Point();
        public bool IsFrightened { get; set; }
        public bool IsEaten { get; set; }

        Random _rand = new Random();

        public Ghost() : base()
            => _pathFinder = new GhostPathFinder(Game.State.Level);

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

        public  void ChangeMode(MovingMode movingMode)
        {
            _movingMode = movingMode;

            if (movingMode == ScatterMode || movingMode == ChaseMode)
                SetSprite(_sprite._image);
            if (movingMode == FrightenedMode)
                SetSprite(GameResources.Fright);
            if (movingMode == ReturningHome)
                SetSprite(GameResources.GhostEyes);
        }

        public Point ScatterMode()
        {
            _goal = (LocationF.IsOnXandY(_corner, 2f)) ? _corner2 : _corner;
            List<Point> path = _pathFinder.FindPath(_prevLocation, Location, _goal);
            return (path.Count == 1) ? Location : path[1];
        }
        public Point FrightenedMode()
        {
            if (!IsFrightened)
                ResetPrevLoc(); //при смене режима может идти назад
            IsFrightened = true;

            SetSpeed(0.7f);

            List<Point> validNeighbourPoints = new List<Point>();

            foreach (var point in Location.NeighbourPoints)
                if (_level.IsWalkablePoint(point) && point != _prevLocation)
                    validNeighbourPoints.Add(point);

            return validNeighbourPoints[_rand.Next(validNeighbourPoints.Count)];
        }
        public Point ReturningHome()
        {
            if (Location == _home)
            {
                ChangeMode(_movingMode);
                IsFrightened = false;
                IsEaten = false;
            }

            SetSpeed(2f);

            List<Point> path = _pathFinder.FindPath(_prevLocation, Location, _home.ToPoint());
            return (path.Count == 1) ? Location : path[1];
        }
        public abstract Point ChaseMode();
        
        public void ResetPrevLoc()
            => _prevLocation = new Point();
    }
}