using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyGame
{ /// dit zorgd voor de fade animatie van de schermen. Hiervoor woordt een zwarte plaatje voor gebrauikt waarbij
  /// de transparantie wordt verkleined of vergroot
    class FadeAnimation : Animation
    {
       #region Variables

        // de variable voor  animaties van images en of text
       private bool increase, stopUpdating;
       private float fadeSpeed, activateValue, defaultAlpha;
       private TimeSpan defaultTime, timer;

       #endregion

       #region properties

       //properties speciaal voor de fade animation class
        // hierbij wordt increase op false of tru gezet
       public override float Alpha 
       { 
           get 
           { 
               return alpha; 
           } 
           set
           {
               if (alpha == 1.0f)
               {
                   increase = false;
               }
               else if ( alpha == 0.0f)
               {
                   increase = true;
               }

           } 
       }

       public float ActivatieValue { get { return activateValue; } set { activateValue = value; } }
       public float Fadespeed { get { return fadeSpeed; } set { fadeSpeed = value; } }
       public TimeSpan Timer { get { return timer; } set { defaultTime = value; timer = defaultTime; } }

       #endregion

       #region constructor & methods

       #region constructor/Loadcontent
       // het laden van de eigenschappen
        public override void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            base.LoadContent(Content, image, text, position);
            increase = false;
            fadeSpeed = 1.0f;
            defaultTime = new TimeSpan(0, 0, 1);
            timer = defaultTime;
            activateValue = 0.0f;
            stopUpdating = false;
            defaultAlpha = alpha;
        }
       #endregion

        #region Update
        //het updaten van de fade animation, is op did moment op seconden gezet
        // het veranderd de alpha waarde
        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                if (!stopUpdating)
                {
                    if (!increase)
                    {
                        alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (alpha <= 0.0f)
                    {
                        alpha = 0.0f;
                        increase = true;
                        
                    }

                    else if (alpha >= 1.0f)
                    {
                        alpha = 1.0f;
                        increase = false;
                        
                    }
                }

                // hier worden ze weer op de default waardes neergezet
                // het moet steeds kijken wat het aan het doen is
                // er is daarom ook geen else toegevoegd
                if (alpha == activateValue)
                {
                    stopUpdating = true;
                    timer -= gameTime.ElapsedGameTime;
                    if (timer.TotalSeconds <= 0)
                    {
                       
                        timer = defaultTime;
                        stopUpdating = false;
                    }
                }

                // hier wordt de waarde van de alpha op default gezet als de fade/animatie niet actief is
            }
            else
            {
                alpha = defaultAlpha;
                stopUpdating = false;
            }
        }

        #endregion

       #endregion
    }
}
