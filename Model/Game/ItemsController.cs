using Pacman.Properties;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Pacman
{
    public class ItemsController
    {
        Mediator _gameState;

        public ItemsController(Mediator gameState)
        {
            _gameState = gameState;
        }

        bool youWon;

        public void Update()
        {
            int i = (int)(_gameState.Pacman.GetLocF().X + 0.5f);
            int j = (int)(_gameState.Pacman.GetLocF().Y + 0.5f);
            Tile pacmanTile = _gameState.Level.Tiles[i, j];

            if ((Keyboard.IsKeyDown(Keys.Escape)))
            {
                PauseForm pf = new PauseForm();
                pf.Show();
            }

            if (Keyboard.IsKeyDown(Keys.B))
                _gameState.Blinky.PathIsVisible = !_gameState.Blinky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.P))
                _gameState.Pinky.PathIsVisible = !_gameState.Pinky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.I))
                _gameState.Inky.PathIsVisible = !_gameState.Inky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.C))
                _gameState.Clyde.PathIsVisible = !_gameState.Clyde.PathIsVisible;


            if (pacmanTile is Dot)
            {
                _gameState.Level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.score += 10;
                _gameState.Pacman.DotsEaten++;
                SoundController.PlaySound("DotEaten");
                Thread.Sleep(17);
            }

            if (pacmanTile is Energizer)
            {
                _gameState.Level.Tiles[i,j] = new Floor(new Point(i, j));
                Game.score += 50;
                _gameState.Pacman.DotsEaten++;
                SoundController.PlaySound("EnergizerEaten");
                Thread.Sleep(51);

                foreach (var ghost in _gameState.Ghosts)
                    ghost.SetMode(ghost.FrightenedMode);
            }


            foreach (var ghost in _gameState.Ghosts)
            {
                int m = (int)(ghost.GetLocF().X + 0.5f);
                int n = (int)(ghost.GetLocF().Y + 0.5f);
                Tile ghostTile = _gameState.Level.Tiles[m, n];

                if (ghostTile == pacmanTile)
                {
                    if (!ghost.IsEaten)

                    if (ghost.IsFrightened)
                        ghost.Eaten();
                    else
                        _gameState.Pacman.Eaten();
                }
            }           

            if (_gameState.Pacman.DotsEaten == 244)
            {
                if (youWon == false)
                {
                    youWon = true;
                    SoundController.StopLongSound();
                    Game.levelNum++;
                    StartForm.mf.stageL.Text = "stage " + Convert.ToString(Game.levelNum) + "/256";                   
                    _gameState.Pacman.DotsEaten = 0;
                    Game game = new Game();
                    game.Init();
                    Thread.Sleep(2000); //сделать анимацию победы
                    SoundController.PlayLongSound(Resources.Siren);
                }
            }

            if (_gameState.Pacman.DotsEaten == 70)
                _gameState.Level.Tiles[14, 20] = new Cherries(new Point(14, 20));
            if (_gameState.Pacman.DotsEaten == 170)
                _gameState.Level.Tiles[14, 20] = new Cherries(new Point(14, 20));

            if (pacmanTile is Fruit)
            {
                _gameState.Level.Tiles[i, j] = new Floor(new Point(i, j));
           //     Game.scoreCounter += Fruit.цена;
                SoundController.PlaySound("FruitEaten");
                Thread.Sleep(17);
            }

            #region tunnel checker
            //if (pacmanTile is r)
            //    _gameState.Pacman.Location = new Point(1, 17);
            //if (pacmanTile is l)
            //    _gameState.Pacman.Location = new Point(26, 17);

            //if (blinkyTile is r)
            //    Game.State.Blinky.Location = new Point(1, 17);
            //if (blinkyTile is l)
            //    Game.State.Blinky.Location = new Point(26, 17);

            //if (blinkyTile is r)
            //    Game.State.Pinky.Location = new Point(1, 17);
            //if (blinkyTile is l)
            //    Game.State.Pinky.Location = new Point(26, 17);

            //if (blinkyTile is r)
            //    Game.State.Inky.Location = new Point(1, 17);
            //if (blinkyTile is l)
            //    Game.State.Inky.Location = new Point(26, 17);

            //if (blinkyTile is r)
            //    Game.State.Clyde.Location = new Point(1, 17);
            //if (blinkyTile is l)
            //    Game.State.Clyde.Location = new Point(26, 17);
            #endregion
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