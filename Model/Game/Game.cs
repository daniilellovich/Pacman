using System.Collections.Generic;

namespace Pacman
{
    public class Game
    {
        public Mediator State;
        public int Score { get; set; }
        public int LevelNumber { get; set; } = 1;
        public int HighScore { get; private set; }

        public Game(int levelNumber)
        {
            LevelNumber = levelNumber;
            Level level = new Level();
            State = new Mediator(this, level);
            State.Level.PutPacmanLivesDownside(State.Pacman.GetLives());
        }

        public void Update()
        {
            State.Characters.ForEach(ch => ch.Update());
            State.ColissionDetector.Update();
        }

        public void OneMoreTry()
        {
            State = new Mediator(this, State.Level);
        }

        public void IsOver()
        {
            //сделать сохранение в таблицу рекордов
        }
    }
}