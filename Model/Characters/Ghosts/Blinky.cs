namespace Pacman 
{
    public class Blinky : Ghost
    {
        public Blinky(Mediator gameState) : base(gameState)
        {
            SetMode(ScatterMode);
            SetSpriteImage(_spriteImage = GameResources.Blinky);
            _color = System.Drawing.Color.Red;
            _locationF = _home = new PointF(13.5f, 14);
            _destination = _curMode();
            _corner = new Point(25, 4);
        }

        public override Point ChaseMode()
        {
            _goal = _gameState.Pacman.GetLoc();
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return PathExists ? _path[1] : GetLoc();
        }
    }
}