using System;
using System.Drawing;

namespace Pacman
{
    public class Ghost : Character
    {
        public delegate Point MovementDelegate(PointF location);

        public MovementDelegate _movementDelegate;

        Point _destination;

        public void ChangeMode(MovementDelegate movementDelegate, Bitmap sprite)
        {
            SetSprite(sprite);
            _movementDelegate = movementDelegate; 
        }

        public Ghost(Bitmap image, PointF location, float speed, MovementDelegate mode)
        {
            Home     = new PointF(13.5f, 14);
            Sprite   = new Sprite(image);
            Location = location;
            Speed    = speed;
            _movementDelegate = mode;
            _destination = _movementDelegate(Location);
        }

        public override void Update()
        {
            int dx = !Location.IsOnX(_destination) ? ((Location.X < _destination.X) ? 1 : -1) : 0;
            int dy = !Location.IsOnY(_destination) ? ((Location.Y < _destination.Y) ? 1 : -1) : 0;

            if (Location.IsOn(_destination))
                _destination = _movementDelegate(Location);

            Move(dx, dy);
        }

        //Point a;
        //bool flag = true;

        //public override void Update()
        //{
        //    int dx = 0;
        //    int dy = 0;
        //    float speed = Speed;

        //    float LX = Location.Y;
        //    float LY = Location.X;

        //    if (flag == true)
        //    {
        //        a = _movementDelegate(Location);
        //        flag = false;
        //    }

        //    if (Math.Abs(LY - a.Y) > 0.06)
        //    {
        //        if (LY < a.Y) dx = 1;
        //        if (LY > a.Y) dx = -1;
        //    }

        //    if (Math.Abs(LX - a.X) > 0.06)
        //    {
        //        if (LX < a.X) dy = 1;
        //        if (LX > a.X) dy = -1;
        //    }

        //    if ((Math.Abs(LX - a.X) < 0.08) && (Math.Abs(LY - a.Y) < 0.08))
        //        a = _movementDelegate(Location);

        //    Move(dx, dy);
        //}

    }
}