namespace Pacman
{
    public class Pinky : Ghost
    {
        public Pinky(Mediator gameState) : base(gameState)
        {
            SetMode(ScatterMode);         
            SetSpriteImage(_spriteImage = GameResources.Pinky);
            _color = System.Drawing.Color.Pink;
            _locationF = _home = new PointF(13.5f, 17);
            _destination = _curMode();
            _corner = new Point(2, 4);
        }

        public override Point ChaseMode()
        {   //his goal is the cell that is in front of Pac-Man through four tiles
            _goal = FindWalkablePoint(GetPacmanFront5Point());
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return PathExists ? _path[1] : GetWalkableNeighbourPoint();
        }

        private Point GetPacmanFront5Point()
        {
            int dx = 0, dy = 0;
            switch (_gameState.Pacman.GetCurDir())
            {
                case Pacman.Directions.up:    dy = -5; break;
                case Pacman.Directions.right: dx =  5; break;
                case Pacman.Directions.down:  dy =  5; break;
                case Pacman.Directions.left:  dx = -5; break;
            }
            return new Point(dx, dy);
        }

        private Point FindWalkablePoint(Point frontPoint)
        {   //gets farthest walkable point
            (int dx, int dy) = frontPoint;

            while (!(dx == 0 && dy == 0))
            {
                dx += (dx != 0) ? ((dx > 0) ? -1 : 1) : 0;
                dy += (dy != 0) ? ((dy > 0) ? -1 : 1) : 0;

                Point point = new Point(_gameState.Pacman.GetLoc().X + dx,
                                        _gameState.Pacman.GetLoc().Y + dy);

                if (_gameState.Level.IsWalkableForGhost(point))
                    return point;
            }

            return _gameState.Pacman.GetLoc();
        }
    }
}
