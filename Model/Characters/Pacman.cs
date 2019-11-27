using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public class Pacman : Character
    {
        public enum Directions { nowhere, up, right, down, left }

        public int DotsEaten { get; set; }

        public int Lifes { get; private set; }

        public Pacman(PointF location, float speed)
        {
            Home = new PointF(13.5f, 26);
            Sprite = new Sprite(GameResources.Pacman);
            Location = location;
            Speed = speed;
        }

        public Directions CurrentDir { get; private set; }
        Directions _nextDirection;

        public override void Update()
        {
            _nextDirection = UpdateDirIfNewer(_nextDirection);
            Point destination = GetNextLocation(_nextDirection);

            int dx = !Location.IsOnX(destination) ? ((Location.X < destination.X) ? 1 : -1) : 0;
            int dy = !Location.IsOnY(destination) ? ((Location.Y < destination.Y) ? 1 : -1) : 0;

            Move(dx, dy);
        }

        bool DirIsValid(Directions dir)
        {
            (int X, int Y) = Location.ToPoint();

            switch (dir)
            {
                case Directions.up: return Game.State.Level.Tiles[X, Y - 1].IsWalkable;
                case Directions.right: return Game.State.Level.Tiles[X + 1, Y].IsWalkable;
                case Directions.down: return Game.State.Level.Tiles[X, Y + 1].IsWalkable;
                case Directions.left: return Game.State.Level.Tiles[X - 1, Y].IsWalkable;
            }

            return false;
        }

        Point GetNextLocation(Directions receivedDir)
        {
            (int X, int Y) = Location.ToPoint();

            CurrentDir = (DirIsValid(receivedDir)) ? 
                receivedDir : !DirIsValid(CurrentDir) ? 
                Directions.nowhere : CurrentDir;

            switch (CurrentDir)
            {
                case Directions.up: return new Point(X, Y - 1);
                case Directions.right: return new Point(X + 1, Y);
                case Directions.down: return new Point(X, Y + 1);
                case Directions.left: return new Point(X - 1, Y);
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

