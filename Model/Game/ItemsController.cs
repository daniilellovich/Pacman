using Pacman.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Pacman
{
    public class ItemsController
    {
        List<Ghost> _ghosts = Game.State.Ghosts;
        Pacman _pacman=Game.State.Pacman;
        Level _level = Game.State.Level;

        bool youWon;

        public ItemsController()
        {
        }

        public void Update()
        {
            int i = (int)(_pacman.LocationF.X + 0.5f);
            int j = (int)(_pacman.LocationF.Y + 0.5f);
            Tile pacmanTile = _level.Tiles[i, j];

            if ((Keyboard.IsKeyDown(Keys.Escape)))
            {
                PauseForm pf = new PauseForm();
                pf.Show();
            }

            if (pacmanTile is Dot)
            {
                _level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.scoreCounter += 10;
                _pacman.DotsEaten++;
                SoundController.PlaySound("DotEaten");
                Thread.Sleep(17);
            }

            if (pacmanTile is Energizer)
            {
                _level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.scoreCounter += 50;
                _pacman.DotsEaten++;
                SoundController.PlaySound("EnergizerEaten");
                Thread.Sleep(51);

                foreach (var ghost in _ghosts)
                    ghost.ChangeMode(ghost.FrightenedMode);
            }

            foreach (var ghost in _ghosts)
            {
                int m = (int)(ghost.LocationF.X + 0.5f);
                int n = (int)(ghost.LocationF.Y + 0.5f);
                Tile ghostTile = _level.Tiles[m, n];

                if (ghostTile == pacmanTile && ghost.IsFrightened)
                    ghost.ChangeMode(ghost.ReturningHome);
            }           

            //if (Game.State.BehaviorChanger._ghostsAreFrightened)
            //{
            //    foreach(var ghost in _ghosts)
            //    {
            //        var p = (int)(ghost.LocationF.X + 0.5f);
            //        var q = (int)(ghost.LocationF.Y + 0.5f);
            //        Tile ghostTile = _level.Tiles[p, q];

            //        if (pacmanTile == ghostTile)
            //        {
            //            ghost.SetSpeed(0.14f);
            //            //  ghost.IsEaten();
            //            ghost.ChangeMode(ghost.ReturningHome);
            //        }
            //    }

            //    SoundController.PlaySound("MonsterEaten");
            //}

            //if (((pacmanTile == blinkyTile) || (pacmanTile == pinkyTile) ||
            //    (pacmanTile == inkyTile) || (pacmanTile == clydeTile))
            //    && (Game.State.BehaviorChanger._isFrightened == false))
            //{
            //    SoundController.StopLongSound();
            //    SoundController.PlaySound("PacmanEaten");
            ////    lives--;
            ////    switch (lives)
            //    //{
            //    //    case 0:
            //    //        GameOver();
            //    //        break;
            //    //    case 1:
            //    //        _level.Tiles[0, 34] = new Floor(new Point(4, 34));
            //    //        break;
            //    //    case 2:
            //    //        _level.Tiles[2, 34] = new Floor(new Point(4, 34));
            //    //        break;
            //    //}

            //    //               if (_pacman.dotsCounter == 70)

            //    //       if (_pacman.dotsCounter == 170)

            //    Thread.Sleep(1500);
            ////    Game.InitCharacters();
            ////    Game.InitCharactersControllers();
            //    SoundController.PlayLongSound(Resources.Siren);
            //    Game.State.BehaviorChanger._currentTime = 0;
            //    Game.State.BehaviorChanger._frightenedTime = 0;
            //    //  StartForm.mf.readyLabelP.Show();
            //    //  Thread.Sleep(1000);
            //    //  StartForm.mf.readyLabelP.Hide();
            //}


            //if (Game.State.Blinky._isEaten)
            //    Game.State.Behaviors.CheckIfBlinkyAtHome();
            //if (Game.State.Pinky._isEaten)
            //    Game.State.Behaviors.CheckIfPinkyAtHome();
            //if (Game.State.Inky._isEaten)
            //    Game.State.Behaviors.CheckIfInkyAtHome();
            //if (Game.State.Clyde._isEaten)
            //    Game.State.Behaviors.CheckIfClydeAtHome();

            if (_pacman.DotsEaten == 244)
            {
                if (youWon == false)
                {
                    youWon = true;
                    //    _pacman.sprite = GameResources.PacmanWon;
                    SoundController.StopLongSound();
                    Game.LevelNum++;
                    StartForm.mf.stageL.Text = "stage " + Convert.ToString(Game.LevelNum) + "/256";                   
                    _pacman.DotsEaten = 0;
                    Game.State = new GameState();
                    Game.Init();
                    Thread.Sleep(2000); //сделать анимацию победы
                    SoundController.PlayLongSound(Resources.Siren);
                }
            }

            if (_pacman.DotsEaten == 70)
                _level.Tiles[14, 20] = new Cherries(new Point(14, 20));
            if (_pacman.DotsEaten == 170)
                _level.Tiles[14, 20] = new Cherries(new Point(14, 20));

            if (pacmanTile is Cherries)
            {
                _level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.scoreCounter += 200;
                SoundController.PlaySound("FruitEaten");
                Thread.Sleep(17);
            }

            #region tunnel checker
            //if (pacmanTile is r)
            //    _pacman.Location = new Point(1, 17);
            //if (pacmanTile is l)
            //    _pacman.Location = new Point(26, 17);

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
            //_pacman.sprite = GameResources.PacmanDepth;
            SoundController.StopLongSound();
            StartForm.mf.gameoverLabelP.Show();
            if (Game.scoreCounter > Convert.ToInt32(StartForm.mf.highScoreL.Text))
                StartForm.mf.highScoreL.Text = Convert.ToString(Game.scoreCounter);
            Game.scoreCounter = 0;
            _pacman.DotsEaten = 0;
            Thread.Sleep(4000);

            PauseForm pf = new PauseForm();
            pf.resumeB.Hide();
            pf.restartB.Text = "play again";
            pf.ShowDialog();

            Game.State = new GameState();
            Game.Init();
        }
    }
}