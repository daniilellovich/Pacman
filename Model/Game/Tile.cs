using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman
{
    public class Tile
    {
        public static Size Size = new Size(Screen.PrimaryScreen.Bounds.Size.Height / 40,
                                           Screen.PrimaryScreen.Bounds.Size.Height / 40);
        public Point Location { get; private set; }

        public Tile(Point location) => Location = location;

        public virtual bool IsWalkable => true;

        public virtual bool IsWalkableForGhost => false;

        public Rectangle Rect
            => new Rectangle(Size.Width * Location.X,
                             Size.Height * Location.Y,
                             Size.Width,
                             Size.Height);

        public virtual void Draw(Graphics gr) { }
    }
}