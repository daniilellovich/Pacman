using System.Windows.Forms;

namespace Pacman
{
    public class Pacman : Character
    {
        public enum Directions { nowhere, up, right, down, left }
        public int DotsEaten { get; set; }
        public int Lifes { get; private set; } = 3;

        //public Pacman(float speed)
        //{
        //    LocationF = new PointF(13.5f, 26);
        //    SetSprite(GameResources.Pacman);
        //    SetSpeed(speed);
        //}

        public Pacman(Mediator mediator) : base(mediator)
        {
            _locationF = new PointF(13.5f, 26);
            SetSprite(GameResources.Pacman);
        }

        public Directions CurrentDir { get; private set; }
        Directions _nextDirection;

        public override void Eaten()
        {
            //SoundController.StopLongSound();
            //SoundController.PlaySound("PacmanEaten");
            //Lifes--;

            //switch (Lifes)
            //{
            //    case 0:
            //        Game.GameOver();
            //        break;
            //    case 1:
            //        _gameState.Level.Tiles[0, 34] = new Floor(new Point(4, 34));
            //        break;
            //    case 2:
            //        _gameState.Level.Tiles[2, 34] = new Floor(new Point(4, 34));
            //        break;
            //}
        }

        public override void Update()
        {
            _nextDirection = UpdateDirIfNewer();
            Point destination = GetNextLocation(_nextDirection);

            int dx = !GetLocF().IsOnX(destination, 0.06f) ? ((GetLocF().X < destination.X) ? 1 : -1) : 0;
            int dy = !GetLocF().IsOnY(destination, 0.06f) ? ((GetLocF().Y < destination.Y) ? 1 : -1) : 0;

            Move(dx, dy);
        }

        bool DirIsValid(Directions dir) =>
            dir switch
            {                
                Directions.up    => _gameState.Level.IsWalkableForPacman(GetLoc().Up),
                Directions.right => _gameState.Level.IsWalkableForPacman(GetLoc().Right),
                Directions.down  => _gameState.Level.IsWalkableForPacman(GetLoc().Down),
                Directions.left  => _gameState.Level.IsWalkableForPacman(GetLoc().Left),
                _                => false,
             };             

        Point GetNextLocation(Directions receivedDir)
        {
            CurrentDir = (DirIsValid(receivedDir)) ? 
                receivedDir : !DirIsValid(CurrentDir) ? 
                Directions.nowhere : CurrentDir;

            switch (CurrentDir)
            {
                case Directions.up:    return GetLoc().Up;
                case Directions.right: return GetLoc().Right;
                case Directions.down:  return GetLoc().Down;
                case Directions.left:  return GetLoc().Left;
            }

            return GetLoc();
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

