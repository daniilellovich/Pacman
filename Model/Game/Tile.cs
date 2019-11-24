using System.Drawing;

namespace Pacman
{
    public abstract class Tile
    {
        public Point _location;
        public int _size;

        public Tile(Point location)
            => _location = location;

        public Rectangle Rect
            => new Rectangle(Game.TileSize.Width * _location.X,
                             Game.TileSize.Width * _location.Y,
                             Game.TileSize.Width,
                             Game.TileSize.Height);

        public virtual bool IsWalkable => true;

        public abstract void Draw(Graphics gr);
    }
}