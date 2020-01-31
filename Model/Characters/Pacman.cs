using System.Windows.Forms;

namespace Pacman
{
    public class Pacman : Character
    {
        public enum Directions { nowhere, up, right, down, left }
        private Directions _currentDir, _nextDir;
        private int _dotsEaten, _eatenGhostsCounter, _lives;

        public Directions GetCurDir()
            => _currentDir;

        public int GetLives()
            => _lives;

        public int GetDots()
            => _dotsEaten;

        public int GetEatenGhostsCounter()
            => _eatenGhostsCounter;

        public Pacman(Mediator gameState, int lives) : base(gameState)
        {
            _lives = lives;
            _locationF = new PointF(13.5f, 26);
            SetSpriteImage(GameResources.Pacman);
            SetCurSpeed(_normalSpeed);
        }

        public override void Update()
            => MoveTo(GetNextLocation());

        public override void Eaten()
            => _lives-=1;
        
        public void EatDot()
            => _dotsEaten++;

        public void EatGhost()
            => _eatenGhostsCounter++;

        public void ResetGhostCounter()
            => _eatenGhostsCounter = 0;

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
            => dir switch
            {
                Directions.up    => GetLoc().Up,
                Directions.right => GetLoc().Right,
                Directions.down  => GetLoc().Down,
                Directions.left  => GetLoc().Left,
                _                => GetLoc(),
            };

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