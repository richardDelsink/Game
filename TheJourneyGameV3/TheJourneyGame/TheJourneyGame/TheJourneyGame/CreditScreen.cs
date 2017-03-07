using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyGame
{
    class CreditScreen : GameScreen
    {

        // het tekene van een credit scherm

        #region data

        #endregion

        #region constructor
        // hierin laden de alles wat in de scherm moet komen
         public override void LoadContent(ContentManager Content, InputManager inputManager)
         {

            base.LoadContent(Content, inputManager);
            background = Content.Load<Texture2D>("Content/Background/creditScreen");
         
         }

        #endregion

        #region methods

        #region Unload
        // unloaden van content op dit scherm
         public override void UnloadContent()
        {
            base.UnloadContent();
            background = null; 
        }

         #endregion

         #region Update
        // updaten als er iets gebeurd
         public override void Update(GameTime gameTime)
         {
            inputManager.Update();

            if (inputManager.KeyPressed(Keys.Back))
            {

                Type newClass = Type.GetType("TheJourneyGame." + "MenuScreen");
                ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), inputManager);

            }
         }

        #endregion

         #region Draw
        // het tekenen
         public override void Draw(SpriteBatch spriteBatch)
         {

            spriteBatch.Draw(background, frame, Color.White);
         }

        #endregion

        #endregion
    }
}
