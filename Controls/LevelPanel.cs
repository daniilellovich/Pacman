using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace Pacman
{
    public class LevelPanel : UserControl
    {
        Mediator _gameState;

        public LevelPanel()
            => SetStyle(ControlStyles.AllPaintingInWmPaint | 
                        ControlStyles.OptimizedDoubleBuffer | 
                        ControlStyles.UserPaint, true);

        public void InitObjsToDraw(Mediator gameState)
            => _gameState = gameState;

        protected override void OnPaintBackground(PaintEventArgs e) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_gameState == null)
                return;

            foreach (var tile in _gameState.Level.GetTiles())
                tile.Draw(e.Graphics);

            foreach (var character in _gameState.Characters)
                character.Draw(e.Graphics);

            foreach (var ghost in _gameState.Ghosts)
                if (ghost.PathIsVisible)
                    ghost.DrawPath(e.Graphics);
        }
    }
}