namespace Pacman
{
    public class Clyde : Ghost
    {
        public Clyde(Mediator gameState) : base(gameState)
        {
            SetSpeed(0.12f);
            SetMode(ScatterMode);
            SetSprite(GameResources.Clyde);
            _color = System.Drawing.Color.Goldenrod;
            _locationF = _home = new PointF(15.5f, 17);
            _destination = _curMode();
            _corner  = new Point(1, 32);
            _corner2 = new Point(8, 32);
        }

        public override Point ChaseMode()
        { 
            _goal = PacmanIsFar() ? _gameState.Pacman.GetLoc() :
                (GetLocF().IsOnXandY(_corner, 2f)) ? _corner2 : _corner;
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetRandomNeighboringPoint() : _path[1];
        }

        bool PacmanIsFar()
            => System.Math.Sqrt(System.Math.Pow(_gameState.Pacman.GetLoc().X - GetLoc().X, 2)
                       + System.Math.Pow(_gameState.Pacman.GetLoc().Y - GetLoc().Y, 2)) > 8;
    }
}
