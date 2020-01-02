using Pacman.Properties;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Pacman
{
    public partial class PauseForm : Form
    {
        GameForm _gameForm;

        public PauseForm(GameForm gameForm)
        {
            InitializeComponent();
            ScaleGUI();
            SoundController.StopLongSound();
            _gameForm = gameForm;
            _gameForm.UpdateTimer.Stop();
            _gameForm.BehaviorControllerTimer.Stop();
        }

        public PauseForm()
        {
            InitializeComponent();
            ScaleGUI();
        }

        void ScaleGUI()
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

        private void SoundB_Click(object sender, EventArgs e)
        {
            if (SoundController._soundON)
            {
                SoundController._soundON = false;
                soundB.BackgroundImage = Resources.volumeOff;
            }
            else
            {
                SoundController._soundON = true;
                soundB.BackgroundImage = Resources.volumeOn;
            }
        }

        private void ExitB_Click(object sender, EventArgs e)
            => Application.Exit();

        private void ResumeB_Click(object sender, EventArgs e)
        {
            Close();
            Thread.Sleep(1000);
         //   _gameForm.readyLabelP.Hide();
            _gameForm.ResumeUpdateAndSound();
        }

        private void RestartB_Click(object sender, EventArgs e)
        {
            Close();
            _gameForm = new GameForm();
        }

        private void PauseForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                _gameForm.ResumeUpdateAndSound();
        }
    }
}
 
