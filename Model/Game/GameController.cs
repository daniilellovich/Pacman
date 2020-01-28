using System;
using System.Timers;
using System.Windows.Forms;

namespace Pacman
{
    public class GameController
    {
        Mediator _gameState;
        private int[] _events = new int[7];
        private int _seconds = 0;
        private int _frightTime;

        public void SetFrightTime(int level)
        {
            _frightTime = level switch
            {
                1 => 6, 2 => 5, 3 => 4, 4 => 3, 5 => 2, 6 => 5, 7 => 2,
                8 => 2, 9 => 1, 10 => 5, 11 => 2, 12 => 1, 13 => 1,
                14 => 3, 15 => 1, _ => 1,
            };
        }

        public void CheckIfGameIsOver()
        {
       //     if (_gameState.Pacman.GetLives() == 0)
              //  Game.IsOver();
        }

        public GameController(Mediator gamestate)
        {
            _gameState = gamestate;
            SetFrightTime(Game.level);

            switch (Game.level)
            {
                case 1:
                    _events = new int[7] { 7, 27, 34, 54, 59, 79, 84 };
                    _gameState.Pacman.SetSpeed(0.1f); SetGhotstsSpeed(0.12f);
                    break;
                case 2:
                    _events = new int[7] { 7, 27, 34, 54, 59, 1092, 1093 };
                    _gameState.Pacman.SetSpeed(0.11f); SetGhotstsSpeed(0.13f);
                    break;
                case 5:
                    _events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    _gameState.Pacman.SetSpeed(0.12f); SetGhotstsSpeed(0.14f);
                    break;
                case 21:
                    _events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    _gameState.Pacman.SetSpeed(0.11f); SetGhotstsSpeed(0.13f);
                    break;
            }
        }

        private void SetGhotstsSpeed(float speed)
            => _gameState.Ghosts.ForEach(g => g.SetSpeed(speed));

        public void BehaviorEvents()
        {
            _seconds++;

            if (_seconds == _events[0] || _seconds == _events[2] ||
                _seconds == _events[4] || _seconds == _events[6])
                _gameState.Ghosts.ForEach(g => g.SetMode(g.GlobalMovingMode = g.ChaseMode));
            
            if (_seconds == _events[1] || _seconds == _events[3] ||
                _seconds == _events[5])
                _gameState.Ghosts.ForEach(g => g.SetMode(g.GlobalMovingMode = g.ScatterMode));
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

        public void SetFrightendTimer()
        {
            var frightTimer = new System.Timers.Timer(_frightTime * 1000);
            frightTimer.Elapsed += OnFrightModeEnded;
            frightTimer.Enabled = true;
            frightTimer.AutoReset = false;

            var flashTimer = new System.Timers.Timer(_frightTime * 700);
            flashTimer.Elapsed += OnFlashTimeStarted;
            flashTimer.Enabled = true;
            flashTimer.AutoReset = false;
        }

        private void OnFrightModeEnded(Object source, ElapsedEventArgs e)
        {
      //      _gameState.Ghosts.ForEach(g => g.SetState(Ghost.State.ChaseOrScatter));
            _gameState.Ghosts.ForEach(g => g.SetMode(g.GlobalMovingMode));
        }

        private void OnFlashTimeStarted(Object source, ElapsedEventArgs e)
            => _gameState.Ghosts.ForEach(g => g.StartBlinking());
    }
}