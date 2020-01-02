using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pacman
{
    public abstract class Ghost : Character
    {
        #region vars
        public delegate Point MovingMode();
        public MovingMode _globalMode;
        public float _globalGhostsSpeed;

        protected MovingMode _curMode;
        protected GhostPathFinder _pathFinder;
        protected Color _color; //для отрисовки пути
        protected Point _destination, _prevLoc, _corner, _corner2, _goal;
        protected List<Point> _path;

        public bool PathIsVisible { get; private set; }
        public bool IsFrightened { get; private set; }
        public bool IsEaten { get; private set; }
        #endregion

        public Ghost(Mediator gameState) : base(gameState)
            => _pathFinder = new GhostPathFinder(_gameState.Level);

        public override void Update()
        {
            (int dx, int dy) = CalcOffset(_destination);

            if (GetLocF().IsOnXandY(_destination, 0.06f))
                _destination = _curMode();

            Point savedLoc = GetLoc(); 
            Move(dx, dy);
            if (GetLocF().IsFarFrom(savedLoc, 0.5f))
                _prevLoc = savedLoc;
        }

        public Point ScatterMode()
        {
            _goal = (_locationF.IsOnXandY(_corner, 2f)) ? _corner2 : _corner;
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetLoc() : _path[1];
        }

        public abstract Point ChaseMode();

        public void SetMode(MovingMode newMode)
        {
            _globalMode = _curMode = newMode;
          //  SetSpeed(_globalGhostsSpeed);

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

        public void SwitchPathVisibility()
            => PathIsVisible = !PathIsVisible;

        public Point FrightenedMode()
        {
            if (!IsFrightened)
            {
                SetMode(_globalMode);
                IsFrightened = false;
            }

            _goal = GetRandomNeighboringPoint();
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetLoc() : _path[1];
        }

        public override void Eaten()
        {
            //IsEaten = true;
            //_globalMovingMode = _curMovingMode;
            //// SetSpeed(3f);
            //SoundController.PlaySound("MonsterEaten");
            ////      SoundController.PlayLongSound("");

            //SetMode(ReturningHome);
        }

        public Point ReturningHome()
        {
            if (GetLocF().IsOnXandY(_home, 0.8f))
            {
                _prevLoc = new Point();
                IsFrightened = false;
                IsEaten = false;
         //       SetMode(_globalMode);
            }
            else
                _goal = _home.ToPoint();

            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetLoc() : _path[1];
        }

        public void DrawPath(Graphics gr)
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

        protected Point GetRandomNeighboringPoint()
        {
            Random _rand = new Random();
            List<Point> validNeighbourPoints = new List<Point>();

            foreach (Point point in GetLoc().NeighbourPoints)
                if (_gameState.Level.IsWalkableForGhost(point) && point != _prevLoc)
                    validNeighbourPoints.Add(point);

            return validNeighbourPoints[_rand.Next(validNeighbourPoints.Count)];
        }
    }
}