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
        public Inky(Mediator gameState) : base(gameState)
        {
            SetSpeed(0.12f);
            SetMode(ScatterMode);
            _sprite._image = GameResources.Inky;
            _color = System.Drawing.Color.Aqua;
            _locationF = _home = new PointF(11.5f, 17);
            _destination = _curMovingMode();
            _prevLoc = new Point(0, 0);
            _corner = new Point(26, 32);
            _corner2 = new Point(18, 32);
        }

        public override Point ChaseMode()
        {
            (int X, int Y) = _gameState.Pacman.GetLoc();
            Point pacLocForBlinky = new Point(X, Y);
            switch (_gameState.Pacman.CurrentDir)
            {
                case Pacman.Directions.up:    pacLocForBlinky = new Point(X, Y - 2); break;
                case Pacman.Directions.right: pacLocForBlinky = new Point(X + 2, Y); break;
                case Pacman.Directions.down:  pacLocForBlinky = new Point(X, Y - 2); break;
                case Pacman.Directions.left:  pacLocForBlinky = new Point(X - 2, Y); break;
            }

            Point lastPoint = new Point(2 * pacLocForBlinky.X - _gameState.Blinky.GetLoc().X,
                                        2 * pacLocForBlinky.Y - _gameState.Blinky.GetLoc().Y);

            List<Point> possiblePoints = RasterizePath(GetLoc(), lastPoint);
            possiblePoints.Reverse();

            for (int i = 0; i < possiblePoints.Count; i++)
                if (_gameState.Level.IsWalkablePoint(possiblePoints[i]))
                {
                    _goal = possiblePoints[i];
                    break;
                }

            _path = _gameState.GhostPathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return (_path.Count == 1) ? GetLoc() : _path[1];
        }

        List<Point> RasterizePath(Point a, Point b) //Алгоритм Брезенхема
        {
            (int x1, int y1) = a; (int x2, int y2) = b;

            List<Point> pointsOnLine = new List<Point>();

            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int signX  = x1 < x2 ? 1 : -1;
            int signY  = y1 < y2 ? 1 : -1;

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