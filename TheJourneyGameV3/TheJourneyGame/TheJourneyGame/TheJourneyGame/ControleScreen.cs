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
    class ControleScreen : GameScreen
    {
        /// <summary>
        /// Dit is een echte scherm waar getekent wordt, deze hebruikt dan ook dezelfde methodes als game1 die wij bijna niet gebruiken
        /// </summary>
      

        #region Variables

        // deze heeft geen eigen variables omdat het alleen background inlaad en deze met een optie van input weer terug kan gaan
        #endregion

        #region constructor/Loadcontent


        // hierin laden de alles wat in de contrle scherm moet komen
        // zoals je ziet is deze classe een child van GameScreen
        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {

            base.LoadContent(Content, inputManager);        
            background = Content.Load<Texture2D>("Content/Background/controleScreen");
            
            
        }

        #endregion

        #region methods

        #region Unload
        
        public override void UnloadContent()
        {
            base.UnloadContent();
            background = null;
        }

        #endregion

        #region Update

        // we hebben een inputmanager deze wordt steeds geupdate
        // als je op deze scherm op backspace drukt dan gaat het terug naar het menuscherm
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

        // hier worde alles getekent
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, frame, Color.White);

        }

        #endregion 

        #endregion
    }
}
