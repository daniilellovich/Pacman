using System;
using System.Threading;

namespace Pacman
{
    public class ItemsController
    {
        Mediator _gameState;

        public ItemsController(Mediator gameState)
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
                    if (ghost.GetState() != Ghost.State.Returning &&
                        !_gameState.Pacman.IsEaten) //to avoid 'sticking' sound
                    {
                        if (ghost.GetState() == Ghost.State.Fright)
                        {
                            ghost.Eaten();
                            SoundController.PlaySound("MonsterEaten");
                        }
                        else
                        {
                            _gameState.Pacman.Eaten();
                            _gameState.Level.RemoveOnePacmanLife(_gameState.Pacman.GetLives());
                            SoundController.PlaySound("PacmanEaten");
                        }
                        Thread.Sleep(51);
                    }
        }

        void CheckIfDotEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Dot)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                _gameState.Pacman.EatDot();
                SoundController.PlaySound("DotEaten");
                Thread.Sleep(17);
            }
        }

        void CheckIfEnergizerEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Energizer)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                _gameState.Pacman.EatDot();
                SoundController.PlaySound("EnergizerEaten");
                _gameState.GameController.SetFrightenedMode();
                Thread.Sleep(51);        
            }
        }

        void CheckIfFruitEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Fruit)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                Game.score += _gameState.GameController.GetFruit().Score;
                SoundController.PlaySound("FruitEaten");
                Thread.Sleep(51);
            }
        }
    }
}