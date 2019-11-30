using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Inky : Ghost
    {
        Ghost _blinky = Game.State.Blinky;

        public Inky() : base()
        {
            SetSpeed(0.12f);
            ChangeMode(ScatterMode);
            _sprite._image = GameResources.Inky;
            _color = System.Drawing.Color.Aqua;
            LocationF = _home = new PointF(11.5f, 17);
            _destination = _movingMode();
            _prevLoc = new Point(0, 0);
            _corner = new Point(26, 32);
            _corner2 = new Point(18, 32);
        }

        public override Point ChaseMode()
        {
            (int X, int Y) = _pacman.Location;
            Point pacLocForInky = new Point(X, Y);
            switch (_pacman.CurrentDir)
            {
                case Pacman.Directions.up:
                    pacLocForInky = new Point(X, Y - 2);
                    break;
                case Pacman.Directions.right:
                    pacLocForInky = new Point(X + 2, Y);
                    break;
                case Pacman.Directions.down:
                    pacLocForInky = new Point(X, Y - 2);
                    break;
                case Pacman.Directions.left:
                    pacLocForInky = new Point(X - 2, Y);
                    break;
            }

            Point lastPoint = new Point(2 * pacLocForInky.X - _blinky.Location.X, 2 * pacLocForInky.Y - _blinky.Location.Y);

            List<Point> possiblePoints = GetPoints(Location, lastPoint);
            possiblePoints.Reverse();

            for (int i = 0; i < possiblePoints.Count; i++)
                if (_level.IsWalkablePoint(possiblePoints[i]))
                {
                    _goal = possiblePoints[i];
                    break;
                }

            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);
            return (_path.Count == 1) ? Location : _path[1];
        }

        List<Point> GetPoints(Point a, Point b)
        {
            (int x1, int y1) = a; (int x2, int y2) = b;

            List<Point> pointsOnLine = new List<Point>();

            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int signX = x1 < x2 ? 1 : -1;
            int signY = y1 < y2 ? 1 : -1;

            int error = deltaX - deltaY;

            pointsOnLine.Add(new Point(x2, y2));

            while (x1 != x2 || y1 != y2)
            {
                pointsOnLine.Add(new Point(x1, y1));
                int error2 = error * 2;

                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += signX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += signY;
                }
            }

            return pointsOnLine;
        }
    }
}