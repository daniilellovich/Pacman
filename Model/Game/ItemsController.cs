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

            if (Keyboard.IsKeyDown(Keys.B))
                Game.State.Blinky.PathIsVisible = !Game.State.Blinky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.P))
                Game.State.Pinky.PathIsVisible = !Game.State.Pinky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.I))
                Game.State.Inky.PathIsVisible = !Game.State.Inky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.C))
                Game.State.Clyde.PathIsVisible = !Game.State.Clyde.PathIsVisible;


            if (pacmanTile is Dot)
            {
                _level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.score += 10;
                _pacman.DotsEaten++;
                SoundController.PlaySound("DotEaten");
                Thread.Sleep(17);
            }

            if (pacmanTile is Energizer)
            {
                _level.Tiles[i, j] = new Floor(new Point(i, j));
                Game.score += 50;
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

                if (ghostTile == pacmanTile)
                {
                    if (!ghost.IsEaten)

                    if (ghost.IsFrightened)
                        ghost.Eaten();
                    else
                        _pacman.Eaten();
                }
            }           

            if (_pacman.DotsEaten == 244)
            {
                if (youWon == false)
                {
                    youWon = true;
                    SoundController.StopLongSound();
                    Game.levelNum++;
                    StartForm.mf.stageL.Text = "stage " + Convert.ToString(Game.levelNum) + "/256";                   
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

            if (pacmanTile is Fruit)
            {
                _level.Tiles[i, j] = new Floor(new Point(i, j));
           //     Game.scoreCounter += Fruit.цена;
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
            if (Game.score > Convert.ToInt32(StartForm.mf.highScoreL.Text))
                StartForm.mf.highScoreL.Text = Convert.ToString(Game.score);
            Game.score = 0;
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