using System;
using System.Threading;

namespace Pacman
{
    public class ColissionDetector
    {
        Mediator _gameState;

        public ColissionDetector(Mediator gameState)
            => _gameState = gameState;

        public void Update()
        {
            CheckIntersectionWithGhost();
            CheckIfDotEaten();
            CheckIfEnergizerEaten();
            CheckIfFruitEaten();
        }

        void CheckIntersectionWithGhost()
        {
            foreach (var ghost in _gameState.Ghosts)
                if (_gameState.Pacman.IntersectsWith(ghost))
                    if (ghost.GetMode() != ghost.ReturningHome)
                    {
                        if (ghost.GetMode() == ghost.FrightenedMode)
                        {
                            ghost.Eaten();
                            _gameState.Pacman.EatGhost();
                            _gameState.Game.score += _gameState.Pacman.GetEatenGhostsCounter() * 200;
                            SoundController.PlaySound("MonsterEaten");
                            Thread.Sleep(51);
                        }
                        else
                        {
                            _gameState.Pacman.Eaten();
                            _gameState.GameController.ResetCharacters();
                            _gameState.Level.RemoveOnePacmanLifeDownside(_gameState.Pacman.GetLives());
                            SoundController.PlaySound("PacmanEaten");
                            Thread.Sleep(500);
                        }
                    }
        }

        void CheckIfDotEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Dot)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                _gameState.Pacman.EatDot();
                _gameState.Game.score += 10;
                _gameState.GameController.CheckEatenDots(_gameState.Pacman.GetDots());
                SoundController.PlaySound("DotEaten");
                Thread.Sleep(17);
            }
        }

        void CheckIfEnergizerEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Energizer)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                _gameState.Game.score += 50;
                _gameState.GameController.SetGhostsFrightenedMode();
                SoundController.PlaySound("EnergizerEaten");
                Thread.Sleep(51);        
            }
        }

        void CheckIfFruitEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Fruit)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                _gameState.Game.score += _gameState.GameController.GetFruit().GetScore();
                SoundController.PlaySound("FruitEaten");
                Thread.Sleep(51);
            }
        }
    }
}