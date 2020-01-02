namespace Pacman
{
    public class Level
    {
        public Tile[,] Tiles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Level() => ReadLevelFromFile();

        Level ReadLevelFromFile()
        {
            string[] _lines = Properties.Resources.PacmanMap.Split('\n');

            Width = _lines[0].Length - 1;
            Height = _lines.Length - 1;
            Tiles = new Tile[Width, Height];

            for (int j = 0; j < Height; j++)
                for (int i = 0; i < Width; i++)
                {
                    Point point = new Point(i, j);
                    switch (_lines[j][i])
                    {
                        case '.': Tiles[i, j] = new Dot(point); break;
                        case '@': Tiles[i, j] = new Energizer(point); break;
                        case '*': Tiles[i, j] = new PacmanLife(point); break;

                        case '=':
                        case ' ': Tiles[i, j] = new Floor(point); break;
                        case 'p': Tiles[i, j] = new Door(point); break;
                        case '1': Tiles[i, j] = new _1(point); break;
                        case '2': Tiles[i, j] = new _2(point); break;
                        case '3': Tiles[i, j] = new _3(point); break;
                        case '4': Tiles[i, j] = new _4(point); break;
                        case '5': Tiles[i, j] = new _5(point); break;
                        case '6': Tiles[i, j] = new _6(point); break;
                        case '7': Tiles[i, j] = new _7(point); break;
                        case '8': Tiles[i, j] = new _8(point); break;
                        case '9': Tiles[i, j] = new _9(point); break;

                        case 'a': Tiles[i, j] = new _a(point); break;
                        case 'b': Tiles[i, j] = new _b(point); break;
                        case 'c': Tiles[i, j] = new _c(point); break;
                        case 'd': Tiles[i, j] = new _d(point); break;

                        case 'l': Tiles[i, j] = new l(point); break;
                        case 'r': Tiles[i, j] = new r(point); break;

                        default: throw new System.Exception($"Unknown tile! ({i};{j})");
                    }
                }

            return this;
        }

        //PUT NEW FRUIT
         //if (_gameState.Pacman.DotsEaten == 70)
         //       _gameState.Level.Tiles[14, 20] = new Cherries(new Point(14, 20));
         //   if (_gameState.Pacman.DotsEaten == 170)
         //       _gameState.Level.Tiles[14, 20] = new Cherries(new Point(14, 20));

        public bool IsWalkableForGhost(Point point)
            => (IsWalkable(point) || Tiles[point.X, point.Y].IsWalkableForGhost);

        public bool IsWalkable(Point point)
        {
            if ((point.X < 0 || point.X >= Width) ||
              (point.Y < 0 || point.Y >= Height))
                return false;

            return (Tiles[point.X, point.Y].IsWalkable);
        }

        public void ChangeTileToFloor(Point point)
            => Tiles[point.X, point.Y] = new Floor(point);
    }
}