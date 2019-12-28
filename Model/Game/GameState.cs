using System.Collections.Generic;

namespace Pacman
{
    public class Mediator
    {
        public Level Level;
        public Pacman Pacman;
        public Blinky Blinky;
        public Pinky Pinky;
        public Inky Inky;
        public Clyde Clyde;
        public List<Ghost> Ghosts;
        public GhostPathFinder GhostPathFinder;
        public GhostsController GhostsController;
        public ItemsController ItemsController;

        public Mediator()
        {
            Level = new Level();

            Pacman = new Pacman(this);
            Blinky = new Blinky(this);
            Pinky  = new Pinky(this);
            Inky   = new Inky(this);
            Clyde  = new Clyde(this);
            Ghosts = new List<Ghost>() { Blinky, Pinky, Inky, Clyde };

            GhostPathFinder = new GhostPathFinder(this);
            GhostsController = new GhostsController(this);
            ItemsController = new ItemsController(this);
        }
    }
}