namespace Pacman
{
    public class Pinky : Ghost
    {
        public Pinky(Mediator gameState) : base(gameState)
        {
            SetSpeed(0.12f);
            SetMode(ScatterMode);
            _sprite._image = GameResources.Pinky;
            _color = System.Drawing.Color.Pink;
            _locationF = _home = new PointF(13.5f, 17);
            _destination = _curMovingMode();
            _prevLoc = new Point();
            _corner  = new Point(6, 4);
            _corner2 = new Point(1, 4);
        }

        public override Point ChaseMode()
        {
            int dx = 0, dy = 0;

            //точка, стоящая на 5 клеток впереди Пакмена
            switch (_gameState.Pacman.CurrentDir)
            {
                case Pacman.Directions.up:    dy = -5; break;
                case Pacman.Directions.right: dx =  5; break;
                case Pacman.Directions.down:  dy =  5; break;
                case Pacman.Directions.left:  dx = -5; break;
            }

            //поиск проходимой точки впереди пакмена
            for (int i = 0; i < 6; i++)
            {
                if (dx > 0) dx -= 1;
                if (dx < 0) dx += 1;

                if (dy > 0) dy -= 1;
                if (dy < 0) dy += 1;

                Point point = new Point(_gameState.Pacman.GetLoc().X + dx, 
                                        _gameState.Pacman.GetLoc().Y + dy);

                if (_gameState.Level.IsWalkablePoint(point))
                {
                    _goal = point;
                    break;
                }
            }

            _path = _gameState.GhostPathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetLoc() : _path[1]; //поменять на случайную точку
        }
    }

}
