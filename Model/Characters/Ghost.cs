using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pacman
{
    public abstract class Ghost : Character
    {
        #region vars
        public enum State { ChaseOrScatter, Fright, Returning };
        private State _currentState;

        public delegate Point MovingMode();
        protected MovingMode _curMode;

        public MovingMode GlobalMovingMode;
        public float GlobalSpeed = 0.11f;

        protected GhostPathFinder _pathFinder;
        protected Image _spriteImage;
        protected Color _color;
        protected Point _destination, _prevLoc, _goal, _corner;
        protected List<Point> _path;
        public bool PathIsVisible { get; private set; }

        public State GetState()
            => _currentState;

        public void SetState(State state)
            => _currentState = state;
        #endregion

        public Ghost(Mediator gameState) : base(gameState)
        {
            GlobalMovingMode = ScatterMode;
            _curMode = ScatterMode;
            _currentState = State.ChaseOrScatter;
            _pathFinder = new GhostPathFinder(_gameState.Level);
        }

        public override void Update()
        {
            if (CameTo(_destination))
                _destination = _curMode();

            //to prevent backtracking
            Point savedPrevLoc = GetLoc();

            MoveTo(_destination);

            if (GetLocF().IsFarFrom(savedPrevLoc, 0.5f))
                _prevLoc = savedPrevLoc;
        }

        public Point ScatterMode()
        {
            _goal = (_locationF.IsOnXandY(_corner, 1.5f)) ?
                GetWalkableNeighbourPoint() : _corner;
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return PathExists ? _path[1] : GetLoc();
        }

        public abstract Point ChaseMode();

        public Point FrightenedMode()
        {
            _goal = GetWalkableNeighbourPoint();
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return PathExists ? _path[1] : GetLoc();
        }

        public Point ReturningHome()
        {
            if (GetLocF().IsOnXandY(_home, 0.8f))
            {
                _currentState = State.ChaseOrScatter;
                SetMode(GlobalMovingMode);
                return GetLoc();
            }
            else
            {
                _goal = _home.ToPoint();
                _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
                return PathExists ? _path[1] : GetLoc();
            }
        }

        public override void Eaten()
            => SetMode(ReturningHome);

        public void SetMode(MovingMode newMode)
        {
            if (newMode == ScatterMode || newMode == ChaseMode)
            {
                if (_currentState != State.ChaseOrScatter)
                {
                    _curMode = newMode;
                    _currentState = State.ChaseOrScatter;
                    SetSpriteImage(_spriteImage);
                    SetSpeed(GlobalSpeed);
                }
            }
            else if (newMode == FrightenedMode)
            {
                if (_currentState != State.Returning)
                {
                    _currentState = State.Fright;
                    _curMode = FrightenedMode;
                    SetSpriteImage(GameResources.Fright);
                    SetSpeed(0.6f * GlobalSpeed);
                }
            }
            else if (newMode == ReturningHome)
            {
           //     if (_currentState != State.Returning)
           //     {
                    _currentState = State.Returning;
                    _curMode = ReturningHome;
                    SetSpriteImage(GameResources.GhostEyes);
                    SetSpeed(GlobalSpeed);
           //     }
            }
        }

        public void StartBlinking()
        { 
            if (_currentState == State.Fright)
                _sprite.ChangeImage(GameResources.FrightEnd);
        }

        protected Point GetWalkableNeighbourPoint()
        {   //move to other class
            List<Point> validNeighbourPoints = new List<Point>();

            foreach (Point point in GetLoc().NeighbourPoints)
                if (_gameState.Level.IsWalkableForGhost(point) && point != _prevLoc)
                    validNeighbourPoints.Add(point);

            return validNeighbourPoints[new Random().Next(validNeighbourPoints.Count)];
        }

        protected bool PathExists
            => (_path.Count != 1);

        public void SwitchPathVisibility()
            => PathIsVisible = !PathIsVisible;

        public void DrawPath(Graphics gr)
        {
            Pen pen = new Pen(_color, 8f);
            var path = new System.Drawing.PointF[_path.Count];

            for (int i = 0; i < _path.Count; i++)
            {
                float p = (_path[i].X + 0.5f) * Tile.Size.Width;
                float q = (_path[i].Y + 0.5f) * Tile.Size.Height;
                path[i] = new System.Drawing.PointF(p, q);
            }

            if (PathExists)
                gr.DrawCurve(pen, path);

            float x = (_goal.X + 0.3f) * Tile.Size.Width;
            float y = (_goal.Y + 0.3f) * Tile.Size.Height;
            gr.DrawEllipse(pen, x, y, 10f, 10f);
            gr.DrawEllipse(pen, x, y, 6, 6f);
        }
    }
}