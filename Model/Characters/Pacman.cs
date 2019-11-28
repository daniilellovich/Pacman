using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public class Pacman : Character
    {
        Level _level;

        public enum Directions { nowhere, up, right, down, left }

        public int DotsEaten { get; set; }

        public int Lifes { get; private set; }

        public Pacman(float speed, Level level)
        {
            LocationF = new PointF(13.5f, 26); //pacmanHome;
            SetSprite(GameResources.Pacman);
            SetSpeed(speed);
            _level = level;
        }

        public Directions CurrentDir { get; private set; }
        Directions _nextDirection;

        public override void Update()
        {
            _nextDirection = UpdateDirIfNewer(_nextDirection);
            Point destination = GetNextLocation(_nextDirection);

            int dx = !LocationF.IsOnX(destination, 0.06f) ? ((LocationF.X < destination.X) ? 1 : -1) : 0;
            int dy = !LocationF.IsOnY(destination, 0.06f) ? ((LocationF.Y < destination.Y) ? 1 : -1) : 0;

            Move(dx, dy);
        }

        bool DirIsValid(Directions dir)
        {
            (int X, int Y) = LocationF.ToPoint();

            switch (dir)
            {
                case Directions.up:    return _level.IsWalkablePoint(new Point(X, Y - 1));
                case Directions.right: return _level.IsWalkablePoint(new Point(X + 1, Y));
                case Directions.down:  return _level.IsWalkablePoint(new Point(X, Y + 1));
                case Directions.left:  return _level.IsWalkablePoint(new Point(X - 1, Y));
            }
           
            return false;
        }

        Point GetNextLocation(Directions receivedDir)
        {
            (int X, int Y) = Location;

            CurrentDir = (DirIsValid(receivedDir)) ? 
                receivedDir : !DirIsValid(CurrentDir) ? 
                Directions.nowhere : CurrentDir;

            switch (CurrentDir)
            {
                case Directions.up:    return new Point(X, Y - 1);
                case Directions.right: return new Point(X + 1, Y);
                case Directions.down:  return new Point(X, Y + 1);
                case Directions.left:  return new Point(X - 1, Y);
            }

            return new Point(X, Y);
        }

        Directions UpdateDirIfNewer(Directions oldDirection)
        {
            if (Keyboard.IsKeyDown(Keys.Up) || Keyboard.IsKeyDown(Keys.W))
                return Directions.up;
            if (Keyboard.IsKeyDown(Keys.Right) || Keyboard.IsKeyDown(Keys.D))
                return Directions.right;
            if (Keyboard.IsKeyDown(Keys.Down) || Keyboard.IsKeyDown(Keys.S))
                return Directions.down;
            if (Keyboard.IsKeyDown(Keys.Left) || Keyboard.IsKeyDown(Keys.A))
                return Directions.left;

            return oldDirection;
        }
    }
}

