using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public class Pacman : Character
    {
        public enum Directions { nowhere, up, right, down, left }
        Directions _dir;
        public Directions _lastDirection { get; private set; } //почему не _dir

     //   int _lives = 3;

        public int _dotsEaten;  //change to private

        public Pacman(PointF location, float speed)
        {
            Home = new PointF(13.5f, 26);
            Sprite = new Sprite(GameResources.Pacman);
            Location = location;
            Speed = speed;
        }

        public override void Update()
        {
            _dir = UpdateDirIfNewer(_dir);
            Destination = GetNextLocation(_dir);

            int dx = !Location.IsOnX(Destination) ? ((Location.X < Destination.X) ? 1 : -1) : 0;
            int dy = !Location.IsOnY(Destination) ? ((Location.Y < Destination.Y) ? 1 : -1) : 0;

            Move(dx, dy);
        }

        Point GetNextLocation(Directions receivedDir)
        {
            Point pacmanLocation = Location.ToPoint();
            (int X, int Y) = pacmanLocation;

            Directions curDir = (DirIsValid(receivedDir)) ? _lastDirection = receivedDir 
                : (DirIsValid(_lastDirection)) ? _lastDirection : Directions.nowhere;

            switch (curDir)
            {
                case Directions.nowhere: return pacmanLocation;
                case Directions.up:      return new Point(X, Y - 1);
                case Directions.right:   return new Point(X + 1, Y);
                case Directions.down:    return new Point(X, Y + 1);
                case Directions.left:    return new Point(X - 1, Y);
            }

            return pacmanLocation;
        }

        bool DirIsValid(Directions dir)
        {
            Point point = Location.ToPoint();
            (int X, int Y) = point;

            switch (dir)
            {
                case Directions.up:    return Game.State.Level.Tiles[X, Y - 1].IsWalkable;
                case Directions.right: return Game.State.Level.Tiles[X + 1, Y].IsWalkable;
                case Directions.down:  return Game.State.Level.Tiles[X, Y + 1].IsWalkable;
                case Directions.left:  return Game.State.Level.Tiles[X - 1, Y].IsWalkable;
            }

            return false;
        }

        Directions UpdateDirIfNewer(Directions oldDirection)
        {
            if ((Keyboard.IsKeyDown(Keys.W)) || (Keyboard.IsKeyDown(Keys.Up)))
                return Directions.up;
            if ((Keyboard.IsKeyDown(Keys.D)) || (Keyboard.IsKeyDown(Keys.Right)))
                return Directions.right;
            if ((Keyboard.IsKeyDown(Keys.S)) || (Keyboard.IsKeyDown(Keys.Down)))
                return Directions.down;
            if ((Keyboard.IsKeyDown(Keys.A)) || (Keyboard.IsKeyDown(Keys.Left)))
                return Directions.left;

            return oldDirection;
        }
    }
}