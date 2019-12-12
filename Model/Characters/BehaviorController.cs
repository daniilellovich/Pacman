using System.Collections.Generic;
using System.Diagnostics;

namespace Pacman
{
    public class GhostsController
    {
        Pacman _pacman = Game.State.Pacman;
        List<Ghost> _ghosts = Game.State.Ghosts;

        int[] _events = new int[7];

        public GhostsController(int level)
        {            
            switch (level)
            {
                case 1:
                    _events = new int[7] { 7, 27, 34, 54, 59, 79, 84 };
                    _pacman.SetSpeed(0.1f); SetGhotstsSpeed(0.12f);
                    break;
                case 2:
                    _events = new int[7] { 7, 27, 34, 54, 59, 1092, 1093 };
                    _pacman.SetSpeed(0.11f); SetGhotstsSpeed(0.13f);
                    break;
                case 5:
                    _events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    _pacman.SetSpeed(0.12f); SetGhotstsSpeed(0.14f);
                    break;
                case 21:
                    _events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    _pacman.SetSpeed(0.11f); SetGhotstsSpeed(0.13f);
                    break;
            }
        }

        void SetGhotstsSpeed(float speed)
        {
            foreach (var ghost in _ghosts)
                ghost.SetSpeed(speed);
        }

        public void BehaviorEvents(int sec)
        {
            if (sec == _events[0] || sec == _events[2] ||
                sec == _events[4] || sec == _events[6])
            {
                foreach (var ghost in _ghosts)
                    ghost.ChangeMode(ghost.ChaseMode);
            }

            if (sec == _events[1] || sec == _events[3] || 
                sec == _events[5])
            {
                foreach (var ghost in _ghosts)
                    ghost.ChangeMode(ghost.ScatterMode);
            }
        }
    }
}