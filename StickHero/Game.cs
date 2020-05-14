using System.Drawing;
using System.Windows.Forms;

namespace StickHeroGame.StickHero
{
    class Game
    {
        private readonly string _pathToModel = "Hero.png";
        public int PlatformSize {get;}

        public Game()
        {
            PlatformSize = 5;
        }

        public Image GetHeroModel(int size)
        {
            Image image = Image.FromFile(_pathToModel);
            Bitmap myImageBitmap = new Bitmap(image,size,size);

            return myImageBitmap;
        }

        public void CheckStickSize(int currentSize,int destinationMin,int platformWidth)
        {
            if(currentSize<destinationMin)
                MessageBox.Show($"Не докинул");

            if (currentSize > destinationMin + platformWidth)
                MessageBox.Show("Перекинул");
        }
    }
}
