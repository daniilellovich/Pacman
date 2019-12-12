namespace Pacman
{
    public class Pinky : Ghost
    {
        public Pinky() : base()
        {
            SetSpeed(0.12f);
            ChangeMode(ScatterMode);
            _sprite._image = GameResources.Pinky;
            _color = System.Drawing.Color.Pink;
            LocationF = _home = new PointF(13.5f, 17);
            _destination = _curMovingMode();
            _prevLoc = new Point();
            _corner = new Point(6, 4);
            _corner2 = new Point(1, 4);
        }

        public override Point ChaseMode()
        {
            int dx = 0, dy = 0;

            switch (_pacman.CurrentDir)
            {
                case Pacman.Directions.up:    dy = -5; break;
                case Pacman.Directions.right: dx =  5; break;
                case Pacman.Directions.down:  dy =  5; break;
                case Pacman.Directions.left:  dx = -5;  break;
            }

            for (int i = 0; i < 6; i++)
            {
                if (dx > 0) dx -= 1;
                if (dx < 0) dx += 1;

                if (dy > 0) dy -= 1;
                if (dy < 0) dy += 1;

                Point point = new Point(_pacman.Location.X + dx, _pacman.Location.Y + dy);

                if (_level.IsWalkablePoint(point))
                {
                    _goal = point;
                    break;
                }
            }

            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);
            return (_path.Count == 1) ? Location : _path[1];
        }
    }

}
