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

        bool DirIsValid(Directions dir) =>
            dir switch
            {                
                Directions.up    => _level.IsWalkablePoint(Location.Up),
                Directions.right => _level.IsWalkablePoint(Location.Right),
                Directions.down  => _level.IsWalkablePoint(Location.Down),
                Directions.left  => _level.IsWalkablePoint(Location.Left),
                _                => false,
             };             

        Point GetNextLocation(Directions receivedDir)
        {
            (int X, int Y) = Location;

            CurrentDir = (DirIsValid(receivedDir)) ? 
                receivedDir : !DirIsValid(CurrentDir) ? 
                Directions.nowhere : CurrentDir;

            switch (CurrentDir)
            {
                case Directions.up:    return Location.Up;
                case Directions.right: return Location.Right;
                case Directions.down:  return Location.Down;
                case Directions.left:  return Location.Left;
            }

            return Location;
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

