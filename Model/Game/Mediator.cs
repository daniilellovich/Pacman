using System.Collections.Generic;

namespace Pacman
{
    public class Mediator
    {
        public Game Game;
        public Level Level;
        public Pacman Pacman;
        public Blinky Blinky;
        public Pinky Pinky;
        public Inky Inky;
        public Clyde Clyde;
        public List<Ghost> Ghosts;
        public List<Character> Characters;
        public GameController GameController;
        public ColissionDetector ColissionDetector;

        public Mediator(Game game, Level level, int levelNumber)
        {
            Game = game;
            Level = level;

            Pacman = new Pacman(this);
            Blinky = new Blinky(this);
            Pinky  = new Pinky(this);
            Inky   = new Inky(this);
            Clyde  = new Clyde(this);
            Ghosts = new List<Ghost>() { Blinky, Pinky, Inky, Clyde };
            Characters = new List<Character>() { Pacman, Blinky };

            GameController = new GameController(game, this, levelNumber);
            ColissionDetector = new ColissionDetector(this);
        }
    }
}