using Pacman.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public partial class PauseForm : Form
    {
        private GameForm _gameForm;

        public PauseForm(GameForm gameForm)
        {
            _gameForm = gameForm;
            InitializeComponent();
            ScaleGUI();
            _gameForm.PauseGameLoopAndSound();
        }

        private void ResumeB_Click(object sender, EventArgs e)
        {
            _gameForm.Invoke(new Action(() => _gameForm.ResumeGameLoopAndSound()));
            Close();
        }

        private void RestartB_Click(object sender, EventArgs e)
        {
            _gameForm.Close();
            new GameForm().Show();
            Close();
        }

        private void ExitB_Click(object sender, EventArgs e)
            => Application.Exit();

        private void SoundB_Click(object sender, EventArgs e)
        {
            SoundController._soundON = !SoundController._soundON;
            soundB.BackgroundImage = SoundController._soundON ?
                Resources.volumeOn : Resources.volumeOff;
        }

        private void PauseForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _gameForm.Invoke(new Action(() => _gameForm.ResumeGameLoopAndSound()));
                Close();
            }
        }

        private void ScaleGUI()
        {
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            int r = resolution.Height / 40;
            ClientSize = new Size(r * 20, r * 16);
            Icon = Resources.badge;

            resumeB.Size = new Size(r * 16, r * 2);
            resumeB.Font = new Font(StartForm.fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, 0);
            resumeB.Location = new System.Drawing.Point(r * 2, r * 2);

            resumeB.Size = new Size(r * 15, r * 2);
            resumeB.Font = new Font(StartForm.fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, 0);

            restartB.Location = new System.Drawing.Point(r * 2, r * 5);
            restartB.Size = new Size(r * 15, r * 2);
            restartB.Font = new Font(StartForm.fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, 0);

            exitB.Location = new System.Drawing.Point(r * 2, r * 8);
            exitB.Size = new Size(r * 15, r * 2);
            exitB.Font = new Font(StartForm.fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, 0);

            soundB.Location = new System.Drawing.Point(r * 16, r * 12);
            soundB.BackgroundImage = Resources.volumeOn;
            soundB.Size = new Size(r * 2, r * 2);
            soundB.BackgroundImageLayout = ImageLayout.Stretch;
            soundB.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}