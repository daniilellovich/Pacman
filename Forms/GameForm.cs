using System;
using System.Windows.Forms;
using Pacman.Properties;
using System.Drawing;
using System.Timers;

namespace Pacman
{
    public partial class GameForm : Form
    {
        private Game _game;

        public GameForm()
        {
            InitializeComponent();        
            ScaleGUI();
            StartGame();
            stageL.Text = $"stage {_game.LevelNumber} / 256";
            highScoreL.Text = Convert.ToString(_game.HighScore);
            GetReady();
        }

        private void StartGame()
        {
            _game = new Game(1);
            ResumeGameLoopAndSound();
            pnPacmanLevel.InitObjsToDraw(_game.State);
        }

        public void PauseGameLoopAndSound()
        {
            SoundController.StopLongSound();
            GameLoopTimer.Stop();
            GameControllerTimer.Stop();
        }

        public void ResumeGameLoopAndSound()
        {
            SoundController.PlayLongSound("Siren");
            GameLoopTimer.Start();
            GameControllerTimer.Start();
        }

        private void GameLoopTimer_Tick(object sender, EventArgs e)
        {
            _game.Update();
            pnPacmanLevel.Invalidate();
            scoreL.Text = Convert.ToString(_game.Score);
        }

        private void GameControllerTimer_Tick(object sender, EventArgs e)
            => _game.State.GameController.BehaviorEvents();

        private void pnPacmanLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                new PauseForm(this).ShowDialog();

            _game.State.Pacman.GetNextDirFromKeyboard(e.KeyData);

            _game.State.GameController.SwitchPathDrawing(e.KeyCode);      
        }

        private void pnPacmanLevel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {//allows to control Pacman by arrows
            switch (e.KeyCode)
            {
                case Keys.Down:  e.IsInputKey = true; break;
                case Keys.Up:    e.IsInputKey = true; break;
                case Keys.Left:  e.IsInputKey = true; break;
                case Keys.Right: e.IsInputKey = true; break;
            }
        }

        public void GetReady()
        {
            PauseGameLoopAndSound();
            SoundController.PlaySound("Intro");
            readyLabelP.Show();
            SetTimerForGettingReady();
        }

        private void SetTimerForGettingReady()
        {
            var readyTimer = new System.Timers.Timer(4100);
            readyTimer.Elapsed += OnGettingReadyTimeEnded;
            readyTimer.Enabled = true;
            readyTimer.AutoReset = false;
        }

        private void OnGettingReadyTimeEnded(object source, ElapsedEventArgs e)
        {
            Invoke(new Action(() => ResumeGameLoopAndSound()));
            Invoke(new Action(() => readyLabelP.Hide()));
            Invoke(new Action(() => _game.State.Level
                .RemoveLifeDownside(_game.State.Pacman.GetLives())));
        }

        private void ScaleGUI()
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