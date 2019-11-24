using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace Pacman
{
    public class Behaviors //исключить поворот налево при выходе из домика
    {
        #region variables & supporting functions declaration

        Point coord = new Point();
        Point pacmanLocation, blinkyLocation, pinkyLocation, inkyLocation, clydeLocation, pacmanLocForInky, dest,
          blinkyFinishLocation, pinkyFinishLocation, clydeFinishLocation, inkyFinishLocation, point,
          blinkyPreviousLocation, pinkyPreviousLocation, inkyPreviousLocation, clydePreviousLocation, randomPoint;

        bool blinkyFlag = false;
        List<Point> blinkyPassedWay = new List<Point>();
        bool pinkyFlag = false;
        List<Point> pinkyPassedWay = new List<Point>();
        bool inkyFlag = false;
        List<Point> inkyPassedWay = new List<Point>();
        bool clydeFlag = false;
        List<Point> clydePassedWay = new List<Point>();

        readonly Random random = new Random();

        bool PacmanIsFar(Point clyde, Point pacman)
            => (Math.Sqrt(clyde.X * pacman.X - clyde.Y * pacman.Y) > 8);        
        #endregion

        #region Chase Mode
        public Point BlinkyChaseMode(PointF blinkyLocationF)
        {
            blinkyLocation = new Point(Convert.ToInt32(blinkyLocationF.Y), Convert.ToInt32(blinkyLocationF.X));

            blinkyFinishLocation = pacmanLocation;
            if ((blinkyLocation.X == 17) && (blinkyLocation.Y > 21))
                blinkyFinishLocation = new Point(17, 27);
            if ((blinkyLocation.X == 17) && (blinkyLocation.Y < 6))
                blinkyFinishLocation = new Point(17, 0);

            coord = Game.State.GhostPathFinder.FindPath(blinkyLocation, blinkyFinishLocation, blinkyPreviousLocation);

            #region record of the last location
            if (blinkyFlag == false)
            {
                blinkyPassedWay.Add(blinkyLocation);
                blinkyFlag = true;
            }

            if (blinkyPassedWay[blinkyPassedWay.Count - 1] != blinkyLocation)
            {
                blinkyPassedWay.Add(blinkyLocation);

                if (blinkyPassedWay.Count > 3)
                    blinkyPassedWay.RemoveAt(0);
            }

            if (blinkyPassedWay.Count > 2)
                blinkyPreviousLocation = blinkyPassedWay[blinkyPassedWay.Count - 2];
            #endregion


            if (coord.IsEmpty)
                return blinkyLocation;
            else
                return coord;
        }

        public Point PinkyChaseMode(PointF pinkyLocationF)
        {
            pinkyLocation = new Point(Convert.ToInt32(pinkyLocationF.Y), Convert.ToInt32(pinkyLocationF.X));

            #region record of next location

            int X = pacmanLocation.X;
            int Y = pacmanLocation.Y;
            int p = 0, q = 0;

            switch (Game.State.Pacman._lastDirection)
            {
                case Pacman.Directions.up:
                    p = -5; q = 0;
                    break;
                case Pacman.Directions.right:
                    p = 0; q = 5;
                    break;
                case Pacman.Directions.down:
                    p = 5; q = 0;
                    break;
                case Pacman.Directions.left:
                    p = 0; q = -5;
                    break;
                case Pacman.Directions.nowhere:
                    p = 0; q = 0;
                    break;
            }

            Point[] nextCells = new Point[5];
            for (int i = 0; i < 5; i++)
            {
                if (q > 0) q--; if (q < 0) q++;
                if (p > 0) p--; if (p < 0) p++;
                pinkyFinishLocation = new Point(X + p, Y + q);
                nextCells[i] = pinkyFinishLocation;
            }

            for (int i = 0; i < 5; i++)
            {
                if (!Game.State.Level.IsWalkablePoint(nextCells[i]))
                //if ((.X < 0 || nextCells[i].X >= Height) ||
                //   (nextCells[i].Y < 0 || nextCells[i].Y >= Width))
                //    continue;
                //if ((lines[nextCells[i].X][nextCells[i].Y] != '.') &&
                //       (lines[nextCells[i].X][nextCells[i].Y] != ' '))
                    continue;
                //         if (nextCells[i] == pinkyLocation)    //у пинки есть ошибка - при достижении цели может менять направление
                //             continue;
                pinkyFinishLocation = nextCells[i];
                break;
            }

            #endregion

            if ((Math.Abs(pinkyLocation.X - pinkyFinishLocation.X) < 0.5) || (Math.Abs(pinkyLocation.Y - pinkyFinishLocation.Y) < 0.5))
                pinkyFinishLocation = pacmanLocation;

            //        Game.State.Level.Tiles[pinkyFinishLocation.Y, pinkyFinishLocation.X] = new ClydeFinishPoint(new Point(pinkyFinishLocation.Y, pinkyFinishLocation.X) };


            coord = Game.State.GhostPathFinder.FindPath(pinkyLocation, pinkyFinishLocation, pinkyPreviousLocation);

            #region record of last location

            if (pinkyFlag == false)
            {
                pinkyPassedWay.Add(pinkyLocation);
                pinkyFlag = true;
            }

            if (pinkyPassedWay[pinkyPassedWay.Count - 1] != pinkyLocation)
            {
                pinkyPassedWay.Add(pinkyLocation);

                if (pinkyPassedWay.Count > 3)
                    pinkyPassedWay.RemoveAt(0);
            }

            if (pinkyPassedWay.Count > 2)
                pinkyPreviousLocation = pinkyPassedWay[pinkyPassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return pacmanLocation;
            else
                return coord;
        }

        public Point InkyChaseMode(PointF inkyLocationF)
        {
            inkyLocation = new Point(Convert.ToInt32(inkyLocationF.Y), Convert.ToInt32(inkyLocationF.X));

            int X = pacmanLocation.X;
            int Y = pacmanLocation.Y;

            switch (Game.State.Pacman._lastDirection)
            {
                case Pacman.Directions.up:
                    pacmanLocForInky = new Point(X - 2, Y);
                    break;
                case Pacman.Directions.right:
                    pacmanLocForInky = new Point(X, Y + 2);
                    break;
                case Pacman.Directions.down:
                    pacmanLocForInky = new Point(X + 2, Y);
                    break;
                case Pacman.Directions.left:
                    pacmanLocForInky = new Point(X, Y - 2);
                    break;
                case Pacman.Directions.nowhere:
                    pacmanLocForInky = new Point(X, Y);
                    break;
            }

            int xLine = pacmanLocForInky.X - blinkyLocation.X;
            int yLine = pacmanLocForInky.Y - blinkyLocation.Y;

            float tgA = 0;

            if (blinkyLocation.X + 2 * xLine != 0)
                tgA = ((float)yLine) / ((float)xLine);

            inkyFinishLocation = new Point(blinkyLocation.X + 2 * xLine, blinkyLocation.Y + 2 * yLine);

            if (Game.State.Level.IsWalkablePoint(inkyFinishLocation))
                inkyFinishLocation = pacmanLocation;

      //Game.State.Level.Tiles[inkyFinishLocation.Y, inkyFinishLocation.X] = new InkyFinishPoint(new Point(inkyFinishLocation.Y, inkyFinishLocation.X) };

            if ((Math.Abs(inkyLocation.X - inkyFinishLocation.X) < 0.5) || (Math.Abs(inkyLocation.Y - inkyFinishLocation.Y) < 0.5))
                inkyFinishLocation = pacmanLocation;

            coord = Game.State.GhostPathFinder.FindPath(inkyLocation, inkyFinishLocation, inkyPreviousLocation);

            #region record of the last location

            if (inkyFlag == false)
            {
                inkyPassedWay.Add(inkyLocation);
                inkyFlag = true;
            }

            if (inkyPassedWay[inkyPassedWay.Count - 1] != inkyLocation)
            {
                inkyPassedWay.Add(inkyLocation);

                if (inkyPassedWay.Count > 3)
                    inkyPassedWay.RemoveAt(0);
            }

            if (inkyPassedWay.Count > 2)
                inkyPreviousLocation = inkyPassedWay[inkyPassedWay.Count - 2];

            #endregion

            if (coord.IsEmpty)
                return inkyLocation;
            else
                return coord;
        }

        public Point ClydeChaseMode(PointF clydeLocationF)
        {
            clydeLocation = new Point(Convert.ToInt32(clydeLocationF.Y), Convert.ToInt32(clydeLocationF.X));

            if ((PacmanIsFar(clydeLocation, pacmanLocation)))
                clydeFinishLocation = pacmanLocation;
            else
                clydeFinishLocation = Game.State.Level.ClydeCorner;              //Нужно чтобы обходил угол

            if ((Math.Abs(clydeLocation.X - clydeFinishLocation.X) < 0.5) || (Math.Abs(clydeLocation.Y - clydeFinishLocation.Y) < 0.5))
                clydeFinishLocation = pacmanLocation;

            #region record of the last location

            if (clydeFlag == false)
            {
                clydePassedWay.Add(clydeLocation);
                clydeFlag = true;
            }

            if (clydePassedWay[clydePassedWay.Count - 1] != clydeLocation)
            {
                clydePassedWay.Add(clydeLocation);

                if (clydePassedWay.Count > 3)
                    clydePassedWay.RemoveAt(0);
            }

            if (clydePassedWay.Count > 2)
                clydePreviousLocation = clydePassedWay[clydePassedWay.Count - 2];

            #endregion

            coord = Game.State.GhostPathFinder.FindPath(clydeLocation, clydeFinishLocation, clydePreviousLocation);


            if (coord.IsEmpty)
                return clydeLocation;
            else
                return coord;
        } //исправить или на и
        #endregion

        #region Scatter Mode

        public Point BlinkyScatterMode(PointF blinkyLocationF)
        {
            blinkyLocation = new Point(Convert.ToInt32(blinkyLocationF.Y), Convert.ToInt32(blinkyLocationF.X));
            blinkyFinishLocation = Game.State.Level.BlinkyCorner;

            if ((Math.Abs(blinkyLocation.X - Game.State.Level.BlinkyCorner.X) < 2) && (Math.Abs(blinkyLocation.Y - Game.State.Level.BlinkyCorner.Y) < 2))
                blinkyFinishLocation = new Point(4, 26);

            //         if ((blinkyLocation.X == Game.State.Level.BlinkyCorner.X) && (blinkyLocation.Y == Game.State.Level.BlinkyCorner.Y))
            //             blinkyFinishLocation = new Point(4, 26);

            coord = Game.State.GhostPathFinder.FindPath(blinkyLocation, blinkyFinishLocation, blinkyPreviousLocation);

            #region record of the last location
            if (blinkyFlag == false)
            {
                blinkyPassedWay.Add(blinkyLocation);
                blinkyFlag = true;
            }

            if (blinkyPassedWay[blinkyPassedWay.Count - 1] != blinkyLocation)
            {
                blinkyPassedWay.Add(blinkyLocation);

                if (blinkyPassedWay.Count > 3)
                    blinkyPassedWay.RemoveAt(0);
            }

            if (blinkyPassedWay.Count > 2)
                blinkyPreviousLocation = blinkyPassedWay[blinkyPassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return blinkyLocation;
            else
                return coord;
        }

        public Point PinkyScatterMode(PointF pinkyLocationF)
        {
            pinkyLocation = new Point(Convert.ToInt32(pinkyLocationF.Y), Convert.ToInt32(pinkyLocationF.X));
            pinkyFinishLocation = Game.State.Level.PinkyCorner;              //Нужно чтобы обходил угол

            if ((Math.Abs(pinkyLocation.X - Game.State.Level.PinkyCorner.X) < 2) && (Math.Abs(pinkyLocation.Y - Game.State.Level.PinkyCorner.Y) < 2))
                pinkyFinishLocation = new Point(4, 1);

            coord = Game.State.GhostPathFinder.FindPath(pinkyLocation, pinkyFinishLocation, pinkyPreviousLocation);

            #region record of the last location
            if (pinkyFlag == false)
            {
                pinkyPassedWay.Add(pinkyLocation);
                pinkyFlag = true;
            }

            if (pinkyPassedWay[pinkyPassedWay.Count - 1] != pinkyLocation)
            {
                pinkyPassedWay.Add(pinkyLocation);

                if (pinkyPassedWay.Count > 3)
                    pinkyPassedWay.RemoveAt(0);
            }

            if (pinkyPassedWay.Count > 2)
                pinkyPreviousLocation = pinkyPassedWay[pinkyPassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return pinkyLocation;
            else
                return coord;
        }

        public Point InkyScatterMode(PointF inkyLocationF)
        {
            inkyLocation = new Point(Convert.ToInt32(inkyLocationF.Y), Convert.ToInt32(inkyLocationF.X));
            inkyFinishLocation = Game.State.Level.InkyCorner;              //Нужно чтобы обходил угол

            if ((Math.Abs(inkyLocation.X - Game.State.Level.InkyCorner.X) < 2) && (Math.Abs(inkyLocation.Y - Game.State.Level.InkyCorner.Y) < 2))
                inkyFinishLocation = new Point(32, 15);

            coord = Game.State.GhostPathFinder.FindPath(inkyLocation, inkyFinishLocation, inkyPreviousLocation);

            #region record of the last location
            if (inkyFlag == false)
            {
                inkyPassedWay.Add(inkyLocation);
                inkyFlag = true;
            }

            if (inkyPassedWay[inkyPassedWay.Count - 1] != inkyLocation)
            {
                inkyPassedWay.Add(inkyLocation);

                if (inkyPassedWay.Count > 3)
                    inkyPassedWay.RemoveAt(0);
            }

            if (inkyPassedWay.Count > 2)
                inkyPreviousLocation = inkyPassedWay[inkyPassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return inkyLocation;
            else
                return coord;
        }

        public Point ClydeScatterMode(PointF clydeLocationF)
        {
            clydeLocation = new Point(Convert.ToInt32(clydeLocationF.Y), Convert.ToInt32(clydeLocationF.X));
            clydeFinishLocation = Game.State.Level.ClydeCorner;

            if ((Math.Abs(clydeLocation.X - Game.State.Level.ClydeCorner.X) < 2) && (Math.Abs(clydeLocation.Y - Game.State.Level.ClydeCorner.Y) < 2))
                clydeFinishLocation = new Point(32, 12);

            coord = Game.State.GhostPathFinder.FindPath(clydeLocation, clydeFinishLocation, clydePreviousLocation);

            #region record of the last location
            if (clydeFlag == false)
            {
                clydePassedWay.Add(clydeLocation);
                clydeFlag = true;
            }

            if (clydePassedWay[clydePassedWay.Count - 1] != clydeLocation)
            {
                clydePassedWay.Add(clydeLocation);

                if (clydePassedWay.Count > 3)
                    clydePassedWay.RemoveAt(0);
            }

            if (clydePassedWay.Count > 2)
                clydePreviousLocation = clydePassedWay[clydePassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return clydeLocation;
            else
                return coord;
        }

        #endregion

        #region Frightened Mode

        //int rBlinky;
        //public Point BlinkyFrightenedMode(PointF blinkyLocationF)  //слишком часто меняет целевую клетку
        //{
        //    blinkyLocation = new Point(Convert.ToInt32(blinkyLocationF.Y, Convert.ToInt32(blinkyLocationF.X));

        //    List<Point> validPoints = new List<Point>();

        //    Point[] neighbourPoints = new Point[4];
        //    neighbourPoints[0] = new Point(blinkyLocation.X, blinkyLocation.Y + 1);
        //    neighbourPoints[1] = new Point(blinkyLocation.X + 1, blinkyLocation.Y);
        //    neighbourPoints[2] = new Point(blinkyLocation.X, blinkyLocation.Y - 1);
        //    neighbourPoints[3] = new Point(blinkyLocation.X - 1, blinkyLocation.Y);

        //    foreach (var point in neighbourPoints)
        //    {
        //        if ((point.X < 0) || (point.X >= Height))
        //            continue;
        //        if ((point.Y < 0) || (point.Y >= Width))
        //            continue;
        //        if (point == blinkyPreviousLocation)
        //            continue;
        //        if ((lines[point.X][point.Y] != '.') && (lines[point.X][point.Y] != ' '))
        //            continue;

        //        validPoints.Add(point);
        //    }

        //    if (validPoints.Count == 1)
        //    {
        //        rBlinky = 0;
        //        randomPoint = validPoints[rBlinky];
        //    }
        //    else
        //    {
        //        rBlinky = random.Next(Game.State.ItemsController.indexList.Count); //проверять прошлую локацию           
        //        randomPoint = Game.State.ItemsController.indexList[rBlinky];
        //    }

        //    #region record of the last location
        //    if (blinkyFlag == false)
        //    {
        //        blinkyPassedWay.Add(blinkyLocation);
        //        blinkyFlag = true;
        //    }

        //    if (blinkyPassedWay[blinkyPassedWay.Count - 1] != blinkyLocation)
        //    {
        //        blinkyPassedWay.Add(blinkyLocation);

        //        if (blinkyPassedWay.Count > 3)
        //            blinkyPassedWay.RemoveAt(0);
        //    }

        //    if (blinkyPassedWay.Count > 2)
        //        blinkyPreviousLocation = blinkyPassedWay[blinkyPassedWay.Count - 2];
        //    #endregion

        //    if (coord.IsEmpty)
        //        return blinkyLocation;
        //    else
        //        return randomPoint;
        //}

        public Point BlinkyFrightenedMode(PointF blinkyLocationF)
        {
            blinkyLocation = new Point(Convert.ToInt32(blinkyLocationF.Y), Convert.ToInt32(blinkyLocationF.X));

            blinkyFinishLocation = Game.State.Level.BlinkyCorner;

            coord = Game.State.GhostPathFinder.FindPath(blinkyLocation, blinkyFinishLocation, blinkyPreviousLocation);

            #region record of the last location
            if (blinkyFlag == false)
            {
                blinkyPassedWay.Add(blinkyLocation);
                blinkyFlag = true;
            }

            if (blinkyPassedWay[blinkyPassedWay.Count - 1] != blinkyLocation)
            {
                blinkyPassedWay.Add(blinkyLocation);

                if (blinkyPassedWay.Count > 3)
                    blinkyPassedWay.RemoveAt(0);
            }

            if (blinkyPassedWay.Count > 2)
                blinkyPreviousLocation = blinkyPassedWay[blinkyPassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return blinkyLocation;
            else
                return coord;
        }

        public Point PinkyFrightenedMode(PointF pinkyLocationF)
        {
            pinkyLocation = new Point(Convert.ToInt32(pinkyLocationF.Y), Convert.ToInt32(pinkyLocationF.X));

            pinkyFinishLocation = Game.State.Level.PinkyCorner;

            coord = Game.State.GhostPathFinder.FindPath(pinkyLocation, pinkyFinishLocation, pinkyPreviousLocation);

            #region record of the last location
            if (pinkyFlag == false)
            {
                pinkyPassedWay.Add(pinkyLocation);
                pinkyFlag = true;
            }

            if (pinkyPassedWay[pinkyPassedWay.Count - 1] != pinkyLocation)
            {
                pinkyPassedWay.Add(pinkyLocation);

                if (pinkyPassedWay.Count > 3)
                    pinkyPassedWay.RemoveAt(0);
            }

            if (pinkyPassedWay.Count > 2)
                pinkyPreviousLocation = pinkyPassedWay[pinkyPassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return pinkyLocation;
            else
                return coord;
        }

        public Point InkyFrightenedMode(PointF inkyLocationF)
        {
            inkyLocation = new Point(Convert.ToInt32(inkyLocationF.Y), Convert.ToInt32(inkyLocationF.X));

            inkyFinishLocation = Game.State.Level.InkyCorner;

            coord = Game.State.GhostPathFinder.FindPath(inkyLocation, inkyFinishLocation, inkyPreviousLocation);

            #region record of the last location
            if (inkyFlag == false)
            {
                inkyPassedWay.Add(inkyLocation);
                inkyFlag = true;
            }

            if (inkyPassedWay[inkyPassedWay.Count - 1] != inkyLocation)
            {
                inkyPassedWay.Add(inkyLocation);

                if (inkyPassedWay.Count > 3)
                    inkyPassedWay.RemoveAt(0);
            }

            if (inkyPassedWay.Count > 2)
                inkyPreviousLocation = inkyPassedWay[inkyPassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return inkyLocation;
            else
                return coord;
        }

        public Point ClydeFrightenedMode(PointF clydeLocationF)
        {
            clydeLocation = new Point(Convert.ToInt32(clydeLocationF.Y), Convert.ToInt32(clydeLocationF.X));

            clydeFinishLocation = Game.State.Level.ClydeCorner;

            coord = Game.State.GhostPathFinder.FindPath(clydeLocation, clydeFinishLocation, clydePreviousLocation);

            #region record of the last location
            if (clydeFlag == false)
            {
                clydePassedWay.Add(clydeLocation);
                clydeFlag = true;
            }

            if (clydePassedWay[clydePassedWay.Count - 1] != clydeLocation)
            {
                clydePassedWay.Add(clydeLocation);

                if (clydePassedWay.Count > 3)
                    clydePassedWay.RemoveAt(0);
            }

            if (clydePassedWay.Count > 2)
                clydePreviousLocation = clydePassedWay[clydePassedWay.Count - 2];
            #endregion

            if (coord.IsEmpty)
                return clydeLocation;
            else
                return coord;
        }
        #endregion

        #region Output
        public string Output()
        {
            string str = null;
            str += String.Format("PACMAN  {{X= {0};  Y= {1}}}", pacmanLocation.X, pacmanLocation.Y);
            str += String.Format("\nBLYNKY  {{X= {0};  Y= {1}}}", blinkyLocation.X, blinkyLocation.Y);
            str += String.Format("\nPINKY    {{X= {0};  Y= {1}}}", pinkyLocation.X, pinkyLocation.Y);
            str += String.Format("\nINKY      {{X= {0};  Y= {1}}}", inkyLocation.X, inkyLocation.Y);
            str += String.Format("\nCLYDE    {{X= {0};  Y= {1}}}", clydeLocation.X, clydeLocation.Y);
         //   str += "\n" + Game.State.BehaviorChanger._currentMode + "    " + Game.State.BehaviorChanger._currentTime;// (сколько осталось до окончания режима)
            return str;
        }

        //public void ShowingMechanismForBlinky()
        //{
        //    Point point1 = pacmanLocation;
        //    Game.State.Level.Tiles[point1.Y, point1.X] = new BlinkyFinishPoint(new Point(point1.Y, point1.X));
        //}

        //public void ShowingMechanismForPinky()
        //{
        //    Point point1 = pinkyFinishLocation;
        //    if ((point1.X > 0 || point1.X <= Height) || (point1.Y > 0 || point1.Y <= Width))
        //        Game.State.Level.Tiles[point1.Y, point1.X] = new PinkyFinishPoint(new Point(point1.Y, point1.X));
        //}

        //public void ShowingMechanismForInky()
        //{
        //    Point point1 = inkyFinishLocation;
        //    if (Game.State.Level.IsWalkablePoint(point1))
        //        Game.State.Level.Tiles[point1.Y, point1.X] = new InkyFinishPoint(new Point(point1.Y, point1.X));
        // //   if ((point1.X > 0 || point1.X <= Height) || (point1.Y > 0 || point1.Y <= Width))
        //}

        //public void ShowingMechanismForClyde()
        //{
        //    Point point1 = clydeFinishLocation;
        //    Game.State.Level.Tiles[point1.Y, point1.X] = new ClydeFinishPoint(new Point(point1.Y, point1.X));
        //}

        public void ShowPoints()  //вызывать, сделать стирание предыдущих
        {
            //      ShowingMechanismForBlinky();
            //      ShowingMechanismForPinky();
            //      ShowingMechanismForInky();
            //      ShowingMechanismForClyde();


        }
        #endregion

        #region Returning Home Mode
        public Point BlinkyReturningHome(PointF blinkyLocationF)
        {
            SoundController.PlaySound("Invincible");
            Point blinkyLocation = new Point(Convert.ToInt32(blinkyLocationF.Y), Convert.ToInt32(blinkyLocationF.X));
            Point blinkyHome = new Point(Convert.ToInt32(Game.State.Blinky.Home.Y), Convert.ToInt32(Game.State.Blinky.Home.X));
            Point prevLoc = new Point();
            coord = Game.State.GhostPathFinder.FindPath(blinkyLocation, blinkyHome, prevLoc);
            return coord;
        }

        public Point PinkyReturningHome(PointF pinkyLocationF)
        {

            SoundController.PlaySound("Invincible");
            Point pinkyLocation = new Point(Convert.ToInt32(pinkyLocationF.Y), Convert.ToInt32(pinkyLocationF.X));
            Point pinkyHome = new Point(Convert.ToInt32(Game.State.Pinky.Home.Y), Convert.ToInt32(Game.State.Pinky.Home.X));
            Point prevLoc = new Point();
            coord = Game.State.GhostPathFinder.FindPath(pinkyLocation, pinkyHome, prevLoc);
            return coord;
        }

        public Point InkyReturningHome(PointF inkyLocationF)
        {
            SoundController.PlaySound("Invincible");
            Point inkyLocation = new Point(Convert.ToInt32(inkyLocationF.Y), Convert.ToInt32(inkyLocationF.X));
            Point inkyHome = new Point(Convert.ToInt32(Game.State.Inky.Home.Y), Convert.ToInt32(Game.State.Inky.Home.X));
            Point prevLoc = new Point();
            coord = Game.State.GhostPathFinder.FindPath(inkyLocation, inkyHome, prevLoc);
            return coord;
        }

        public Point ClydeReturningHome(PointF clydeLocationF)
        {
            SoundController.PlaySound("Invincible");
            Point clydeLocation = new Point(Convert.ToInt32(clydeLocationF.Y), Convert.ToInt32(clydeLocationF.X));
            Point clydeHome = new Point(Convert.ToInt32(Game.State.Clyde.Home.Y), Convert.ToInt32(Game.State.Clyde.Home.X));
            Point prevLoc = new Point();
            coord = Game.State.GhostPathFinder.FindPath(clydeLocation, clydeHome, prevLoc);
            return coord;
        }
        #endregion

        #region Check If Ghost Is At Home
        public void CheckIfBlinkyAtHome() // передавать объект и локацию
        {
            if ((Math.Abs(Game.State.Blinky.Location.X - Game.State.Blinky.Home.X) < 0.5) && (Math.Abs(Game.State.Blinky.Location.Y - Game.State.Blinky.Home.Y) < 0.5))
            {
                Game.State.Blinky = new Ghost(GameResources.Blinky, Game.State.Blinky.Home, 0.09f, Game.State.Behaviors.BlinkyScatterMode);
    //            Game.State.Blinky._isEaten = false;
            }
        }

        public void CheckIfPinkyAtHome()
        {
            if ((Math.Abs(Game.State.Pinky.Location.X - Game.State.Pinky.Home.X) < 0.5) && (Math.Abs(Game.State.Pinky.Location.Y - Game.State.Pinky.Home.Y) < 0.5))
            {
                Game.State.Pinky = new Ghost(GameResources.Pinky, Game.State.Pinky.Home, 0.09f, Game.State.Behaviors.PinkyScatterMode);
     //           Game.State.Pinky._isEaten = false;
            }
        }

        public void CheckIfInkyAtHome()
        {
            if ((Math.Abs(Game.State.Inky.Location.X - Game.State.Inky.Home.X) < 0.5) && (Math.Abs(Game.State.Inky.Location.Y - Game.State.Inky.Home.Y) < 0.5))
            {
                Game.State.Inky = new Ghost(GameResources.Inky, Game.State.Inky.Home, 0.09f, Game.State.Behaviors.InkyScatterMode);
      //          Game.State.Inky._isEaten = false;
            }
        }

        public void CheckIfClydeAtHome()
        {
            if ((Math.Abs(Game.State.Clyde.Location.X - Game.State.Clyde.Home.X) < 0.5) && (Math.Abs(Game.State.Clyde.Location.Y - Game.State.Clyde.Home.Y) < 0.5))
            {
                Game.State.Clyde = new Ghost(GameResources.Clyde, Game.State.Clyde.Home, 0.09f, Game.State.Behaviors.ClydeScatterMode);
    //            Game.State.Clyde._isEaten = false;
            }
        }
        #endregion
    }
}