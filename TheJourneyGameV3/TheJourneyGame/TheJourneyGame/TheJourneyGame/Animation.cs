using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyGame
{   /// Deze class zorgt voor de algemene animatie van alle objecten waar animatie voor nodig is. 
    /// BV de animatie van de splash screen 
    class Animation
    {
        #region variables

        /// <summary>
        /// Ik heb hiervoor voor protected gekozen omdat deze de super classe is 
        /// </summary>
        protected string text;
        protected bool isActive; // kijken of het actief is
        protected float alpha, rotation, scale; // waardes aangeven aan bepaalde aniamties

        protected SpriteFont font;
        protected Color color;
        protected Vector2 origin, position;
        protected ContentManager content;
        protected Texture2D image; // image
        protected Rectangle rect; // nieuwe rect
       

        #endregion

        #region properties

        // properties
        // de virtual wordt veranderd in de fade animation vandaar virtual
        public virtual float Alpha { get { return alpha; } set { alpha = 0.0f; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public float Scale { set { scale = value; } }

        public SpriteFont Font { get { return font; } set { font = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        
        #endregion

        #region methods
        // virtual omdat een andere class er iets van anders kan maken
        // de loadcontent is ook een soort constructer in XNA

        #region LoadContent/Constructor

        public virtual void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            this.image = image;
            this.text = text;
            this.position = position;
            //als text leeg is wordt het ingevuld
            if (text != String.Empty)
            {
                font = this.content.Load<SpriteFont>("SpriteFont1");
                color = new Color(255, 255, 255);
            }
            if (image != null)
            {
                rect = new Rectangle(0, 0, image.Width, image.Height);
            }
            rotation = 0.0f;
            
            scale = alpha = 1.0f;
            isActive = false;

        }

        #endregion

        #region Unload
        // hier wordt het content weggehaald van het scherm en alles wordt weer op de goede positie gezet
        public virtual void UnloadContent()
        {
            content.Unload();
            text = String.Empty;
            position = Vector2.Zero;
            rect = Rectangle.Empty;
            image = null;
        
        }

        #endregion

        #region Update

        public virtual void Update(GameTime gameTime)
        {
            // hier zit nog niks in wat van toepassing is 
        }

        #endregion

        #region Draw
        // setting center origin
        public virtual void Draw(SpriteBatch spritebatch)
        {
            // hierbij kijk je naar de center van een image 
            if (image != null)
            {
                origin = new Vector2(rect.Width / 2, rect.Height / 2);
                spritebatch.Draw(image, position + origin, rect, Color.White * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }

            // en als het een text is word hiervan de center bepaald en getekent
            if (text != String.Empty)
            {
                origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
                spritebatch.DrawString(font, text, position + origin, color * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }
        }

        #endregion

        #endregion
    }

}
