using System;
using System.Drawing;

namespace Pacman
{
    public class Ghost : Character
    {
        public delegate Point MovementDelegate(PointF location);

        public MovementDelegate _movementDelegate;

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
            Destination = _movementDelegate(Location);
        }

        public override void Update()
        {
            int dx = Location.IsOnX(Destination) ? ((Location.X < Destination.X) ? 1 : -1) : 0;
            int dy = Location.IsOnY(Destination) ? ((Location.Y < Destination.Y) ? 1 : -1) : 0;

            if (Location.IsOn(Destination))
               Destination = _movementDelegate(Location);

            Move(dx, dy);         
        }
    }
}