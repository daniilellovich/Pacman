using System.Drawing;

namespace Pacman
{
    public class Apple : Tile
    {
        public Apple(Point location) : base(location) { }

        public Apple(Point location, int time) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Apple, Rect);
        }
    }

    public class Key : Tile
    {
        public Key(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Key, Rect);
        }
    }

    public class Cherries : Tile
    {
        public Cherries(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Cherries, Rect);
        }
    }

    public class Bell : Tile
    {
        public Bell(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Bell, Rect);
        }
    }

    public class Peach : Tile
    {
        public Peach(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Peach, Rect);
        }
    }

    public class Strawberry : Tile
    {
        public Strawberry(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Strawberry, Rect);
        }
    }

    public class Grapes : Tile
    {
        public Grapes(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Grapes, Rect);
        }
    }

    public class Galaxian : Tile
    {
        public Galaxian(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Galaxian, Rect);
        }
    }
}
