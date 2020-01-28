using System.Windows.Forms;

namespace Pacman
{
    public class Pacman : Character
    {
        public enum Directions { nowhere, up, right, down, left }
        private Directions _currentDir, _nextDir;
        private int _dotsEaten, _lives = 3;
        public bool IsEaten = false;

        public Directions GetCurDir()
            => _currentDir;

        public int GetLives()
            => _lives;

        public int GetDots()
            => _dotsEaten;

        public Pacman(Mediator gameState) : base(gameState)
        {
            _locationF = new PointF(13.5f, 26);
            SetSpriteImage(GameResources.Pacman);
            SetSpeed(0.1f);
        }

        public override void Update()
            => MoveTo(GetNextLocation());

        public override void Eaten()
        {
            IsEaten = true;
            _lives--;
         //   new Pacman(_gameState);
        }

        public void EatDot()
            => _dotsEaten++;

        private Point GetNextLocation()
        {
            _currentDir = (DirIsValid(_nextDir)) ?
                         _nextDir : !DirIsValid(_currentDir) ?
                         Directions.nowhere : _currentDir;

            return GetPointByDir(_currentDir);
        }

        private bool DirIsValid(Directions dir)
            => _gameState.Level.IsWalkableForPacman(GetPointByDir(dir));

        private Point GetPointByDir(Directions dir)
        {
            switch (dir)
            {
                case Directions.up:    return GetLoc().Up;
                case Directions.right: return GetLoc().Right;
                case Directions.down:  return GetLoc().Down;
                case Directions.left:  return GetLoc().Left;
                default:               return GetLoc();
            }
        }

        public void GetNextDirFromKeyboard(Keys pressedKey)
        {
            if (pressedKey == Keys.Up || pressedKey == Keys.W)
                _nextDir = Directions.up;
            if (pressedKey == Keys.Right || pressedKey == Keys.D)
                _nextDir = Directions.right;
            if (pressedKey == Keys.Down || pressedKey == Keys.S)
                _nextDir = Directions.down;
            if (pressedKey == Keys.Left || pressedKey == Keys.A)
                _nextDir = Directions.left;
        }
    }
}