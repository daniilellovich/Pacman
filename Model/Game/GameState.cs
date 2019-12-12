using System.Collections.Generic;

namespace Pacman
{
    public class GameState
    {
        public GhostPathFinder GhostPathFinder { get; set; }
        public GhostsController GhostsController { get; set; }
        public Level Level { get; set; }
        public Pacman Pacman { get; set; }
        public Ghost Blinky { get; set; }
        public Ghost Pinky { get; set; }
        public Ghost Inky { get; set; }
        public Ghost Clyde { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public ItemsController ItemsController { get; set; }
    }
}