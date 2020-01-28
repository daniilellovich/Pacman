namespace Pacman
{
    public abstract class Character
    {
        #region vars
        protected Mediator _gameState;
        protected Sprite _sprite = new Sprite();
        protected PointF _locationF, _home;
        protected float _speed;

        public PointF GetLocF()
            => _locationF;

        public Point GetLoc()
            => _locationF.ToPoint();

        public void SetSpeed(float speed)
            => _speed = speed;

        protected void SetSpriteImage(System.Drawing.Image image)
            => _sprite.ChangeImage(image);
        #endregion

        public Character(Mediator gameState)
            => _gameState = gameState;

        public abstract void Update();

        public abstract void Eaten();

        protected void MoveTo(Point destination)
        {
            (int dx, int dy) = CalcOffset(destination);
            _sprite.ChoosePicByDir(dx, dy, _speed);

            float newX = _locationF.X + dx * _speed;
            float newY = _locationF.Y + dy * _speed;
            _locationF = new PointF(newX, newY);
        }

        protected (int, int) CalcOffset(Point destination)
        {
            int dx = !_locationF.IsOnX(destination, 0.06f) ?
                ((_locationF.X < destination.X) ? 1 : -1) : 0;
            int dy = !_locationF.IsOnY(destination, 0.06f) ?
                ((_locationF.Y < destination.Y) ? 1 : -1) : 0;
            return (dx, dy);
        }

        public bool IntersectsWith(Character character)
            => (GetUnderfootTile() == character.GetUnderfootTile());

        public Tile GetUnderfootTile()
        {
            int i = (int)(_locationF.X + 0.5f);
            int j = (int)(_locationF.Y + 0.5f);
            return _gameState.Level.GetTile(new Point(i, j));
        }

        protected bool CameTo(Point destination)
            => (GetLocF().IsOnXandY(destination, 0.06f));

        public void Draw(System.Drawing.Graphics gr)
        {
            int x = (int)(_locationF.X * Tile.Size.Width  - 6);
            int y = (int)(_locationF.Y * Tile.Size.Height - 6);
            _sprite.Draw(gr, new System.Drawing.Point(x, y));
        }
    }
}