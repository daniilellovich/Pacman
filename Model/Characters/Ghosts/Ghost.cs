using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Pacman
{
    public abstract class Ghost : Character //исправить Pinky чтобы проходил мимо цели
    {
        public delegate Point MovingMode();
        protected MovingMode _movingMode;

        protected GhostPathFinder _pathFinder = Game.State.GhostPathFinder;
        protected Level _level = Game.State.Level;
        protected Pacman _pacman = Game.State.Pacman;

        protected Color _color;
        protected List<Point> _path;
        protected PointF _home;
        protected Point _destination, _prevLoc, _corner, _corner2, _goal;

        public bool PathIsVisible { get; set; }
        public bool IsFrightened { get; set; }
        public bool IsEaten { get; set; }

        Random _rand = new Random();

        public int[] time= new int[4];
        public delegate void ChangeMode(MovingMode mode);
        public event MovingMode ChangeMode;

        public Ghost() : base() 
        { 
            _pathFinder = new GhostPathFinder(Game.State.Level);
            modeChanged += FrightenedMode;
            modeChanged += ChaseMode;
            modeChanged += ScatterMode;
        }

        public override void Update()
        {
            Point savedLoc = Location;

            int dx = !LocationF.IsOnX(_destination, 0.06f) ? ((LocationF.X < _destination.X) ? 1 : -1) : 0;
            int dy = !LocationF.IsOnY(_destination, 0.06f) ? ((LocationF.Y < _destination.Y) ? 1 : -1) : 0;

            int walkablePointsCounter = 0;
            foreach (var point in Location.NeighbourPoints)
                if (_level.IsWalkablePoint(point))
                    walkablePointsCounter++;

            if (walkablePointsCounter > 1)
                if (LocationF.IsOnXandY(_destination, 0.06f))
                    _destination = _movingMode();

            Move(dx, dy);

            if (LocationF.IsFarFrom(savedLoc, 0.5f))
                _prevLoc = savedLoc;
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
            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);
            return (_path.Count == 1) ? Location : _path[1];
        }
        public Point FrightenedMode()
        {
            //изменять скорость

            List<Point> validNeighbourPoints = new List<Point>();

            if (Location.NeighbourPoints.Count > 2)
                foreach (Point point in Location.NeighbourPoints)
                    if (_level.IsWalkablePoint(point) && point != _prevLoc)
                        validNeighbourPoints.Add(point);

            int randNum = _rand.Next(validNeighbourPoints.Count);
        //    Debug.WriteLine(randNum);

            Point randomPoint = validNeighbourPoints[randNum];
            _path = new List<Point>() { randomPoint };
            return randomPoint;
        }
        public Point ReturningHome()
        {
            if (Location == _home)
            {
         //       ChangeMode(_movingMode);
                IsFrightened = false;
                IsEaten = false;
            }

            //SetSpeed(2f);

            _path = _pathFinder.FindPath(_prevLoc, Location, _home.ToPoint());
            return (_path.Count == 1) ? Location : _path[1];
        }
        public abstract Point ChaseMode();
      
        public void ResetPrevLoc()
            => _prevLoc = new Point();

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
    }
}