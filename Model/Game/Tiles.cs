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

    public class l : Tile
    {
        public l(Point location) : base(location) { }

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
    }

    public class r : Tile
    {
        public r(Point location) : base(location) { }

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
    }

    public class Door : Tile
    {
        public Door(Point location) : base(location) { }

        public override bool IsWalkable => false;
        public override bool IsWalkableForGhost => true;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
    }

    public class PacmanLife : Tile
    {
        public PacmanLife(Point location) : base(location) { }

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.PacmanLife, Rect);
        }
    }

    #region Walls

    public class _a : Tile
    {
        public _a(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._a, Rect);
    }

    public class _b : Tile
    {
        public _b(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._b, Rect);
    }

    public class _c : Tile
    {
        public _c(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._c, Rect);
    }

    public class _d : Tile
    {
        public _d(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._d, Rect);
    }

    public class _1 : Tile
    {
        public _1(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._1, Rect);
    }

    public class _2 : Tile
    {
        public _2(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._2, Rect);
    }

    public class _3 : Tile
    {
        public _3(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._3, Rect);
    }

    public class _4 : Tile
    {
        public _4(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._4, Rect);
    }

    public class _5 : Tile
    {
        public _5(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._5, Rect);
    }

    public class _6 : Tile
    {
        public _6(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._6, Rect);
    }

    public class _7 : Tile
    {
        public _7(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._7, Rect);
    }

    public class _8 : Tile
    {
        public _8(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._8, Rect);
    }

    public class _9 : Tile
    {
        public _9(Point location) : base(location) { }

        public override bool IsWalkable => false;

        public override void Draw(Graphics gr)
            => gr.DrawImageUnscaledAndClipped(GameResources._9, Rect);
    }

    #endregion
}

