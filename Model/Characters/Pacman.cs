using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public class Pacman : Character
    {
        Level _level = Game.State.Level;

        public enum Directions { nowhere, up, right, down, left }

        public int DotsEaten { get; set; }

        public int Lifes { get; private set; } = 3;

        public Pacman(float speed)
        {
            LocationF = new PointF(13.5f, 26);
            SetSprite(GameResources.Pacman);
            SetSpeed(speed);
        }

        public Pacman()
        {
            LocationF = new PointF(13.5f, 26);
            SetSprite(GameResources.Pacman);
        }

        public Directions CurrentDir { get; private set; }
        Directions _nextDirection;

        public override void Eaten()
        {
            SoundController.StopLongSound();
            SoundController.PlaySound("PacmanEaten");
            Lifes--;

            switch (Lifes)
            {
                case 0:
                    Game.GameOver();
                    break;
                case 1:
                    _level.Tiles[0, 34] = new Floor(new Point(4, 34));
                    break;
                case 2:
                    _level.Tiles[2, 34] = new Floor(new Point(4, 34));
                    break;
            }
        }

        public override void Update()
        {
            _nextDirection = UpdateDirIfNewer();
            Point destination = GetNextLocation(_nextDirection);

            int dx = !LocationF.IsOnX(destination, 0.06f) ? ((LocationF.X < destination.X) ? 1 : -1) : 0;
            int dy = !LocationF.IsOnY(destination, 0.06f) ? ((LocationF.Y < destination.Y) ? 1 : -1) : 0;

            Move(dx, dy);
        }

        bool DirIsValid(Directions dir) =>
            dir switch
            {                
                Directions.up    => _level.IsWalkableForPacman(Location.Up),
                Directions.right => _level.IsWalkableForPacman(Location.Right),
                Directions.down  => _level.IsWalkableForPacman(Location.Down),
                Directions.left  => _level.IsWalkableForPacman(Location.Left),
                _                => false,
             };             

        Point GetNextLocation(Directions receivedDir)
        {
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

        Directions UpdateDirIfNewer()
        {
            if (Keyboard.IsKeyDown(Keys.Up) || Keyboard.IsKeyDown(Keys.W))
                return Directions.up;
            if (Keyboard.IsKeyDown(Keys.Right) || Keyboard.IsKeyDown(Keys.D))
                return Directions.right;
            if (Keyboard.IsKeyDown(Keys.Down) || Keyboard.IsKeyDown(Keys.S))
                return Directions.down;
            if (Keyboard.IsKeyDown(Keys.Left) || Keyboard.IsKeyDown(Keys.A))
                return Directions.left;

            return _nextDirection;
        }
    }
}

