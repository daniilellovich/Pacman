using System.Collections.Generic;

namespace Pacman
{
    public class Game
    {
        public Mediator State;
        public int score, levelNumber = 1;
        public int highScore;

        public Game()
        {
            Level level = new Level();
            State = new Mediator(this, level, levelNumber);
            State.Level.PutPacmanLivesDownside(State.Pacman.GetLives());
        }

        public void Update()
        {
            State.Characters.ForEach(ch => ch.Update());
            State.ColissionDetector.Update();
        }

        public static void GameOver()
        {

        }
    }
}