using System.Collections.Generic;
using System.Drawing;

namespace Pacman
{
    public class Tile
    {
        public static Size Size { get; set; }

        public Point Location { get; private set; }

        public Tile(Point location)
            => Location = location;

        public virtual bool IsWalkable => true;

        public Rectangle Rect
            => new Rectangle(Size.Width * Location.X,
                             Size.Height * Location.Y,
                             Size.Width,
                             Size.Height);

        public virtual void Draw(Graphics gr) { }
    }
}