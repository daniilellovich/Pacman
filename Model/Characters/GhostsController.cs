using System.Windows.Forms;

namespace Pacman
{
    public class GhostsController
    {
        Mediator _gameState;

        int[] _events = new int[7];
        int seconds = 0;

        public GhostsController(Mediator gamestate)
            => _gameState = gamestate;

        public GhostsController(int level)
        {
            switch (level)
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

        void SetGhotstsSpeed(float speed)
        {
            foreach (var ghost in _gameState.Ghosts)
                ghost.SetSpeed(speed);
        }

        public void BehaviorEvents()
        {
            seconds++;

            if (seconds == _events[0] || seconds == _events[2] ||
                seconds == _events[4] || seconds == _events[6])
            {
                foreach (var ghost in _gameState.Ghosts)
                    ghost.SetMode(ghost.ChaseMode);
            }

            if (seconds == _events[1] || seconds == _events[3] ||
                seconds == _events[5])
            {
                foreach (var ghost in _gameState.Ghosts)
                    ghost.SetMode(ghost.ScatterMode);
            }
        }

        public void SwitchPathDrawing(Keys pressedKey)
        {
            switch (pressedKey)
            {
                case Keys.B: _gameState.Blinky.SwitchPathVisibility(); break;
                case Keys.P: _gameState.Pinky.SwitchPathVisibility();  break;
                case Keys.I: _gameState.Inky.SwitchPathVisibility();   break;
                case Keys.C: _gameState.Clyde.SwitchPathVisibility();  break;
            }
        }
    }
}