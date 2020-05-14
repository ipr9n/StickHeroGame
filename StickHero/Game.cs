using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StickHeroGame.StickHero
{
    class Game
    {
        private string pathToModel = "Hero.png";

        public Image GetHeroModel(int size)
        {
            Image image = Image.FromFile(pathToModel);
            Bitmap myImageBitmap = new Bitmap(image,size,size);

            return myImageBitmap;
        }

        public void CheckStickSize(int currentSize,int destinationMin,int platformHeight)
        {
            if(currentSize<destinationMin)
                MessageBox.Show($"Не докинул");
            if (currentSize > destinationMin + platformHeight)
                MessageBox.Show("Перекинул");
        }
    }
}
