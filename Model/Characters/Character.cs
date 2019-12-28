namespace Pacman
{
    public abstract class Character
    {
        protected Mediator _gameState;

        protected Sprite _sprite = new Sprite();
        protected PointF _locationF, _home;
        private float _speed;

        public Character(Mediator gameState)
            => _gameState = gameState;

        public PointF GetLocF() 
            => _locationF;

        public Point GetLoc()
            => _locationF.ToPoint();

        public void SetSpeed(float speed) 
            => _speed = speed;

        protected void SetSprite(System.Drawing.Image image) 
            => _sprite.ChangeImage(image);

        public abstract void Update();

        public abstract void Eaten();

        protected void Move(int dx, int dy)
        {
            float newX = _locationF.X + dx * _speed;
            float newY = _locationF.Y + dy * _speed;
            _locationF = new PointF(newX, newY);

            _sprite.MoveSprite(dx, dy, _speed);
        }

        public void Draw(System.Drawing.Graphics gr)
        {
            var x = (int)(_locationF.X * Tile.Size.Width  - 6);
            var y = (int)(_locationF.Y * Tile.Size.Height - 6);
            _sprite.Draw(gr, new System.Drawing.Point(x, y));
        }
    }
}