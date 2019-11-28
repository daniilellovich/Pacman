using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Inky : Ghost
    {
        Point _blinkyLocation;
        public Inky(Pacman pacman, Level level) : base()
        {
            _pacman = pacman;
            _level = level;

            SetSpeed(0.12f);
            SetSprite(GameResources.Inky);
            ChangeMode(ScatterMode);
            LocationF = _home = new PointF(11.5f, 17); //как то обозначить что это конст
            _destination = _movingMode();
            _prevLocation = new Point(0, 0);
            _corner = new Point(26, 32);
            _corner2 = new Point(18, 32);

            _blinkyLocation = Game.State.Blinky.Location; //переместить
        }

        public override Point ChaseMode()
        {
            (int X, int Y) = _pacman.Location;
            Point pacmanLocForInky = new Point();

            switch (_pacman.CurrentDir)
            {
                case Pacman.Directions.up:
                    pacmanLocForInky = new Point(X - 2, Y);
                    break;
                case Pacman.Directions.right:
                    pacmanLocForInky = new Point(X, Y + 2);
                    break;
                case Pacman.Directions.down:
                    pacmanLocForInky = new Point(X + 2, Y);
                    break;
                case Pacman.Directions.left:
                    pacmanLocForInky = new Point(X, Y - 2);
                    break;
                case Pacman.Directions.nowhere:
                    pacmanLocForInky = new Point(X, Y);
                    break;
            }

            int xLine = pacmanLocForInky.X - _blinkyLocation.X;
            int yLine = pacmanLocForInky.Y - _blinkyLocation.Y;

            _goal = new Point(_blinkyLocation.X + 2 * xLine, _blinkyLocation.Y + 2 * yLine);

            if (!_level.IsWalkablePoint(_goal))
                _goal = _pacman.Location;

            if (LocationF.IsOnXorY(_goal, 0.5f))
                _goal = _pacman.Location;

            List<Point> path = _pathFinder.FindPath(_prevLocation, Location, _goal);
            return (path.Count == 1) ? Location : path[1];
        }
    }
}
