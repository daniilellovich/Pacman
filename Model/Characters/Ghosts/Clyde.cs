using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Clyde : Ghost
    {
        public Clyde(Pacman pacman, Level level) : base()
        {
            _pacman = pacman;
            _level = level;

            SetSpeed(0.12f);
            SetSprite(GameResources.Clyde);
            ChangeMode(ScatterMode);
            LocationF = _home = new PointF(15.5f, 17);
            _destination = _movingMode();
            _prevLocation = new Point(0, 0);
            _corner = new Point(1, 32);
            _corner2 = new Point(8, 32);
        }

        public override Point ChaseMode()
        {
            _goal = PacmanIsFar() ? _pacman.Location :
                (LocationF.IsOnXandY(_corner, 2f)) ? _corner2 : _corner;

            List<Point> path = _pathFinder.FindPath(_prevLocation, Location, _goal);

            return (path.Count == 1) ? Location : path[1];
        }

        bool PacmanIsFar()
            => Math.Sqrt(Math.Pow(_pacman.Location.X - Location.X, 2)
                + Math.Pow(_pacman.Location.Y - Location.Y, 2)) > 8;
    }
}
