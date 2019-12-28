using System.Drawing;
using Pacman.Properties;

namespace Pacman
{
    public static class GameResources
    {
        #region characters
        public static Bitmap Pacman        = LoadSpritePic(Resources.pacman);
        public static Bitmap Blinky        = LoadSpritePic(Resources.blinky);
        public static Bitmap Pinky         = LoadSpritePic(Resources.pinky);
        public static Bitmap Inky          = LoadSpritePic(Resources.inky);
        public static Bitmap Clyde         = LoadSpritePic(Resources.clyde);
        public static Bitmap Fright        = LoadSpritePic(Resources.fright);
        public static Bitmap FrightEnd     = LoadSpritePic(Resources.frightEnd);
        public static Bitmap GhostEyes     = LoadSpritePic(Resources.ghostEyes);
        public static Bitmap PacmanDeath   = LoadSpritePic(Resources.pacmanDepth);
        public static Bitmap PacmanVictory = LoadSpritePic(Resources.pacmanWon);
        #endregion

        #region items
        public static Bitmap Floor      = LoadTile(Resources.floor);
        public static Bitmap Dot        = LoadTile(Resources.dot);
        public static Bitmap Energizer  = LoadTile(Resources.energizer);
        public static Bitmap PacmanLife = LoadTile(Resources.pacmanLife);
        #endregion

        #region fruits
        public static Bitmap Apple      = LoadTile(Resources.Apple);
        public static Bitmap Cherries   = LoadTile(Resources.Cherries);
        public static Bitmap Grapes     = LoadTile(Resources.Grapes);
        public static Bitmap Galaxian   = LoadTile(Resources.Galaxian);
        public static Bitmap Key        = LoadTile(Resources.Key);
        public static Bitmap Strawberry = LoadTile(Resources.Strawberry);
        public static Bitmap Bell       = LoadTile(Resources.Bell);
        public static Bitmap Peach      = LoadTile(Resources.Peach);
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
            return new Bitmap(img, Tile.Size);
        }

        static Bitmap LoadSpritePic(Bitmap img)
        {
            img.SetResolution(64, 64);
            return img;
        }
    }
}