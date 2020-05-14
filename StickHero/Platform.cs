using System.Drawing;
using System.Windows.Forms;

namespace StickHeroGame.StickHero
{
    class Platform
    {
        public Point PositionPoint;
        private Size _sizePoint;
        private bool _isExist = false;

        public Platform(Point position, Size size)
        {
            PositionPoint = position;
            _sizePoint = size;
        }

       public int GetWidth()
        {
            return _sizePoint.Width;
        }

        public void Draw(PaintEventArgs e)
        {
            Rectangle startPlatform = new Rectangle();
            startPlatform.Size = _sizePoint;
            startPlatform.Location = PositionPoint;
            e.Graphics.DrawRectangle(Pens.Black, startPlatform);
            _isExist = true;
        }

        public void Reset()
        {
            _isExist = false;
        }

        public void SetNewData(Point position, Size size)
        {
            PositionPoint = position;
            _sizePoint = size;
        }

        public bool IsExist()
        {
            return _isExist;
        }
    }
}
