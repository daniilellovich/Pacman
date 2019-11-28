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

        public Tile(int x, int y)
            => Location = new Point(x, y);

        public Rectangle Rect
            => new Rectangle(Size.Width * Location.X,
                             Size.Height * Location.Y,
                             Size.Width,
                             Size.Height);

        public virtual bool IsWalkable => true;
            //(Location.X < Game.State.Level.Width &&
            // Location.X > 0 && Location.Y > 0 &&
            // Location.Y < Game.State.Level.Height);

   //     public virtual bool UnwalkableForPacman =>
   //         !IsWalkable && Location != Door;

        public virtual void Draw(Graphics gr) { }
    }
}