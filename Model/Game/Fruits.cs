using System.Drawing;

namespace Pacman
{
    public class Apple : Fruit
    {
        public Apple() :base() => _score = 700; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Apple, Rect);
        }
    }

    public class Key : Fruit
    {
        public Key() : base() => _score = 5000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Key, Rect);
        }
    }

    public class Cherries : Fruit
    {
        public Cherries() : base() => _score = 100; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Cherries, Rect);
        }
    }

    public class Bell : Fruit
    {
        public Bell() : base() => _score = 3000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Bell, Rect);
        }
    }

    public class Peach : Fruit
    {
        public Peach() : base() => _score = 500; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Peach, Rect);
        }
    }

    public class Strawberry : Fruit
    {
        public Strawberry() : base() => _score = 300; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Strawberry, Rect);
        }
    }

    public class Grapes : Fruit
    {
        public Grapes() : base() => _score = 1000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Grapes, Rect);
        }
    }

    public class Galaxian : Fruit
    {
        public Galaxian() : base() => _score = 2000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Galaxian, Rect);
        }
    }
}
