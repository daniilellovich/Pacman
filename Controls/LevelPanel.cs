using System.Windows.Forms;
using System.Drawing;

namespace Pacman
{
    public class LevelPanel : UserControl
    {
        Mediator _gameState;

        public LevelPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        public void SetObjsToDraw(Mediator gameState)
        {
            _gameState = gameState;
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_gameState == null)
                return;

            foreach (var tile in _gameState.Level.Tiles)
                tile.Draw(e.Graphics);

            _gameState.Pacman.Draw(e.Graphics);

            _gameState.Blinky.Draw(e.Graphics);
            _gameState.Pinky.Draw(e.Graphics);
            _gameState.Inky.Draw(e.Graphics);
            _gameState.Clyde.Draw(e.Graphics);

            if (_gameState.Blinky.PathIsVisible)
                _gameState.Blinky.DrawPath(e.Graphics);
            if (_gameState.Pinky.PathIsVisible)
                _gameState.Pinky.DrawPath(e.Graphics);
            if (_gameState.Inky.PathIsVisible)
                _gameState.Inky.DrawPath(e.Graphics);
            if (_gameState.Clyde.PathIsVisible)
                _gameState.Clyde.DrawPath(e.Graphics);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "PacmanLevelPanel";
            this.Size = new Size(78, 66);
            this.Load += new System.EventHandler(this.PacmanLevelPanel_Load);
            this.ResumeLayout(false);
        }

        private void PacmanLevelPanel_Load(object sender, System.EventArgs e)
        {
            
        }
    }
}