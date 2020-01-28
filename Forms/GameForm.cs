using System;
using System.Windows.Forms;
using Pacman.Properties;
using System.Drawing;

namespace Pacman
{
    public partial class GameForm : Form
    {
        Game _game;

        public GameForm()
        {
            InitializeComponent();        
            ScaleGUI();
            SoundController.PlaySound("Intro");
            StartGame();
        }

        void StartGame()
        {
            _game = new Game();
            pnPacmanLevel.InitObjsToDraw(_game.State);
            UpdateTimer.Start();
            BehaviorControllerTimer.Start();
            SoundController.PlayLongSound("Siren");
        }

        public void PauseUpdateAndSound()
        {
            SoundController.StopLongSound();
            UpdateTimer.Stop();
            BehaviorControllerTimer.Stop();
        }

        public void ResumeUpdateAndSound()
        {
            SoundController.PlayLongSound("Siren");
            UpdateTimer.Start();
            BehaviorControllerTimer.Start();
        }

        void UpdateTimer_Tick(object sender, EventArgs e)
        {
            _game.Update();
            pnPacmanLevel.Invalidate();
            scoreL.Text = Convert.ToString(_game.score);
        }

        void BehaviorControllerTimer_Tick(object sender, EventArgs e)
            => _game.State.GameController.BehaviorEvents();

        private void pnPacmanLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) //make the same to PauseForm
            {
                PauseUpdateAndSound();
                PauseForm pf = new PauseForm();
                pf.Show();
            }

            _game.State.Pacman.GetNextDirFromKeyboard(e.KeyData);

            _game.State.GameController.SwitchPathDrawing(e.KeyCode);      
        }

        void ScaleGUI()
        {
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            int r = resolution.Height;

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

            highScoreTextL.Font = new Font(StartForm.fonts.Families[0], r / 60, FontStyle.Regular, GraphicsUnit.Point, 0);
            stageL.Font = new Font(StartForm.fonts.Families[0], r / 110, FontStyle.Regular, GraphicsUnit.Point, 0);
            scoreL.Font = highScoreTextL.Font;
            highScoreL.Font = highScoreTextL.Font;
            SetClientSizeCore(28 * Tile.Size.Width, 36 * Tile.Size.Height);
        }
    }
}