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
    /// <summary>
    ///  Deze class regeld de schermen 
    ///  Deze classe is de super van de andere schermen
    /// </summary>
   class GameScreen
   {
       #region variables

        protected ContentManager content;
        protected List<List<string>> attributes, contents;
        protected InputManager inputManager;
        protected Texture2D background;
        protected Rectangle frame;

       #endregion


        #region methods/constructor
        #region Constructor/ Load
        public virtual void LoadContent(ContentManager Content, InputManager inputManager) 
        {
            this.inputManager = inputManager;
            content = new ContentManager(Content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            frame = new Rectangle(0, 0, 960, 640);
            
        }
        #endregion

        #region Unload
        public virtual void UnloadContent()
        {
            content.Unload();
            inputManager = null;
            attributes.Clear();
            contents.Clear();
        }
        #endregion
        #region Update
        public virtual void Update(GameTime gameTIme) 
        {
            //todo
        }
        #endregion

        #region Draw
        public virtual void Draw(SpriteBatch spriteBatch) 
        { 
           //todo
        }

        #endregion

        #endregion
   }
}
