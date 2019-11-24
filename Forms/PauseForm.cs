using Pacman.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public partial class PauseForm : Form
    {
        #region stuff
        public PauseForm()
        {
            InitializeComponent();

            SoundController.StopLongSound();
            StartForm.mf.tmMain.Stop();
            StartForm.mf.BehaviorControllerTimer.Stop();
            StartForm.mf.MainTimer.Stop();
            #region Designer
            Size resolution = StartForm.resolution;
            Icon = Resources.badge;
            int r = resolution.Height / 40;
            this.ClientSize = new Size(r * 20, r * 16);

            resumeB.Size = new Size(r * 16, r * 2);
            resumeB.Font = new Font(StartForm.private_fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            resumeB.Location = new System.Drawing.Point(r * 2, r * 2);

            resumeB.Size = new Size(r * 15, r * 2);
            resumeB.Font = new Font(StartForm.private_fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            restartB.Location = new System.Drawing.Point(r * 2, r * 5);
            restartB.Size = new Size(r * 15, r * 2);
            restartB.Font = new Font(StartForm.private_fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            exitB.Location = new System.Drawing.Point(r * 2, r * 8);
            exitB.Size = new Size(r * 15, r * 2);
            exitB.Font = new Font(StartForm.private_fonts.Families[0], r / 2, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            soundB.Location = new System.Drawing.Point(r * 16, r * 12);
            soundB.BackgroundImage = Resources.volumeOn;
            soundB.Size = new Size(r * 2, r * 2);
            soundB.BackgroundImageLayout = ImageLayout.Stretch;
            soundB.BackgroundImageLayout = ImageLayout.Stretch;
            #endregion
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
        {
            Application.Exit();
        }

        #endregion

        private void ResumeB_Click(object sender, EventArgs e)
        {
            this.Close();
            StartForm.mf.readyLabelP.Show();
            Thread.Sleep(1000);
            StartForm.mf.readyLabelP.Hide();
            if (SoundController._soundON)
                SoundController.PlayLongSound(Resources.Siren);
            StartForm.mf.tmMain.Start();
            StartForm.mf.BehaviorControllerTimer.Start();
            StartForm.mf.MainTimer.Start();
        }

        private void RestartB_Click(object sender, EventArgs e)
        {
            this.Close();
           // LevelNum
            StartForm.mf.readyLabelP.Show();
            StartForm.mf.timer = 0;
            StartForm.mf.MainTimer.Start();
        }

        private void PauseForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                this.Close();
                StartForm.mf.readyLabelP.Show();
                Thread.Sleep(1000);
                StartForm.mf.readyLabelP.Hide();
                if (SoundController._soundON)
                    SoundController.PlayLongSound(Resources.Siren);
                StartForm.mf.tmMain.Start();
                StartForm.mf.BehaviorControllerTimer.Start();
                StartForm.mf.MainTimer.Start();
            }
        } //почему то нужно держать
    }
    
}
 
