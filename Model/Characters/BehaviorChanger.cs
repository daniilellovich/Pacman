namespace Pacman
{
    public class BehaviorChanger
    {
        enum ghostMode  {Chase, Scatter, Frightened }
        int[] events = new int[7];
        ghostMode _currentMode = ghostMode.Frightened;

        public int _currentTime = 0;
        public int _frightenedTime = 0;
        public bool _isFrightened;

        void SetGhotstsSpeed(float speed)
        {
            Game.State.Blinky.SetSpeed(speed);
            Game.State.Pinky.SetSpeed(speed);
            Game.State.Inky.SetSpeed(speed);
            Game.State.Clyde.SetSpeed(speed);
        }

        public BehaviorChanger(int level)
        {
            switch (level)
            {
                case 1:
                    events = new int[7] { 7, 27, 34, 54, 59, 79, 84 };
                    Game.State.Pacman.SetSpeed(0.1f);
                    SetGhotstsSpeed(0.12f);
                    break;
                case 2:
                    events = new int[7] { 7, 27, 34, 54, 59, 1092, 1093 };
                    Game.State.Pacman.SetSpeed(0.11f);
                    SetGhotstsSpeed(0.13f);
                    break;
                case 5:
                    events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    Game.State.Pacman.SetSpeed(0.12f);
                    SetGhotstsSpeed(0.14f);
                    break;
                case 21:
                    events = new int[7] { 5, 25, 30, 50, 55, 1092, 1093 };
                    Game.State.Pacman.SetSpeed(0.11f);
                    SetGhotstsSpeed(0.13f);
                    break;
            }
        }

        public void GhostAreFrightened()
        {
            _frightenedTime = 0;
            _isFrightened = true;
            _currentMode = ghostMode.Frightened;
            #region
            if (Game.State.Blinky._isEaten == false)
                Game.State.Blinky.ChangeMode(Game.State.Behaviors.BlinkyFrightenedMode, GameResources.Fright);
            if (Game.State.Pinky._isEaten == false)
                Game.State.Pinky.ChangeMode(Game.State.Behaviors.PinkyFrightenedMode, GameResources.Fright);
            if (Game.State.Inky._isEaten == false)
                Game.State.Inky.ChangeMode(Game.State.Behaviors.InkyFrightenedMode, GameResources.Fright);
            if (Game.State.Clyde._isEaten == false)
                Game.State.Clyde.ChangeMode(Game.State.Behaviors.ClydeFrightenedMode, GameResources.Fright);
            #endregion          
        }

        public void BehaviorsController()
        {
            if (_isFrightened)
            {
                _frightenedTime++;

                if (_frightenedTime == 3)
                {   
                    if (Game.State.Blinky._isEaten == false)
                        Game.State.Blinky.SetSprite(GameResources.FrightEnd);
                    if (Game.State.Pinky._isEaten == false)
                        Game.State.Pinky.SetSprite(GameResources.FrightEnd);
                    if (Game.State.Inky._isEaten == false)
                        Game.State.Inky.SetSprite(GameResources.FrightEnd);
                    if (Game.State.Clyde._isEaten == false)
                        Game.State.Clyde.SetSprite(GameResources.FrightEnd);
                }
                if (_frightenedTime == 5)
                {
                    //Game.State.Blinky.isEaten = true;
                    //Game.State.Pinky.isEaten = true;
                    //Game.State.Inky.isEaten = true;
                    //Game.State.Clyde.isEaten = true;
                    _isFrightened = false;
                    _frightenedTime = 0;
                }
            }
            
            if (!(_isFrightened))
            {               
                if (((_currentTime > events[0]) && (_currentTime < events[1])) ||
                    ((_currentTime > events[2]) && (_currentTime < events[3])) ||
                    ((_currentTime > events[4]) && (_currentTime < events[5])) ||
                     (_currentTime > events[6]))
                {
                    _currentMode = ghostMode.Chase;
                    #region
                    if (Game.State.Blinky._isEaten == false)
                        Game.State.Blinky.ChangeMode(Game.State.Behaviors.BlinkyChaseMode, GameResources.Blinky);
                    if (Game.State.Pinky._isEaten == false)
                        Game.State.Pinky.ChangeMode(Game.State.Behaviors.PinkyChaseMode, GameResources.Pinky);
                    if (Game.State.Inky._isEaten == false)
                        Game.State.Inky.ChangeMode(Game.State.Behaviors.InkyChaseMode, GameResources.Inky);
                    if (Game.State.Clyde._isEaten == false)
                        Game.State.Clyde.ChangeMode(Game.State.Behaviors.ClydeChaseMode ,GameResources.Clyde);
                    #endregion
                }
                
                if ((_currentTime < events[0]) ||
                   ((_currentTime > events[1]) && (_currentTime < events[2])) ||
                   ((_currentTime > events[3]) && (_currentTime < events[4])) ||
                   ((_currentTime > events[5]) && (_currentTime < events[6])))
                {
                    _currentMode = ghostMode.Scatter;
                    #region
                    if (Game.State.Blinky._isEaten == false)
                        Game.State.Blinky.ChangeMode(Game.State.Behaviors.BlinkyScatterMode, GameResources.Blinky);               
                    if (Game.State.Pinky._isEaten == false)
                        Game.State.Pinky.ChangeMode(Game.State.Behaviors.PinkyScatterMode, GameResources.Pinky);
                    if (Game.State.Inky._isEaten == false)
                        Game.State.Inky.ChangeMode(Game.State.Behaviors.InkyScatterMode, GameResources.Inky);
                    if (Game.State.Clyde._isEaten == false)
                        Game.State.Clyde.ChangeMode(Game.State.Behaviors.ClydeScatterMode, GameResources.Clyde);
                    #endregion                
                }

                _currentTime++;
                if (_currentTime == 4)
                    StartForm.mf.readyLabelP.Hide();
            }
        }
    }
}

//более эффективная проверка
//if ((currentTime == events[0]) || (currentTime == events[2]) || (currentTime == events[4]) || (currentTime == events[6]))
//{
//    currentMode = "Chase Mode:";
//    localTime = 0;
//    //   Game.State.Behavior.Nulling();
//    #region
//    Game.State.BlinkyController.movementDelegate = Game.State.Behavior.BlinkyChaseMode;
//    Game.State.PinkyController.movementDelegate = Game.State.Behavior.PinkyChaseMode;
//    Game.State.InkyController.movementDelegate =  Game.State.Behavior.InkyChaseMode;
//    Game.State.ClydeController.movementDelegate = Game.State.Behavior.ClydeChaseMode;
//    #endregion
//}

//if ((currentTime == events[1]) || (currentTime == events[3]) || (currentTime == events[5]))
//{
////    Game.State.Behavior.Nulling();
//    currentMode = "Scatter Mode:";
//    localTime = 0;
//    #region
//    Game.State.BlinkyController.movementDelegate = Game.State.Behavior.BlinkyScatterMode;
//    Game.State.PinkyController.movementDelegate = Game.State.Behavior.PinkyScatterMode;
//    Game.State.InkyController.movementDelegate = Game.State.Behavior.InkyScatterMode;
//    Game.State.ClydeController.movementDelegate = Game.State.Behavior.ClydeScatterMode;
//    #endregion
//}