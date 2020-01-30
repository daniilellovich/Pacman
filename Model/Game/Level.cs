namespace Pacman
{
    public class Level
    {
        public Tile[,] Tiles { get; private set; }
        private int _width;
        private int _height;

        public Level()
            => ReadLevelFromFile();

        void ReadLevelFromFile()
        {
            string[] _lines = Properties.Resources.PacmanMap.Split('\n');

            _width = _lines[0].Length - 1;
            _height = _lines.Length - 1;
            Tiles = new Tile[_width, _height];

            for (int j = 0; j < _height; j++)
                for (int i = 0; i < _width; i++)
                {
                    Point point = new Point(i, j);
                    Tiles[i, j] = (_lines[j][i]) switch
                    {
                        '.' => new Dot(point),
                        '@' => new Energizer(point),
                        '*' => new PacmanLife(point),
                        ' ' => new Floor(point),
                        'p' => new Door(point),
                        '=' => new Wall(point, GameResources.Floor),
                        '1' => new Wall(point, GameResources._1),
                        '2' => new Wall(point, GameResources._2),
                        '3' => new Wall(point, GameResources._3),
                        '4' => new Wall(point, GameResources._4),
                        '5' => new Wall(point, GameResources._5),
                        '6' => new Wall(point, GameResources._6),
                        '7' => new Wall(point, GameResources._7),
                        '8' => new Wall(point, GameResources._8),
                        '9' => new Wall(point, GameResources._9),
                        'a' => new Wall(point, GameResources._a),
                        'b' => new Wall(point, GameResources._b),
                        'c' => new Wall(point, GameResources._c),
                        'd' => new Wall(point, GameResources._d),
                        'l' => new LeftAisle(point),
                        'r' => new RightAisle(point),
                        _ => throw new System.Exception($"Unknown tile! ({i};{j})"),
                    };
                }
        }

        public Tile GetTile(Point point) 
            => Tiles[point.X, point.Y];

        private bool IsValidPoint(Point point)
            => !((point.X < 0 || point.X >= _width) ||
               (point.Y < 0 || point.Y >= _height));

        public bool IsWalkableForGhost(Point point)
            => IsValidPoint(point) && (Tiles[point.X, point.Y].IsWalkableForGhost);

        public bool IsWalkableForPacman(Point point)
            => IsValidPoint(point) && (Tiles[point.X, point.Y].IsWalkableForPacman);

        public void ChangeTileToFloor(Point point)
            => Tiles[point.X, point.Y] = new Floor(point);

        public void PutNewFruit(Fruit fruit)
            => Tiles[14, 20] = fruit;

        public void PutPacmanLivesDownside(int lives)
        {
            if (lives-- == 3)
                  Tiles[4, 34] = new PacmanLife(new Point(4, 34));
            if (lives-- == 2)
                 Tiles[2, 34] = new PacmanLife(new Point(2, 34));
            if (lives-- == 1)
                Tiles[0, 34] = new PacmanLife(new Point(0, 34));
        }

        public void RemoveOnePacmanLifeDownside(int livesLeft)
        {
            switch (livesLeft)
            {
                case 2: Tiles[4, 34] = new Wall(new Point(4, 34), GameResources.Floor); break;
                case 1: Tiles[2, 34] = new Wall(new Point(2, 34), GameResources.Floor); break;
                case 0: Tiles[0, 34] = new Wall(new Point(0, 34), GameResources.Floor);
                        Game.GameOver(); break;
            }
        }
    }
}