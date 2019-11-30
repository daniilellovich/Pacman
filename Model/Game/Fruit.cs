using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Model.Game
{
    class Fruit : Tile
    {
        public int Time { get; protected set; }
        public int Level { get; protected set; }

        public Fruit(Point location) : base(location) { }
    }
}
