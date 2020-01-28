namespace Pacman
{
    public class Fruit : Tile
    {
        protected int _score;

        public int GetScore() => _score;

        public Fruit(Point location) : base(location) { }
    }
}