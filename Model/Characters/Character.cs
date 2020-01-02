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

        protected (int, int) CalcOffset(Point destination)
        {
            int dx = !_locationF.IsOnX(destination, 0.06f) ? 
                ((_locationF.X < destination.X) ? 1 : -1) : 0;
            int dy = !_locationF.IsOnY(destination, 0.06f) ?
                ((_locationF.Y < destination.Y) ? 1 : -1) : 0;
            return (dx, dy);
        }

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
       
        public Tile GetUnderfootTile()
        {
            int i = (int)(_locationF.X + 0.5f);
            int j = (int)(_locationF.Y + 0.5f);
            return _gameState.Level.Tiles[i, j];
        }

        public bool IntersectsWith(Character tile)
            => (GetUnderfootTile() == tile.GetUnderfootTile());
    }
}