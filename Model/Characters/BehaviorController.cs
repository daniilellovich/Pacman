using System.Collections.Generic;
using System.Diagnostics;

namespace Pacman
{
    public class BehaviorController
    {
        Pacman _pacman = Game.State.Pacman;
        List<Ghost> _ghosts = Game.State.Ghosts;


        enum ghostModes { Scatter, Chase, Frightened};
        ghostModes _globalCurrentMode = ghostModes.Scatter;
        int[] _events = new int[7];

        public BehaviorController(int level)
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

        void ChangeGhostsMode(ghostModes movingMode)
        {
            foreach (Ghost ghost in _ghosts)
            {
                ghost.ResetPrevLoc();

                if(movingMode == ghostModes.Scatter)
                    ghost.ChangeMode(ghost.ScatterMode);
                if (movingMode == ghostModes.Chase)
                    ghost.ChangeMode(ghost.ChaseMode);
                if (movingMode == ghostModes.Frightened)
                    ghost.ChangeMode(ghost.FrightenedMode);                
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
                ChangeGhostsMode(ghostModes.Chase);
                _globalCurrentMode = ghostModes.Chase;
            }

            if (sec == _events[1] || sec == _events[3] || 
                sec == _events[5])
            {
                ChangeGhostsMode(ghostModes.Scatter);
                _globalCurrentMode = ghostModes.Scatter;
            }
        }
    }
}