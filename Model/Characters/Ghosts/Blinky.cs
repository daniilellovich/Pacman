using System.Collections.Generic;
using System.Diagnostics;

namespace Pacman
{
    public class Blinky : Ghost
    {
        public Blinky() : base()
        {
            SetSpeed(0.12f);
            ChangeMode(ScatterMode);
            _sprite._image = GameResources.Blinky;
            _color = System.Drawing.Color.Red;
            LocationF = _home = new PointF(13.5f, 14); //как то обозначить что это конст
            _destination = _curMovingMode();
            _prevLoc = new Point(0, 0);
            _corner = new Point(21, 4);
            _corner2 = new Point(26, 4);
        }

        public override Point ChaseMode()
        {           
            _goal = _pacman.Location;
            _path = _pathFinder.FindPath(_prevLoc, Location, _goal);
            return (_path.Count == 1) ? GetRandomNeighbourWalkablePoint() : _path[1];
        }
    }
}