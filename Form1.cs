using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using StickHeroGame.StickHero;

namespace StickHeroGame
{
    public partial class Form1 : Form
    {
        private Image heroModel;
        Game stickHeroGame = new Game();
        private int squareSize = 20;
        private int squareCount = 40;
        private int platformSize = 5;
        private int heroSize = 3;
        private int startStickSize = 0;
        private int currentStickSize = 0;
        private int destinationToNextPlatform = 0;
        private int currentStickAngle = 180;
        private int stickStartPointX = 0;
        private bool stickDropped = false;
        Point stickStartPoint = new Point();
        private Rectangle stickRectangle = new Rectangle(0,0,5,0);

        int randomX;
        int randomPlatformSize;

        delegate void eventDelegate(PaintEventArgs eventArgs);
        private event eventDelegate stickEvent;

        public Form1()
        {
            InitializeComponent();
            SetWindowSize();
            StickTimer.Interval = 1;
            heroModel = stickHeroGame.GetHeroModel(heroSize * squareSize);
        }

        void SetWindowSize()
        {
            StickHeroField.Location = new Point(0, 0);
            StickHeroField.Size = new Size(squareSize*squareCount, squareSize*squareCount);
            this.Size = new Size(squareSize*squareCount+100, squareSize*squareCount+100);
        }

        void SetBackgroundColor(PaintEventArgs e)
        {
            Rectangle r = new Rectangle(StickHeroField.Location.X, StickHeroField.Location.Y, StickHeroField.Width, StickHeroField.Height);
            LinearGradientBrush br =
                new LinearGradientBrush(r, Color.Blue, Color.Aqua, 90, true);
            e.Graphics.FillRectangle(br, r);
        }

        void DrawStartPosition(PaintEventArgs e)
        {
            Rectangle startPlatform = new Rectangle();
            startPlatform.Size = new Size(squareSize*platformSize,squareSize*platformSize);
            startPlatform.Location = new Point(0,(squareCount*squareSize)-squareSize*platformSize-1);
            e.Graphics.DrawRectangle(Pens.Black,startPlatform);
        }

        int GetDestination(int start, int finish)
        {
            return finish-start;
        }

        void DrawHeroOnPlatform(PaintEventArgs e)
        {
            e.Graphics.DrawImage(heroModel, new Point(squareSize * platformSize-heroModel.Width, (squareCount * squareSize) - squareSize * platformSize - heroModel.Height-1));
        }

        void DrawNextPlatform(PaintEventArgs e)
        {
            Random random = new Random();
            int randomX = random.Next(squareSize * platformSize + 2, squareSize * squareCount);
                int randomPlatformSize = random.Next(1, 5);

                Rectangle nextPlatform = new Rectangle();
            nextPlatform.Size = new Size(squareSize * randomPlatformSize, squareSize * platformSize);
            nextPlatform.Location = new Point(randomX, (squareCount * squareSize) - squareSize * platformSize - 1);
            e.Graphics.DrawRectangle(Pens.Black, nextPlatform);
        }

        void SetStickStartPoint(PaintEventArgs e)
        {
            stickStartPoint = new Point(squareSize * platformSize + 1,
                (squareCount * squareSize) - squareSize * platformSize - 1);
            stickRectangle.Location = stickStartPoint;
        }

        void DrawStick(PaintEventArgs e)
        {
            stickRectangle.Size = new Size(5, currentStickSize);

            PointF[] stickPoints = RotateStick(stickRectangle, 180);

                e.Graphics.DrawPolygon(Pens.Red, stickPoints);
        }

        private void StickHeroField_Paint(object sender, PaintEventArgs e)
        {
            SetBackgroundColor(e);
            SetStickStartPoint(e);
            DrawStartPosition(e);
            DrawHeroOnPlatform(e);
            if(StickTimer.Enabled)
                DrawStick(e);
            //else
            //{
            //    StickDrop(e);
            //}
            // DrawNextPlatform(e);
        }

        void StickDrop(PaintEventArgs e)
        {
           // stickDropped = true;
           stickRectangle.Size = new Size(5, currentStickSize);

                    PointF[] stickPoints = RotateStick(stickRectangle, 270);

                e.Graphics.DrawPolygon(Pens.Red, stickPoints);
                StickHeroField.Refresh();



        }

        PointF[] RotateStick(Rectangle stick,int angle)
        {
            //currentStickSize += squareSize; 
            Matrix M = new Matrix();

            // create an array of all corner points:
            var p = new PointF[] {
                stick.Location,
                new PointF(stick.Right, stick.Top),
                new PointF(stick.Right, stick.Bottom),
                new PointF(stick.Left, stick.Bottom) };


            M.RotateAt(angle, new PointF(stick.X+2, stick.Y));
            M.TransformPoints(p);

            return p;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    stickEvent += DrawStick;
                        StickTimer.Start();
                   

                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    StickTimer.Stop();
                    StickHeroField.Refresh();
                    StickDropTimer.Interval = 1;
                    StickDropTimer.Start();
                    destinationToNextPlatform = GetDestination(stickStartPointX, randomX);
                    stickHeroGame.CheckStickSize(currentStickSize, destinationToNextPlatform);

                    break;
            }
        }

        private void StickTimer_Tick(object sender, EventArgs e)
        {
            currentStickSize += 1;
            StickHeroField.Refresh();
        }

        private void StickDropTimer_Tick(object sender, EventArgs e)
        {
            stickRectangle.Size = new Size(5, currentStickSize);

            currentStickAngle++;
            if (currentStickAngle < 270)
            {
                PointF[] stickPoints = RotateStick(stickRectangle, currentStickAngle);

                using (Graphics g = StickHeroField.CreateGraphics())
                    g.DrawPolygon(Pens.Red, stickPoints);
                StickHeroField.Refresh();
            }
            else
            {
                PointF[] stickPoints = RotateStick(stickRectangle, currentStickAngle);

                using (Graphics g = StickHeroField.CreateGraphics())
                    g.DrawPolygon(Pens.Red, stickPoints);

                currentStickAngle = 180;
                StickDropTimer.Stop();
            }
        }
    }
}
