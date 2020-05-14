﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
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
        private int stickStartPointX = 0;
        Point stickStartPoint = new Point();

        int randomX;
        int randomPlatformSize;

        public Form1()
        {
            InitializeComponent();
            SetWindowSize();
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
        }

        void DrawStick(PaintEventArgs e)
        {
            Rectangle stickRectangle = new Rectangle();
            stickRectangle.Location = stickStartPoint;
            stickRectangle.Size = new Size(5,currentStickSize*squareSize);
            e.Graphics.DrawRectangle(Pens.Red, stickRectangle);
        }

        private void StickHeroField_Paint(object sender, PaintEventArgs e)
        {
            SetBackgroundColor(e);
            SetStickStartPoint(e);
            DrawStartPosition(e);
            DrawHeroOnPlatform(e);
            DrawStick(e);
            DrawNextPlatform(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Space:
            //        StickTimer.Start();
            //        break;
            //}
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Space:
            //        StickTimer.Stop();
            //        destinationToNextPlatform = GetDestination(stickStartPointX, randomX);
            //        stickHeroGame.CheckStickSize(currentStickSize,destinationToNextPlatform);
                    
            //        break;
            //}
        }

        private void StickTimer_Tick(object sender, EventArgs e)
        {
            currentStickSize += squareSize;
            StickHeroField.Refresh();
        }
    }
}