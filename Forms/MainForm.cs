using System;
using System.Windows.Forms;
using Pacman.Properties;
using System.Drawing;

namespace Pacman
{
    public partial class MainForm : Form
    {
        static Size resolution = StartForm.resolution;
        int r = resolution.Height;
        public bool hiddenFeaturesVisible = false;
        public bool Stopped = false;

        public MainForm()     //сделать отключение звука
        {
            InitializeComponent();
            Designer();
        }

        void Designer()
        {
            StartPosition = FormStartPosition.CenterScreen;

            Icon = Resources.badge;

            highScoreTextL.Location = new System.Drawing.Point(r / 4, r / 95);
            highScoreL.Location = new System.Drawing.Point(r / 3, r / 20);

            scoreL.Location = new System.Drawing.Point(Convert.ToInt32(r * 0.57), r / 95);

            stageL.Location = new System.Drawing.Point(Convert.ToInt32(r * 0.555), r / 16);

            readyLabelP.Image = Resources.readyLabel;
            readyLabelP.Size = new Size(r / 7, r / 36);
            readyLabelP.Location = new System.Drawing.Point(Convert.ToInt32(r / 3.62), r / 2);
            readyLabelP.SizeMode = PictureBoxSizeMode.StretchImage;
            readyLabelP.Hide();

            gameoverLabelP.Image = Resources.readyLabel;
            gameoverLabelP.Size = new Size(r / 7, r / 36);
            gameoverLabelP.Location = new System.Drawing.Point(Convert.ToInt32(r / 3.62), r / 2);
            gameoverLabelP.SizeMode = PictureBoxSizeMode.StretchImage;
            gameoverLabelP.Hide();

            path.Hide();
            path.Font = new Font(StartForm.private_fonts.Families[0], r / 128, FontStyle.Regular, GraphicsUnit.Point, 0);

            highScoreTextL.Font = new Font(StartForm.private_fonts.Families[0], r / 60, FontStyle.Regular, GraphicsUnit.Point, 0);
            stageL.Font = new Font(StartForm.private_fonts.Families[0], r / 110, FontStyle.Regular, GraphicsUnit.Point, 0);
            scoreL.Font = highScoreTextL.Font;
            highScoreL.Font = highScoreTextL.Font;
            SetClientSizeCore(Game.State.Level.Width * Tile.Size.Width, Game.State.Level.Height * Tile.Size.Height);
        }

        void tmMain_Tick(object sender, EventArgs e)
        {
            Game.Update();
            pnPacmanLevel.Invalidate();

            if (hiddenFeaturesVisible)
            {
           //     Game.State.Behaviors.ShowPoints();
                path.Show(); //не открывать каждый раз, сделать флажок
                //проверить нужна ли строка ниже
       //         this.path.Font = new Font(StartForm.private_fonts.Families[0], resolution.Height / 128, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
           //     path.Text = Game.State.Behaviors.Output();
            }
            else
                path.Hide();

            scoreL.Text = Convert.ToString(Game.scoreCounter);                        
        }

        public int timer = 0;
        private void MainTimer_Tick(object sender, EventArgs e)
        {
           // readyLabelP.Show();

            if (timer == 0)
            {
                tmMain.Start();
         //       readyLabelP.Hide();
                BehaviorControllerTimer.Start();
                SoundController.PlayLongSound(Resources.Siren);
        //        Game.State.Level.Tiles[4, 34] = new Floor(new Point(4, 34));
                MainTimer.Stop();
            }   
            
            timer++;
        }

        int sec=0;
        private void BehaviorControllerTimer_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Keys.B))
                Game.State.Blinky.PathIsVisible = !Game.State.Blinky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.P))
                Game.State.Pinky.PathIsVisible = !Game.State.Pinky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.I))
                Game.State.Inky.PathIsVisible = !Game.State.Inky.PathIsVisible;
            if (Keyboard.IsKeyDown(Keys.C))
                Game.State.Clyde.PathIsVisible = !Game.State.Clyde.PathIsVisible;

            Game.State.BehaviorChanger.BehaviorEvents(sec++);
        }

        private void scoreL_Click(object sender, EventArgs e) //убрать клик, сделать по клавиатуре
        {
            if (hiddenFeaturesVisible == false)
                hiddenFeaturesVisible = true;
            else
                hiddenFeaturesVisible = false;
        }

        #region elements

        #endregion
    }
}