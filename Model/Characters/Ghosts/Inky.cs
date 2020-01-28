using System;
using System.Collections.Generic;

namespace Pacman
{
    public class Inky : Ghost
    {
        public Inky(Mediator gameState) : base(gameState)
        {
            SetSpeed(0.12f);
            SetMode(ScatterMode);
            SetSpriteImage(_spriteImage = GameResources.Inky);
            _color = System.Drawing.Color.Aqua;
            _locationF = _home = new PointF(11.5f, 17);
            _destination = _curMode();
            _corner = new Point(26, 32);
        }

        public override Point ChaseMode()
        {   
            Point finishPoint = GetFinishPoint(GetPointForBlinky());
            List<Point> possiblePoints = RasterizePath(GetLoc(), finishPoint);          
            _goal = GetGoal(possiblePoints);

            _path = _pathFinder.FindPath(_prevLoc, GetLoc(), _goal);
            return PathExists ? _path[1] : GetWalkableNeighbourPoint();
        }

        private Point GetGoal(List<Point> possiblePoints)
        {
            for (int i = 0; i < possiblePoints.Count; i++)
                if (_gameState.Level.IsWalkableForGhost(possiblePoints[i]))
                    return possiblePoints[i];               
            return GetLoc();
        }

        private Point GetPointForBlinky()
        {   
            (int X, int Y) = _gameState.Pacman.GetLoc();
            switch (_gameState.Pacman.GetCurDir())
            {
                case Pacman.Directions.up:    return new Point(X, Y - 2);
                case Pacman.Directions.right: return new Point(X + 2, Y);
                case Pacman.Directions.down:  return new Point(X, Y - 2);
                case Pacman.Directions.left:  return new Point(X - 2, Y);
                default:                      return new Point(X, Y);
            }
        }

        private Point GetFinishPoint(Point pacLocForBlinky)
            => new Point(2 * pacLocForBlinky.X - _gameState.Blinky.GetLoc().X,
                         2 * pacLocForBlinky.Y - _gameState.Blinky.GetLoc().Y);

        private List<Point> RasterizePath(Point a, Point b)
        {   //Bresenham's line algorithm
            //to get farthest walkable point
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

            pointsOnLine.Reverse();
            return pointsOnLine;
        }
    }
}