using System.Drawing;

namespace Pacman
{
    public class Floor : Tile
    {
        public Floor(Point location) : base(location) { }

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
    }

    public class Dot : Tile
    {
        public Dot(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Dot, Rect);
        }
    }

    public class Energizer : Tile
    {
        public Energizer(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Energizer, Rect);
        }
    }

    public class LeftAisle : Tile
    {
        public LeftAisle(Point location) : base(location) { }

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
    }

    public class RightAisle : Tile
    {
        public RightAisle(Point location) : base(location) { }

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
    }

    public class Door : Tile
    {
        public Door(Point location) : base(location) { }

        public override bool IsWalkableForPacman => false;

        public override bool IsWalkableForGhost => true;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
    }

    public class PacmanLife : Tile
    {
        public PacmanLife(Point location) : base(location) { }

        public override bool IsWalkableForPacman => false;

        public override bool IsWalkableForGhost => false;

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.PacmanLife, Rect);
        }
    }

    public class Wall : Tile
    {
        public Wall(Point location, Bitmap image) : base(location) 
            => _image = image;

        public override bool IsWalkableForPacman => false;

        public override bool IsWalkableForGhost => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(_image, Rect);
    }
}