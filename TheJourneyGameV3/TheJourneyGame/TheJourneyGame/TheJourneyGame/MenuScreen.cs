using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TheJourneyGame
{/// het menuscherm
    class MenuScreen : GameScreen
    {
        #region data

        private SpriteFont font; // inladen van font en menumanager
        private MenuManager menu;
       
        #endregion

        #region constructor


        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {

            base.LoadContent(Content, inputManager);
            // als fond leeg is laat new font in
            if (font == null)
            {
                font = this.content.Load<SpriteFont>("Menu");
            }
            menu = new MenuManager();
            background = Content.Load<Texture2D>("Content/Background/menuScreen");
            menu.LoadContent(content, "Title");
              
        }

        #endregion

        #region methods

        #region Unload

        public override void UnloadContent()
        {
            base.UnloadContent();         
            menu.UnloadContent();
        }

        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
             menu.Update(gameTime, inputManager);
        }

        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {  
            spriteBatch.Draw(background, frame ,Color.White);
            menu.Draw(spriteBatch);
        }

        #endregion

        #endregion
    }
}
