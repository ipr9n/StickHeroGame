using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void CheckStickSize(int currentSize,int destinationMin)
        {

        }
    }
}
