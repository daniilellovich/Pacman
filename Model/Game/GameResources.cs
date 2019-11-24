using System.Drawing;
using Pacman.Properties;

namespace Pacman
{
    public static class GameResources
    {
        #region characters
        public static Bitmap Pacman        = Load(Properties.Resources.pacman);
        public static Bitmap Blinky        = Load(Properties.Resources.blinky);
        public static Bitmap Pinky         = Load(Properties.Resources.pinky);
        public static Bitmap Inky          = Load(Properties.Resources.inky);
        public static Bitmap Clyde         = Load(Properties.Resources.clyde);
        public static Bitmap Fright        = Load(Properties.Resources.fright);
        public static Bitmap FrightEnd     = Load(Properties.Resources.frightEnd);
        public static Bitmap GhostEyes     = Load(Properties.Resources.ghostEyes);
        public static Bitmap PacmanDeath   = Load(Properties.Resources.pacmanDepth);
        public static Bitmap PacmanVictory = Load(Properties.Resources.pacmanWon);
        #endregion

        #region items
        public static Bitmap Empty              = new Bitmap(Game.TileSize.Width, Game.TileSize.Height);
        public static Bitmap Floor              = LoadTile(Resources.floor);
        public static Bitmap Cell               = LoadTile(Resources.cell);
        public static Bitmap Dot                = LoadTile(Resources.dot);
        public static Bitmap Energizer          = LoadTile(Resources.energizer);
        public static Bitmap PacmanLife        = LoadTile(Resources.pacmanLife);
        public static Bitmap BlinkyFinishPoint  = LoadTile(Resources.blinkyFinishPoint);
        public static Bitmap PinkyFinishPoint   = LoadTile(Resources.pinkyFinishPoint);
        public static Bitmap InkyFinishPoint    = LoadTile(Resources.inkyFinishPoint);
        public static Bitmap ClydeFinishPoint   = LoadTile(Resources.clydeFinishPoint);
        #endregion

        #region fruits
        public static Bitmap Apple      = LoadTile(Properties.Resources.Apple);
        public static Bitmap Cherries   = LoadTile(Properties.Resources.Cherries);
        public static Bitmap Grapes     = LoadTile(Properties.Resources.Grapes);
        public static Bitmap Galaxian   = LoadTile(Properties.Resources.Galaxian);
        public static Bitmap Key        = LoadTile(Properties.Resources.Key);
        public static Bitmap Strawberry = LoadTile(Properties.Resources.Strawberry);
        public static Bitmap Bell       = LoadTile(Properties.Resources.Bell);
        public static Bitmap Peach      = LoadTile(Properties.Resources.Peach);
        #endregion

        #region walls
        public static Bitmap _1 = LoadTile(Resources._1);  
        public static Bitmap _2 = LoadTile(Resources._2);                                  
        public static Bitmap _3 = LoadTile(Resources._3);       //   1(a)   2    3(b)
        public static Bitmap _4 = LoadTile(Resources._4);       //   
        public static Bitmap _5 = LoadTile(Resources._5);       //    4     5     6
        public static Bitmap _6 = LoadTile(Resources._6);       //
        public static Bitmap _7 = LoadTile(Resources._7);       //   7(c)   8    9(d)
        public static Bitmap _8 = LoadTile(Resources._8);       
        public static Bitmap _9 = LoadTile(Resources._9);       
        public static Bitmap _a = LoadTile(Resources._a);       
        public static Bitmap _b = LoadTile(Resources._b);       
        public static Bitmap _c = LoadTile(Resources._c);       
        public static Bitmap _d = LoadTile(Resources._d);
        #endregion
        
        static Bitmap LoadTile(Bitmap img)
        {
            img.SetResolution(64, 64);
            return new Bitmap(img, Game.TileSize);
        }

        static Bitmap Load(Bitmap img)
        {
            img.SetResolution(64, 64);
            return img;
        }
    }
}