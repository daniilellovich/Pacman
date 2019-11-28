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
        public bool youWon = false;
        //  int lives = 3;
        List<Ghost> _ghosts;

        public void GameOver()
        {
            //Game.State.Pacman.sprite = GameResources.PacmanDepth;
            SoundController.StopLongSound();
            StartForm.mf.gameoverLabelP.Show();
            if (Game.scoreCounter > Convert.ToInt32(StartForm.mf.highScoreL.Text))
                StartForm.mf.highScoreL.Text = Convert.ToString(Game.scoreCounter);
            Game.scoreCounter = 0;
            Game.State.Pacman.DotsEaten = 0;
            Thread.Sleep(4000);

            PauseForm pf = new PauseForm();
            pf.resumeB.Hide();
            pf.restartB.Text = "play again";
            pf.ShowDialog();

            Game.State = new GameState();
            Game.Init();
        }

        public void Update()
        {
            var i = (int)(Game.State.Pacman.LocationF.X + 0.5f);
            var j = (int)(Game.State.Pacman.LocationF.Y + 0.5f);
            Tile pacmanTile = Game.State.Level.Tiles[i, j];

            if ((Keyboard.IsKeyDown(Keys.Escape)))
            {
                PauseForm pf = new PauseForm();
                pf.Show();
            }

            //if ((Keyboard.IsKeyToggled(Keys.C))) //добавить вниз жизни
            //{
            //    if (StartForm.mf.hiddenFeaturesVisible == false)
            //        StartForm.mf.hiddenFeaturesVisible = true;
            //    else
            //        StartForm.mf.hiddenFeaturesVisible = false;
            //}

            if (pacmanTile is Dot)
            {
                Game.State.Level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.scoreCounter += 10;
                Game.State.Pacman.DotsEaten++;
                SoundController.PlaySound("DotEaten");
                Thread.Sleep(17);
            }

            if (pacmanTile is Energizer)
            {
                Game.State.Level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.scoreCounter += 50;
                Game.State.Pacman.DotsEaten++;
                SoundController.PlaySound("EnergizerEaten");
                Thread.Sleep(51);
        //        Game.State.BehaviorChanger.GhostAreFrightened();
            }

            //if (Game.State.BehaviorChanger._ghostsAreFrightened)
            //{
            //    foreach(var ghost in _ghosts)
            //    {
            //        var p = (int)(ghost.LocationF.X + 0.5f);
            //        var q = (int)(ghost.LocationF.Y + 0.5f);
            //        Tile ghostTile = Game.State.Level.Tiles[p, q];

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
            //    //        Game.State.Level.Tiles[0, 34] = new Floor(new Point(4, 34));
            //    //        break;
            //    //    case 2:
            //    //        Game.State.Level.Tiles[2, 34] = new Floor(new Point(4, 34));
            //    //        break;
            //    //}

            //    //               if (Game.State.Pacman.dotsCounter == 70)

            //    //       if (Game.State.Pacman.dotsCounter == 170)

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

            if (Game.State.Pacman.DotsEaten == 244)
            {
                if (youWon == false)
                {
                    youWon = true;
                    //    Game.State.Pacman.sprite = GameResources.PacmanWon;
                    SoundController.StopLongSound();
                    Game.LevelNum++;
                    StartForm.mf.stageL.Text = "stage " + Convert.ToString(Game.LevelNum) + "/256";                   
                    Game.State.Pacman.DotsEaten = 0;
                    Game.State = new GameState();
                    Game.Init();
                    Thread.Sleep(2000); //сделать анимацию победы
                    SoundController.PlayLongSound(Resources.Siren);
                }
            }

            //if (Game.State.Blinky._isEaten)
            //    Game.State.Behaviors.CheckIfBlinkyAtHome();
            //if (Game.State.Pinky._isEaten)
            //    Game.State.Behaviors.CheckIfPinkyAtHome();
            //if (Game.State.Inky._isEaten)
            //    Game.State.Behaviors.CheckIfInkyAtHome();
            //if (Game.State.Clyde._isEaten)
            //    Game.State.Behaviors.CheckIfClydeAtHome();

            if (Game.State.Pacman.DotsEaten == 70)
                Game.State.Level.Tiles[14, 20] = new Cherries(new Point(14, 20));
            if (Game.State.Pacman.DotsEaten == 170)
                Game.State.Level.Tiles[14, 20] = new Cherries(new Point(14, 20));

            if (pacmanTile is Cherries)
            {
                Game.State.Level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.scoreCounter += 200;
                SoundController.PlaySound("FruitEaten");
                Thread.Sleep(17);
            }

            #region tunnel checker
            //if (pacmanTile is r)
            //    Game.State.Pacman.Location = new Point(1, 17);
            //if (pacmanTile is l)
            //    Game.State.Pacman.Location = new Point(26, 17);

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
    }
}