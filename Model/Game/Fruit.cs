namespace Pacman
{
    public class Fruit : Tile
    {
        protected int _score;

        public Fruit() : base(new Point(14, 20)) { }

        public int GetScore() => _score;
    }
}