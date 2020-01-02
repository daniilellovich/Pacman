namespace Pacman
{
    public class Blinky : Ghost
    {
        public Blinky(Mediator gameState) : base(gameState)
        {
            SetSpeed(0.12f);
            SetMode(ScatterMode);
            SetSprite(GameResources.Blinky);
            _color = System.Drawing.Color.Red;
            _locationF = _home = new PointF(13.5f, 14);
            _destination = _curMode();
            _corner  = new Point(21, 4);
            _corner2 = new Point(26, 4);
        }

        public override Point ChaseMode()
        { 
            _goal = _gameState.Pacman.GetLoc();
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetRandomNeighboringPoint() : _path[1];
        }
    }
}