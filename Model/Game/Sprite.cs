using System.Drawing;

namespace Pacman
{
    public class Sprite
    {
        Size _frameSize = new Size(64, 64);
        float _currentFrame;
        int _row;
        public Image _image;

        public void ChangeImage(Image image)
            => _image = image;

        public void Draw(Graphics gr, System.Drawing.Point location)
        {
            int framesPerRow = _image.Width / _frameSize.Width;
            int x = (int)_currentFrame % framesPerRow;
            int y = _row >= 0 ? _row :  (int)_currentFrame / framesPerRow;

            Size sizeOfSprite = new Size(Tile.Size.Height + 15, Tile.Size.Width + 15);
            var locOfDestRect = new System.Drawing.Point(x * _frameSize.Width + 3,
                                                         y * _frameSize.Height + 3);
            gr.DrawImage(_image, new Rectangle(location, sizeOfSprite),
                                 new Rectangle(locOfDestRect, _frameSize),
                                 GraphicsUnit.Pixel);
        }

        public void ChoosePicByDir(int dx, int dy, float speed)
        {
            if (dy > 0) 
                _row = 0;
            else if (dy < 0)
                _row = 3;
            else if (dx > 0)
                _row = 2;
            else if (dx < 0)
                _row = 1;
            else
            {
                _row = (_row == 3) ? 3 : 0;
                _currentFrame = 0;
            }

            if (dx != 0 || dy != 0)
                _currentFrame += speed * 3;
        }
    }
}