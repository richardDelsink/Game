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
    class Level1
    {
        // het laden van de levelmap
        #region variables

        private Layer layer;// laden van de layer
        private string id; // laden van tekst bestand met bepaald ID

        #endregion

        #region properties

        public string Id { get { return id; } }
        public Layer Layer {   get {  return layer;   }  }

        #endregion

        #region construct/ methods
        #region Constructor/Load
        public void LoadContent(ContentManager content, Level1 map ,string levelID)
        {
            layer = new Layer();
            id = levelID;
            layer.LoadContent( map, "Layer1");
           
        }
        #endregion

        #region Unload
        public void UnloadContent()
        {
            // op dit moment niet echt nodig
            
        }
        #endregion

        #region Update
        // layer updaten
        public void Update(GameTime gameTime)
        {
           layer.Update(gameTime);
        }

        #endregion

        #region Collision update
        // als er een collision is met eneity update
        public void UpdateCollision(ref Entity e)
        {
            layer.UpdateCollision(ref e);
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            layer.Draw(spriteBatch);
        }
        #endregion
        #endregion
    }
}
