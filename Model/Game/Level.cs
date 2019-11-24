using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pacman
{
    public class Level
    {
        public Tile[,] Tiles { get; private set; }
        public int Width     { get; private set; }
        public int Height    { get; private set; }

        string[] _lines;

        public Level()
        {
            _lines = Properties.Resources.PacmanMap.Split('\n');
            Width = _lines[0].Length - 1;
            Height = _lines.Length - 1;
            FillLevel();
            InitItems();
        }

        Level FillLevel()
        {
            Tiles = new Tile[Width, Height];

            for (int j = 0; j < Height; j++)
                for (int i = 0; i < Width; i++)
                {
                    Point point = new Point(i, j);
                    switch (_lines[j][i])
                    {
                        case '.':
                        case '=':
                        case 'p': Tiles[i, j] = new Door(point); break;
                        case ' ': Tiles[i, j] = new Floor(point); break;
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

                        case 'e': Tiles[i, j] = new Dot(point); break;

                        default: throw new System.Exception($"Unknown tile! ({i};{j})");
                    }
                }

            return this;
        }

        void InitItems()
        {
            for (int l = 0; l < Height; l++)
                for (int k = 0; k < Width; k++)
                    if (_lines[l][k] == '.')
                        Tiles[k, l] = new Dot(new Point(k, l));

            Tiles[1, 6] = new Energizer(new Point(1, 6));
            Tiles[26, 6] = new Energizer(new Point(26, 6));
            Tiles[1, 26] = new Energizer(new Point(1, 26));
            Tiles[26, 26] = new Energizer(new Point(26, 26));

            Tiles[0, 34] = new PacmanLife(new Point(0, 34));// убрать отсюда
            Tiles[2, 34] = new PacmanLife(new Point(2, 34));
            Tiles[4, 34] = new PacmanLife(new Point(4, 34));

            Tiles[0, 17] = new l(new Point(0, 17)); //порталы
            Tiles[27, 17] = new r(new Point(27, 17));
        }

        public bool IsWalkablePoint(Point point)
        {
            //      return Tiles[point.X, point.Y].Walkable;

            if ((point.X < 0 || point.X >= Height) ||
              (point.Y < 0 || point.Y >= Width))
                return false;

            if ((_lines[point.X][point.Y] != '.') &&
                (_lines[point.X][point.Y] != ' '))
                return false;
            else
                return true;
        }

        //public void ChangeTileTo(Tile tile)
        //{

        //}

        #region Ghosts corners
        public Point BlinkyCorner { get; } = new Point(4, 21);    //26 Game.State.Level.BlinkyCorner
        public Point InkyCorner { get; } = new Point(32, 26);     //15 Game.State.Level.InkyCorner
        public Point PinkyCorner { get; } = new Point(4, 6);      //1 Game.State.Level.PinkyCorner
        public Point ClydeCorner { get; } = new Point(32, 1);     //12 Game.State.Level.ClydeCorner
        #endregion
    }
}