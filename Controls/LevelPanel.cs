using System.Windows.Forms;

namespace Pacman.Controls
{
    public class LevelPanel : UserControl
    {
        public LevelPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Game.State == null)
                return;

            foreach (var tile in Game.State.Level.Tiles)
                tile.Draw(e.Graphics);

            Game.State.Pacman.Draw(e.Graphics);
            Game.State.Blinky.Draw(e.Graphics);
            Game.State.Pinky.Draw(e.Graphics);
            Game.State.Inky.Draw(e.Graphics);   
            Game.State.Clyde.Draw(e.Graphics);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "PacmanLevelPanel";
            this.Size = new System.Drawing.Size(78, 66);
            this.Load += new System.EventHandler(this.PacmanLevelPanel_Load);
            this.ResumeLayout(false);
        }

        private void PacmanLevelPanel_Load(object sender, System.EventArgs e)
        {
        }
    }
}