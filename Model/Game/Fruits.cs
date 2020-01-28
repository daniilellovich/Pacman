using System.Drawing;

namespace Pacman
{
    public class Apple : Fruit
    {
        public Apple(Point location) : base(location) 
            => _score = 700; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Apple, Rect);
        }
    }

    public class Key : Fruit
    {
        public Key(Point location) : base(location) 
            => _score = 5000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Key, Rect);
        }
    }

    public class Cherries : Fruit
    {
        public Cherries(Point location) : base(location) 
            => _score = 100; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Cherries, Rect);
        }
    }

    public class Bell : Fruit
    {
        public Bell(Point location) : base(location)
             => _score = 3000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Bell, Rect);
        }
    }

    public class Peach : Fruit
    {
        public Peach(Point location) : base(location)
             => _score = 500; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Peach, Rect);
        }
    }

    public class Strawberry : Fruit
    {
        public Strawberry(Point location) : base(location)
             => _score = 300; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Strawberry, Rect);
        }
    }

    public class Grapes : Fruit
    {
        public Grapes(Point location) : base(location)
             => _score = 1000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Grapes, Rect);
        }
    }

    public class Galaxian : Fruit
    {
        public Galaxian(Point location) : base(location) 
            => _score = 2000; 

        public override void Draw(Graphics gr)
        {
            gr.DrawImageUnscaledAndClipped(GameResources.Floor, Rect);
            gr.DrawImageUnscaledAndClipped(GameResources.Galaxian, Rect);
        }
    }
}
