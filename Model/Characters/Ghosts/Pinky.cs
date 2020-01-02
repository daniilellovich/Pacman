namespace Pacman
{
    public class Pinky : Ghost
    {
        public Pinky(Mediator gameState) : base(gameState)
        {
            SetSpeed(0.12f);
            SetMode(ScatterMode);
            SetSprite(GameResources.Pinky);
            _color = System.Drawing.Color.Pink;
            _locationF = _home = new PointF(13.5f, 17);
            _destination = _curMode();
            _corner  = new Point(6, 4);
            _corner2 = new Point(1, 4);
        }

        public override Point ChaseMode()
        {   
            _goal = FindWalkableFontPoint(GetPacmanFrontPoint());            
            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetRandomNeighboringPoint() : _path[1];
        }

        Point GetPacmanFrontPoint()
        {
            int dx = 0, dy = 0;
            switch (_gameState.Pacman.CurrentDir)
            {
                case Pacman.Directions.up:    dy = -5; break;
                case Pacman.Directions.right: dx =  5; break;
                case Pacman.Directions.down:  dy =  5; break;
                case Pacman.Directions.left:  dx = -5; break;
            }
            return new Point (dx, dy);
        }

        Point FindWalkableFontPoint(Point frontPoint)
        {
            (int dx, int dy) = frontPoint;

            for (int i = 0; i < 6; i++)
            {
                if (dx > 0) dx -= 1;
                if (dx < 0) dx += 1;

                if (dy > 0) dy -= 1;
                if (dy < 0) dy += 1;

                Point point = new Point(_gameState.Pacman.GetLoc().X + dx,
                                        _gameState.Pacman.GetLoc().Y + dy);

                if (_gameState.Level.IsWalkableForGhost(point))
                    return point;
            }
            return GetLoc();
        }
    }

}
