using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public class Tile
    {
        public Tile(Point location)
            => _location = location;

        public virtual bool IsWalkableForPacman => true;

        public virtual bool IsWalkableForGhost => true;

        public static Size Size = new Size(Screen.PrimaryScreen.Bounds.Size.Height / 40,
                                           Screen.PrimaryScreen.Bounds.Size.Height / 40);
        protected Point _location;

        protected Bitmap _image;

        protected Rectangle Rect
            => new Rectangle(Size.Width * _location.X, Size.Height * _location.Y, 
                Size.Width, Size.Height);

        public virtual void Draw(Graphics gr) { }
    }
}