using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pacman
{
    public abstract class Ghost : Character
    {
        #region vars
        public delegate Point MovingMode();
        protected MovingMode _curMode;

        public enum State { ChaseOrScatter, Fright, Returning };
        private State _curState;

        protected GhostPathFinder _pathFinder;
        protected Image _spriteImage;
        protected Color _color;
        protected Point _destination, _prevLoc, _goal, _corner;
        protected List<Point> _path;
        public bool PathIsVisible { get; private set; }

        public State GetState()
            => _curState;

        public void SetState(State state)
            => _curState = state;
        #endregion

        public Ghost(Mediator gameState) : base(gameState)
        {
            _curState = State.ChaseOrScatter;
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
            _goal = (_locationF.IsOnXandY(_corner, 2f)) ?
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
                _curState = State.ChaseOrScatter;
                SetMode(_gameState.GameController.GetGlobalMovingMode());
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
            if ((newMode == ScatterMode || newMode == ChaseMode)
                && (_curState == State.ChaseOrScatter))
            {
                _curMode = newMode;
                SetSpriteImage(_spriteImage);
                SetCurSpeed(_normalSpeed);
            }
            else if ((newMode == FrightenedMode))
            {
                _curState = State.Fright;
                _curMode = FrightenedMode;
                SetSpriteImage(GameResources.Fright);
                SetCurSpeed(0.6f * _normalSpeed);
            }
            else if (newMode == ReturningHome)
            {
                _curState = State.Returning;
                _curMode = ReturningHome;
                SetSpriteImage(GameResources.GhostEyes);
                SetCurSpeed(_normalSpeed);
            }
        }

        public void StartBlinking()
        { 
            if (_curState == State.Fright)
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