using System.Timers;
using System.Windows.Forms;
using System.Linq;

namespace Pacman
{
    public class GameController
    {
        private Mediator _gameState;
        private Fruit _fruit;
        private Game _game;
        private int[] _events = new int[7];
        private int _seconds = 0, _frightTime;
        private Ghost.MovingMode _globalMovingMode;
        private bool _ghostsAreFrightened;

        public Ghost.MovingMode GetGlobalMovingMode()
            => _globalMovingMode;

        public GameController(Game game, Mediator gamestate, int level)
        {
            _game = game;
            _gameState = gamestate;
            _frightTime = SetFrightTime(level);
            _fruit = ChooseFruit(level);

            switch (level)
            {
                case 1:
                    _events = new int[7] { 7, 27, 34, 54, 59, 79, 84 };
                    _gameState.Pacman.SetSpeed(0.1f); SetGhostsSpeed(0.11f);
                    break;
                case 2:
                    _events = new int[7] { 7, 27, 34, 54, 59, 1092, 1093 };
                    _gameState.Pacman.SetCurSpeed(0.11f); SetGhostsSpeed(0.12f);
                    break;
                case 5:
                    _events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    _gameState.Pacman.SetCurSpeed(0.12f); SetGhostsSpeed(0.13f);
                    break;
                case 21:
                    _events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    _gameState.Pacman.SetCurSpeed(0.11f); SetGhostsSpeed(0.12f);
                    break;
            }
        }

        public void CheckEatenDots(int dotsEaten)
        {
            //   if (dotsEaten == 10)
            if (dotsEaten == 3)
                _gameState.Characters.Add(_gameState.Pinky);

            //    if (dotsEaten == 30)
            if (dotsEaten == 3)

                _gameState.Characters.Add(_gameState.Inky);

            //    if (dotsEaten == 60)
            if (dotsEaten == 3)

                _gameState.Characters.Add(_gameState.Clyde);

                if (dotsEaten == 70 || dotsEaten == 170)
                _gameState.Level.PutNewFruit(_fruit);

            if (dotsEaten == 240)
            {
                _game.levelNumber++;
                _game = new Game();
            }
        }

        public void ResetCharacters()
        {
            _gameState = new Mediator(_game, _gameState.Level, _game.levelNumber);
        }

        public Fruit GetFruit()
            => _fruit;

        public int SetFrightTime(int level) => level switch
            {
                1 => 6, 2 => 5, 3 => 4, 4 => 3, 5 => 2, 6 => 5, 7 => 2,
                8 => 2, 9 => 1, 10 => 5, 11 => 2, 12 => 1, 13 => 1,
                14 => 3, 15 => 1, _ => 1,
            };

        public Fruit ChooseFruit(int level) => level switch
            { 
                1 => new Cherries(), 2 => new Strawberry(),  3 => new Peach(),
                4 => new Peach(), 5 => new Apple(), 6 => new Apple(),
                7 => new Grapes(), 8 => new Grapes(), 9 => new Galaxian(),
                10 => new Galaxian(), 11 => new Bell(),  12 => new Bell(),
                _ => new Key(),
            };

        private void SetGhostsSpeed(float speed)
            => _gameState.Ghosts.ForEach(g => g.SetSpeed(speed));

        public void SetGhostsFrightenedMode()
        {
           SetFrightendTimer();
            _gameState.Ghosts.ForEach(g => g.SetMode(g.FrightenedMode));
            _ghostsAreFrightened = true;
        }

        public void BehaviorEvents()
        {
            if(!_ghostsAreFrightened)
                _seconds++;

            if (_seconds == _events[0] || _seconds == _events[2] ||
                _seconds == _events[4] || _seconds == _events[6])
                _gameState.Ghosts.ForEach(g => g.SetMode(_globalMovingMode = g.ChaseMode));

            if (_seconds == _events[1] || _seconds == _events[3] ||
                _seconds == _events[5])
                _gameState.Ghosts.ForEach(g => g.SetMode(_globalMovingMode = g.ScatterMode));
        }

        public void SwitchPathDrawing(Keys pressedKey)
        {
            switch (pressedKey)
            {
                case Keys.B: _gameState.Blinky.SwitchPathVisibility(); break;
                case Keys.P: _gameState.Pinky.SwitchPathVisibility(); break;
                case Keys.I: _gameState.Inky.SwitchPathVisibility(); break;
                case Keys.C: _gameState.Clyde.SwitchPathVisibility(); break;
            }
        }

        #region timers
        public void SetFrightendTimer()
        {
            var frightTimer = new System.Timers.Timer(_frightTime * 1000);
            frightTimer.Elapsed += OnFrightTimeEnded;
            frightTimer.Enabled = true;
            frightTimer.AutoReset = false;

            var flashTimer = new System.Timers.Timer(_frightTime * 700);
            flashTimer.Elapsed += OnFlashTimeStarted;
            flashTimer.Enabled = true;
            flashTimer.AutoReset = false;
        }

        private void OnFrightTimeEnded(object source, ElapsedEventArgs e)
        {
            foreach (var ghost in _game.State.Ghosts)
            {
                if (ghost.GetMode() != ghost.ReturningHome)
                    ghost.SetMode(_globalMovingMode);
            }

            _ghostsAreFrightened = false;
            _gameState.Pacman.ResetGhostCounter();
        }

        private void OnFlashTimeStarted(object source, ElapsedEventArgs e)
            => _gameState.Ghosts.ForEach(g => g.StartBlinking());
        #endregion
    }
}