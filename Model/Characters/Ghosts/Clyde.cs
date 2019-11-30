using System;

namespace Pacman
{
    public class Clyde : Ghost
    {
        public Clyde() : base()
        {
            SetSpeed(0.12f);
            ChangeMode(ScatterMode);
            _sprite._image = GameResources.Clyde;
            _color = System.Drawing.Color.Goldenrod;
            LocationF = _home = new PointF(15.5f, 17);
            _destination = _movingMode();
            _prevLoc = new Point(0, 0);
            _corner = new Point(1, 32);
            _corner2 = new Point(8, 32);
        }

        public override Point ChaseMode()
        {
            _goal = PacmanIsFar() ? _pacman.Location :
                (LocationF.IsOnXandY(_corner, 2f)) ? _corner2 : _corner;
            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);

            return (_path.Count == 1) ? Location : _path[1];
        }

        bool PacmanIsFar()
            => Math.Sqrt(Math.Pow(_pacman.Location.X - Location.X, 2)
                + Math.Pow(_pacman.Location.Y - Location.Y, 2)) > 8;
    }
}
