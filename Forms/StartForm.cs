using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Pacman.Properties;

namespace Pacman
{
    public partial class StartForm : Form
    {
        public static Size resolution = Screen.PrimaryScreen.Bounds.Size;
        public static MainForm mf;
        public static int r;

        #region Font Loader
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public static PrivateFontCollection private_fonts = new PrivateFontCollection();

        public static void LoadFont()
        {
            Stream fontStream = new MemoryStream(Resources.atari_full);
            IntPtr data = Marshal.AllocCoTaskMem(Convert.ToInt32(fontStream.Length));
            Byte[] fontData = new Byte[fontStream.Length];
            fontStream.Read(fontData, 0, Convert.ToInt32(fontStream.Length));
            Marshal.Copy(fontData, 0, data, Convert.ToInt32(fontStream.Length));
            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cFonts);
            private_fonts.AddMemoryFont(data, Convert.ToInt32(fontStream.Length));
            fontStream.Close();
            Marshal.FreeCoTaskMem(data);

            //from http://www.swsoftware.net/using-custom-fonts-c-application/
        }
        #endregion

        public StartForm()
        {
            Game.Init();
            InitializeComponent();
            Designer();
            SoundController.PlayLongSound(Resources.pacmanFever);      
        }

        private void StartGameB_Click(object sender, EventArgs e)
        {
            Hide();
            mf = new MainForm();
            mf.Show();
            SoundController.StopLongSound();
            SoundController.PlaySound("Intro");
        }

        private void RecordsB_Click(object sender, EventArgs e)
        {

        }

        private void ExitB_Click(object sender, EventArgs e)
            => Application.Exit();

        void Designer()
        {
            r = resolution.Height / 40;
            Icon = Resources.badge;
            ClientSize = new Size(r * 28, r * 36);

            backgroundI.Image = Resources.backgroundPacman;
            backgroundI.Size = new Size(r * 28, r * 36);
            backgroundI.SizeMode = PictureBoxSizeMode.StretchImage;

            ghostI.Image = Resources.ghostGif;
            ghostI.Size = new Size(r * 18, r * 18);
            ghostI.Location = new System.Drawing.Point(Convert.ToInt32(r * -1.4), r * 19);
            ghostI.SizeMode = PictureBoxSizeMode.StretchImage;

            LoadFont();
            startGameB.Location = new System.Drawing.Point(r * 7, r * 10);
            startGameB.Size = new Size(r * 15, r * 2);
            startGameB.Font = new Font(private_fonts.Families[0], r / 3 * 2, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            recordsB.Location = new System.Drawing.Point(r * 7, r * 13);
            recordsB.Size = startGameB.Size; recordsB.Font = startGameB.Font;

            exitB.Location = new System.Drawing.Point(r * 7, r * 16);
            exitB.Size = startGameB.Size; exitB.Font = startGameB.Font;
        }
    }
}
