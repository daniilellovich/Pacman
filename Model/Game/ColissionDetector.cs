using System.Threading;

namespace Pacman
{
    public class ColissionDetector
    {
        Mediator _gameState;
        Game _game;

        public ColissionDetector(Game game, Mediator gameState)
        {
            _game = game;
            _gameState = gameState;
        }

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
                    if (ghost.GetCurMode() != ghost.ReturningHome)
                    {
                        if (ghost.GetCurMode() == ghost.FrightenedMode)
                        {
                            ghost.Eaten();
                            _gameState.Pacman.EatGhost();
                            _gameState.Game.Score += _gameState.Pacman.GetEatenGhostsCounter() * 200;
                            SoundController.PlaySound("MonsterEaten");
                            Thread.Sleep(300);
                        }
                        else
                        {
                            _gameState.Pacman.Eaten();
                            _gameState.Level.RemoveLifeDownside(_gameState.Pacman.GetLives());
                            SoundController.PlaySound("PacmanEaten");

                            if (_gameState.Pacman.GetLives() != 0)
                                _game.OneMoreTry();
                            else
                                _game.IsOver();

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
                _gameState.Game.Score += 10;
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
                _gameState.Game.Score += 50;
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
                _gameState.Game.Score += _gameState.GameController.GetFruit().GetScore();
                SoundController.PlaySound("FruitEaten");
                Thread.Sleep(51);
            }
        }
    }
}