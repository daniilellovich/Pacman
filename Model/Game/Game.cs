using System.Collections.Generic;

namespace Pacman
{
    public class Game
    {
        public Mediator State;
        public static int levelNum = 1;
        public static int highScore;    //сделать сохранение
        public int score;

        public void Init()
        {
            State = new Mediator();
            Controls.LevelPanel.SetState(State);
        }

        public void Update()
        {
            State.ItemsController.Update();
            State.Pacman.Update();

            State.Blinky.Update();
            State.Pinky.Update();
            State.Inky.Update();
            State.Clyde.Update();
        }

        public void GameOver()
        {

        }
    }
}