using System.Drawing;
using System.Windows.Forms;

namespace StickHeroGame.StickHero
{
    class Hero
    {
       public Image HeroModel { get; }

        public Hero(Image heroModel)
        {
            HeroModel = heroModel;
        }

        public void Draw(PaintEventArgs e, Point location)
        {
            e.Graphics.DrawImage(HeroModel, location);
        }
    }
}
