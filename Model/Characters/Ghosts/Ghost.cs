using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Pacman
{
    public abstract class Ghost : Character //исправить Pinky чтобы проходил мимо цели
    {
        #region vars
        public delegate Point MovingMode();
        protected MovingMode _curMovingMode;
        public MovingMode _globalMovingMode;
        public float _globalGhostsSpeed;

        protected GhostPathFinder _pathFinder = Game.State.GhostPathFinder;
        protected Level _level = Game.State.Level;
        protected Pacman _pacman = Game.State.Pacman;

        protected Color _color;
        protected PointF _home;
        protected Point _destination, _prevLoc, _corner, _corner2, _goal;
        protected List<Point> _path;

        public bool PathIsVisible { get; set; }
        public bool IsFrightened { get; set; }
        public bool IsEaten { get; set; }
        #endregion

        public Ghost()
            => _pathFinder = new GhostPathFinder(Game.State.Level);

        public override void Update()
        {
            Point savedLoc = Location;

            int dx = !LocationF.IsOnX(_destination, 0.06f) ? ((LocationF.X < _destination.X) ? 1 : -1) : 0;
            int dy = !LocationF.IsOnY(_destination, 0.06f) ? ((LocationF.Y < _destination.Y) ? 1 : -1) : 0;

            if (LocationF.IsOnXandY(_destination, 0.06f))
                _destination = _curMovingMode();

            Move(dx, dy);

            if (LocationF.IsFarFrom(savedLoc, 0.5f))
                _prevLoc = savedLoc;
        }

        public Point ScatterMode()
        {
            _goal = (LocationF.IsOnXandY(_corner, 2f)) ? _corner2 : _corner;
            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);
            return (_path.Count == 1) ? Location : _path[1];
        }

        public abstract Point ChaseMode();





















        public void ChangeMode(MovingMode newMode)
        {
            _globalMovingMode = _curMovingMode = newMode;
            SetSpeed(_globalGhostsSpeed);

            if (newMode == ScatterMode || newMode == ChaseMode)
            {
                SetSprite(_sprite._image);
            }
            else if (newMode == FrightenedMode)
            {
                SetSprite(GameResources.Fright);
                IsFrightened = true;
            }
            else if (newMode == ReturningHome)
            {
                SetSprite(GameResources.GhostEyes);
                IsEaten = true;
            }
        }

        public Point FrightenedMode()
        {
            if (!IsFrightened)
            {
                ChangeMode(_globalMovingMode);
                IsFrightened = false;
            }

            _goal = GetRandomNeighbourWalkablePoint();
            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);
            return (_path.Count == 1) ? Location : _path[1];
        }

        public override void Eaten()
        {
            IsEaten = true;
            _globalMovingMode = _curMovingMode;
            // SetSpeed(3f);
            SoundController.PlaySound("MonsterEaten");
            //      SoundController.PlayLongSound("");

            ChangeMode(ReturningHome);
        }

        public Point ReturningHome()
        {
            if (LocationF.IsOnXandY(_home, 0.8f))
            {
                _prevLoc = new Point();
                IsFrightened = false;
                IsEaten = false;
                ChangeMode(_globalMovingMode);
            }
            else
                _goal = _home.ToPoint();

            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);
            return (_path.Count == 1) ? Location : _path[1];
        }















        public void DisplayPathAndGoal(Graphics gr)
        {
            Pen pen = new Pen(_color, 8f);
            System.Drawing.PointF[] path = new System.Drawing.PointF[_path.Count];

            for (int i = 0; i < _path.Count; i++)
            {
                float p = (_path[i].X + 0.5f) * Tile.Size.Width;
                float q = (_path[i].Y + 0.5f) * Tile.Size.Height;
                path[i] = new System.Drawing.PointF(p, q);
            }

            if (path.Length > 1)
                gr.DrawCurve(pen, path);

            float x = (_goal.X + 0.4f) * Tile.Size.Width;
            float y = (_goal.Y + 0.4f) * Tile.Size.Height;
            gr.DrawEllipse(pen, x, y, 10f, 10f);
            gr.DrawEllipse(pen, x, y, 6, 6f);
        }

        public Point GetRandomNeighbourWalkablePoint()
        {
            Random _rand = new Random();
            List<Point> validNeighbourPoints = new List<Point>();

            foreach (Point point in Location.NeighbourPoints)
                if (_level.IsWalkablePoint(point) && point != _prevLoc)
                    validNeighbourPoints.Add(point);

            return validNeighbourPoints[_rand.Next(validNeighbourPoints.Count)];
        }
    }
}