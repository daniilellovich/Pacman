using System.Threading;

namespace Pacman
{
    public class ItemsController
    {
        Mediator _gameState;

        public ItemsController(Mediator gameState)
            => _gameState = gameState;

        void CheckIfGhost()
        {
            foreach (var ghost in _gameState.Ghosts)
                if (_gameState.Pacman.IntersectsWith(ghost))
                    if (!ghost.IsEaten)
                    {
                        if (ghost.IsFrightened)
                            ghost.Eaten();
                        else
                            _gameState.Pacman.Eaten();
                    }
        }

        void CheckIfDotEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Dot)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                _gameState.Pacman.DotsEaten++;
                SoundController.PlaySound("DotEaten");
                Thread.Sleep(17);
            }
        }

        void CheckIfEnergizerEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Energizer)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                _gameState.Pacman.DotsEaten++;
                SoundController.PlaySound("EnergizerEaten");
                Thread.Sleep(51);

                foreach (var ghost in _gameState.Ghosts)
                    ghost.SetMode(ghost.FrightenedMode);
            }
        }

        void CheckIfFruitEaten()
        {
            if (_gameState.Pacman.GetUnderfootTile() is Fruit)
            {
                _gameState.Level.ChangeTileToFloor(_gameState.Pacman.GetLoc());
                //     Game.scoreCounter += Fruit.price;
                SoundController.PlaySound("FruitEaten");
                Thread.Sleep(17);
            }
        }

        public void Update()
        {
            CheckIfGhost();
            CheckIfDotEaten();
            CheckIfEnergizerEaten();
            CheckIfFruitEaten();
        }

        public void GameOver()
        {
            ////_gameState.Pacman.sprite = GameResources.PacmanDepth;
            //SoundController.StopLongSound();
            //StartForm.mf.gameoverLabelP.Show();
            //if (Game.score > Convert.ToInt32(StartForm.mf.highScoreL.Text))
            //    StartForm.mf.highScoreL.Text = Convert.ToString(Game.score);
            //Game.score = 0;
            //_gameState.Pacman.DotsEaten = 0;
            //Thread.Sleep(4000);

            //PauseForm pf = new PauseForm();
            //pf.resumeB.Hide();
            //pf.restartB.Text = "play again";
            //pf.ShowDialog();

            //Game.State = new Mediator();
            //Game.Init();
        }
    }
}

//if (pacmanTile is r)
//    _gameState.Pacman.Location = new Point(1, 17);
//if (pacmanTile is l)
//    _gameState.Pacman.Location = new Point(26, 17);



//            if (_gameState.Pacman.DotsEaten == 244)
//            {
//                if (youWon == false)
//                {
//                    youWon = true;
//                    SoundController.StopLongSound();
//                    Game.levelNum++;
//                    StartForm.mf.stageL.Text = "stage " + Convert.ToString(Game.levelNum) + "/256";                   
//                    _gameState.Pacman.DotsEaten = 0;
//                    Game game = new Game();
//Thread.Sleep(2000); //create victory animation
//                    SoundController.PlayLongSound(Resources.Siren);
//                }
//            }