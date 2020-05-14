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
        readonly Game _stickHeroGame = new Game();
        private readonly int _squareSize = 20;
        private readonly int _squareCount = 40;
        private readonly int _heroSize = 3;
        private int _currentStickSize = 0;
        private int _currentStickAngle = 180;

        Platform _nextPlatform = new Platform(new Point(0, 0), new Size(0, 0));
        private Platform _startPlatform;
        private readonly Hero hero;

        Point _stickStartPoint = new Point();
        private Rectangle _stickRectangle = new Rectangle(0, 0, 5, 0);


        public Form1()
        {
            InitializeComponent();
            SetWindowSize();
            StickTimer.Interval = 1;
            StickDropTimer.Interval = 1;
            Image heroModel = _stickHeroGame.GetHeroModel(_heroSize * _squareSize);
            hero = new Hero(heroModel);
        }

        void SetWindowSize()
        {
            StickHeroField.Location = new Point(0, 0);
            StickHeroField.Size = new Size(_squareSize * _squareCount, _squareSize * _squareCount);
            this.Size = new Size(_squareSize * _squareCount + 100, _squareSize * _squareCount + 100);
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
            Size newSize = new Size(_squareSize * _stickHeroGame.PlatformSize, _squareSize * _stickHeroGame.PlatformSize);
            Point location = new Point(0, (_squareCount * _squareSize) - _squareSize * _stickHeroGame.PlatformSize - 1);

            _startPlatform = new Platform(location, newSize);
            _startPlatform.Draw(e);
        }

        int GetDestination(int start, int finish)
        {
            return finish - start;
        }

        void DrawHeroOnPlatform(PaintEventArgs e)
        {
            Point location = new Point(_squareSize * _stickHeroGame.PlatformSize - hero.HeroModel.Width,
                (_squareCount * _squareSize) - _squareSize * _stickHeroGame.PlatformSize - hero.HeroModel.Height - 1);
            hero.Draw(e, location);
        }

        void DrawNextPlatform(PaintEventArgs e)
        {
            if (!_nextPlatform.IsExist())
            {
                Random random = new Random();
                int randomX = random.Next(_squareSize * _stickHeroGame.PlatformSize + 2, _squareSize * _squareCount);
                int randomPlatformSize = random.Next(1, 5);

                Point newPosition = new Point(randomX, (_squareCount * _squareSize) - _squareSize * _stickHeroGame.PlatformSize - 1);
                Size newSize = new Size(_squareSize * randomPlatformSize, _squareSize * _stickHeroGame.PlatformSize);

                _nextPlatform.SetNewData(newPosition, newSize);
                _nextPlatform.Draw(e);
            }
            else
            {
                _nextPlatform.Draw(e);
            }
        }

        void SetStickStartPoint(PaintEventArgs e)
        {
            _stickStartPoint = new Point(_squareSize * _stickHeroGame.PlatformSize + 1,
                (_squareCount * _squareSize) - _squareSize * _stickHeroGame.PlatformSize - 1);
            _stickRectangle.Location = _stickStartPoint;
        }

        void DrawStick(PaintEventArgs e)
        {
            _stickRectangle.Size = new Size(5, _currentStickSize);

            PointF[] stickPoints = RotateStick(_stickRectangle, 180);

            e.Graphics.DrawPolygon(Pens.Red, stickPoints);
        }

        private void StickHeroField_Paint(object sender, PaintEventArgs e)
        {
            SetBackgroundColor(e);
            SetStickStartPoint(e);
            DrawStartPosition(e);
            DrawHeroOnPlatform(e);
            if (StickTimer.Enabled)
                DrawStick(e);
            DrawNextPlatform(e);
        }

        PointF[] RotateStick(Rectangle stick, int angle)
        {
            Matrix M = new Matrix();

            // create an array of all corner points:
            var p = new PointF[] {
                stick.Location,
                new PointF(stick.Right, stick.Top),
                new PointF(stick.Right, stick.Bottom),
                new PointF(stick.Left, stick.Bottom) };


            M.RotateAt(angle, new PointF(stick.X + 2, stick.Y));
            M.TransformPoints(p);

            return p;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    StickTimer.Start();
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    int destinationToNextPlatform;
                    StickTimer.Stop();
                    StickHeroField.Refresh();
                    StickDropTimer.Start();
                    destinationToNextPlatform = GetDestination(_stickStartPoint.X, _nextPlatform.PositionPoint.X);
                    _stickHeroGame.CheckStickSize(_currentStickSize, destinationToNextPlatform, _nextPlatform.GetWidth());

                    break;
            }
        }

        private void StickTimer_Tick(object sender, EventArgs e)
        {
            _currentStickSize += 1;
            StickHeroField.Refresh();
        }

        private void StickDropTimer_Tick(object sender, EventArgs e)
        {
            _stickRectangle.Size = new Size(5, _currentStickSize);

            _currentStickAngle++;
            if (_currentStickAngle < 270)
            {
                PointF[] stickPoints = RotateStick(_stickRectangle, _currentStickAngle);

                using (Graphics g = StickHeroField.CreateGraphics())
                    g.DrawPolygon(Pens.Red, stickPoints);
                StickHeroField.Refresh();
            }
            else
            {
                PointF[] stickPoints = RotateStick(_stickRectangle, _currentStickAngle);

                using (Graphics g = StickHeroField.CreateGraphics())
                    g.DrawPolygon(Pens.Red, stickPoints);

                _currentStickAngle = 180;
                StickDropTimer.Stop();
            }
        }
    }
}
