using System;
using System.Drawing;

namespace Pacman
{
    public abstract class Character
    {
        public PointF LocationF { get; protected set; }
        public Point Location => LocationF.ToPoint();
        private float _speed;
        protected Sprite _sprite;

        public Character()
            => _sprite = new Sprite();

        protected void SetSprite(Image image) 
            => _sprite.SetSprite(image);

        public void SetSpeed(float speed) 
            => _speed = speed;

        public abstract void Update();

        public abstract void Eaten();

        protected void Move(int dx, int dy)
        {
            float newX = LocationF.X + dx * _speed;
            float newY = LocationF.Y + dy * _speed;
            LocationF = new PointF(newX, newY);

            _sprite.MoveSprite(dx, dy, _speed);
        }

        public void Draw(Graphics gr)
        {
            var x = (int)(LocationF.X * Tile.Size.Width  - 6);
            var y = (int)(LocationF.Y * Tile.Size.Height - 6);
            _sprite.Draw(gr, new System.Drawing.Point(x, y));
        }
    }
}