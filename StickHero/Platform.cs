using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StickHeroGame.StickHero
{
    class Platform
    {
        public Point positionPoint;
        private Size sizePoint;
        private bool isExist = false;

        public Platform(Point position, Size size)
        {
            positionPoint = position;
            sizePoint = size;
        }

       public int GetHeight()
        {
            return sizePoint.Width;
        }

        public void Draw(PaintEventArgs e)
        {
            Rectangle startPlatform = new Rectangle();
            startPlatform.Size = sizePoint;
            startPlatform.Location = positionPoint;
            e.Graphics.DrawRectangle(Pens.Black, startPlatform);
            isExist = true;



        }

        public void Reset()
        {
            isExist = false;
        }

        public void SetNewData(Point position, Size size)
        {
            positionPoint = position;
            sizePoint = size;
        }

        public bool IsExist()
        {
            return isExist;
        }
    }
}
