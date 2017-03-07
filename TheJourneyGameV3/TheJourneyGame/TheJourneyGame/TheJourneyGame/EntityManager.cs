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
    class EntityManager
    {
        // deze classe zorgt voor de enemy en player classe zoals het al zegt managed het
        #region Variables
        private List<Entity> entities;
        private List<List<string>> attributes, contents;
        private FileManager fileManager;
        private InputManager inputManager;
        private Level1 map;
        #endregion

        #region properties
        public List<Entity> Entities {get {return entities;}}
        #endregion

        #region constructor/loadcontent

        public void LoadContent(string entityType, ContentManager Content, string fileName, string Identifier, InputManager inputManager)
        {
            this.inputManager = inputManager;
            map = new Level1();
            entities = new List<Entity>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            fileManager = new FileManager();

            if (Identifier == String.Empty)
            {
                fileManager.LoadContent(fileName, attributes, contents);
            }
            else
            {
                fileManager.LoadContent(fileName, attributes, contents, Identifier);
            }

            for (int i = 0; i < attributes.Count; i++)
            {// het aanmaken van een entities 
                Type newClass = Type.GetType("TheJourneyGame." + entityType);
                entities.Add((Entity)Activator.CreateInstance(newClass));
                entities[i].LoadContent(Content, attributes[i], contents[i], this.inputManager);
            }
        }
        #endregion

        #region Unload
        // voor elke entity
        public void UnloadContent()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].UnloadContent();
            }
        }

        #endregion 

        #region Update

        // updaten van elke entity
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime, inputManager, map.Layer);
            }
        }

        #endregion

        #region entityCollision
        // als een entity met elkaar collision hebben
        public void EntityCollision(EntityManager en)
        {
            foreach (Entity e in entities)
            {
                foreach (Entity ee in en.Entities)
                {
                    if( e.Box.Intersects(ee.Box))
                    {
                        e.OnCollision(ee);
                    }
                }
            }
        }

        #endregion

        #region Draw

        //voor elke entity teken deze
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(spriteBatch);
            }
        }
        #endregion

    }
}
