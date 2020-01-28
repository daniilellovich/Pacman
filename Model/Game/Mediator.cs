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
        public GameController GameController;
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

            GameController = new GameController(this);
            ItemsController = new ItemsController(this);
        }
    }
}