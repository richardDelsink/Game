using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyGame
{
    class SSAnimation : Animation
    {
        // deze classe zorgd voor de sprite aniamtie 
        // deze laat een heel plaatje in en knipt het af

        #region Variables

        private int frameCounter, switchFrame; // welke frame en weke switch frame, switchframe = index
        private Vector2 frames, currentFrame; // frames is hoeveel bij de animatie hoort, current frame is frame waar je nu bent

        #endregion 

        #region Properties

        public Vector2 Frames { set { frames = value; }}
        public Vector2 CurrentFrame { get { return currentFrame; } set { currentFrame = value; }}

        public int FrameWidth { get { return image.Width / (int)frames.X; } }
        public int FrameHeight { get { return image.Height / (int)frames.Y; } }


        #endregion

        #region constructor / methods

        #region Constructor/load
        public override void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            base.LoadContent(Content, image, text, position);
            frameCounter = 0;
            switchFrame = 100;
            frames = new Vector2(3, 4); // frames bv je hebt een image van 64 bij 32 en je crop is 32 pixels dan heb je 2,1 
            currentFrame = new Vector2(0, 0);
            rect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight, FrameWidth, FrameHeight);
        }

        #endregion

        #region Unload
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        { // als het actief is dan willen we dat het gaat switchen -- aniamtie laten komen
            // deze is zo groot en als het dat bereikt heeft begint het weer op nieuw dus een loop
            // tijd geven we weer in secondes
            if (isActive)
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (frameCounter >= switchFrame)
                {
                    frameCounter = 0;
                    currentFrame.X++;

                    if (currentFrame.X * FrameWidth >= image.Width)
                    {
                        currentFrame.X = 0;
                    }
                }
            }
            else
            {
                frameCounter = 0;
                currentFrame.X = 0;
            }
            rect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight, FrameWidth, FrameHeight);
        }
        #endregion

        #endregion
    }
}

