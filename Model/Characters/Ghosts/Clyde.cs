using System;

namespace Pacman
{
    public class Clyde : Ghost
    {
        public Clyde(Mediator gameState) : base(gameState)
        {
            SetMode(ScatterMode);
            SetSpriteImage(_spriteImage = GameResources.Clyde);
            _color = System.Drawing.Color.Goldenrod;
            _locationF = _home = new PointF(15.5f, 17);
            _destination = _curMode();
            _corner  = new Point(1, 32);
        }

        public override Point ChaseMode()
        {   //if pacman is eight tiles away, he is a target, else - corner 
            _goal = PacmanIsFar() ? _gameState.Pacman.GetLoc() :
                (GetLocF().IsOnXandY(_corner, 2f)) ?
                _corner : GetWalkableNeighbourPoint();
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return PathExists ? _path[1] : GetLoc();
        }

        private bool PacmanIsFar()
          => Math.Sqrt(Math.Pow(_gameState.Pacman.GetLoc().X - GetLoc().X, 2)
                     + Math.Pow(_gameState.Pacman.GetLoc().Y - GetLoc().Y, 2)) > 8;
    }
}
