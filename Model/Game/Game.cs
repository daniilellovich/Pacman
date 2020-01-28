namespace Pacman
{
    public class Game
    {
        public Mediator State;
        public static int level = 1;
        public static int highScore;
        public int score;

        public Game()
            => State = new Mediator();

        public void Update()
        {
            State.Pacman.Update();

            State.Blinky.Update();
            State.Pinky.Update();
            State.Inky.Update();
            State.Clyde.Update();

            State.ItemsController.Update();
        }

        public void GameOver()
        {

        }
    }
}