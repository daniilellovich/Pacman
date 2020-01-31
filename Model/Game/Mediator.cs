using System.Collections.Generic;

namespace Pacman
{
    public class Mediator
    {
        public Game Game { get; set; }
        public Level Level { get; set; }
        public Pacman Pacman { get; set; }
        public Blinky Blinky { get; set; }
        public Pinky Pinky { get; set; }
        public Inky Inky { get; set; }
        public Clyde Clyde { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public List<Character> Characters { get; set; }
        public GameController GameController { get; set; }
        public ColissionDetector ColissionDetector { get; set; }

        public Mediator(Game game, Level level)
        {
            Game = game;
            Level = level;

            Pacman = new Pacman(this, 3);

            Blinky = new Blinky(this);
            Pinky  = new Pinky(this);
            Inky   = new Inky(this);
            Clyde  = new Clyde(this);

            Ghosts = new List<Ghost> { Blinky, Pinky, Inky, Clyde };
            Characters = new List<Character>() { Pacman, Blinky, Pinky, Inky, Clyde };

            GameController = new GameController(game, this);
            ColissionDetector = new ColissionDetector(game, this);
        }
    }
}