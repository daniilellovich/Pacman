using System.Collections.Generic;

namespace Pacman
{
    public class Blinky : Ghost
    {
        public Blinky(Pacman pacman, Level level) : base()
        {
            _pacman = pacman;
            _level = level;

            SetSpeed(0.12f);
            ChangeMode(ScatterMode);
            _sprite._image = GameResources.Blinky;
            LocationF = _home = new PointF(13.5f, 14); //как то обозначить что это конст
            _destination = _movingMode();
            _prevLocation = new Point(0, 0);
            _corner = new Point(21, 4);
            _corner2 = new Point(26, 4);
        }

        public override Point ChaseMode()
        {
            _goal = _pacman.Location;
            List<Point> path = _pathFinder.FindPath(_prevLocation, Location, _goal);
            return (path.Count == 1) ? Location : path[1];
        }
    }
}