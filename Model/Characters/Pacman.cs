using System.Windows.Forms;

namespace Pacman
{
public class Pacman : Character
{
    public enum Directions { nowhere, up, right, down, left }
    public Directions CurrentDir { get; private set; }
    Directions _nextDirection;
    public int DotsEaten { get; set; }
    public int Lifes { get; private set; } = 3;

    //public Pacman(float speed)
    //{
    //    LocationF = new PointF(13.5f, 26);
    //    SetSprite(GameResources.Pacman);
    //    SetSpeed(speed);
    //}

    public Pacman(Mediator mediator) : base(mediator)
    {
        _locationF = new PointF(13.5f, 26);
        SetSprite(GameResources.Pacman);
        SetSpeed(0.1f);
    }

    public override void Eaten()
    {
        //SoundController.StopLongSound();
        //SoundController.PlaySound("PacmanEaten");
        //Lifes--;

        //switch (Lifes)
        //{
        //    case 0:
        //        Game.GameOver();
        //        break;
        //    case 1:
        //        _gameState.Level.Tiles[0, 34] = new Floor(new Point(4, 34));
        //        break;
        //    case 2:
        //        _gameState.Level.Tiles[2, 34] = new Floor(new Point(4, 34));
        //        break;
        //}
    }

    public override void Update()
    {
        Point destination = GetNextLocation(_nextDirection);
        (int dx, int dy) = CalcOffset(destination);
        Move(dx, dy);
    }

    bool DirIsValid(Directions dir)
        => _gameState.Level.IsWalkable(GetPointByDir(dir));

    Point GetNextLocation(Directions receivedDir)
    {
        CurrentDir = (DirIsValid(receivedDir)) ?
                     receivedDir : !DirIsValid(CurrentDir) ?
                     Directions.nowhere : CurrentDir;

        return GetPointByDir(CurrentDir);
    }

    public void UpdateDirFromKeyboard(Keys pressedKey)
    {
        if (pressedKey == Keys.Up || pressedKey == Keys.W)
            _nextDirection =  Directions.up;
        if (pressedKey == Keys.Right || pressedKey == Keys.D)
            _nextDirection = Directions.right;
        if (pressedKey == Keys.Down || pressedKey == Keys.S)
            _nextDirection = Directions.down;
        if (pressedKey == Keys.Left || pressedKey == Keys.A)
            _nextDirection = Directions.left;
    }

    Point GetPointByDir(Directions dir)
    {
        switch (dir)
        {
        case Directions.up: return GetLoc().Up;
        case Directions.right: return GetLoc().Right;
        case Directions.down: return GetLoc().Down;
        case Directions.left: return GetLoc().Left;
        default: return GetLoc();
        }
    }
}
}