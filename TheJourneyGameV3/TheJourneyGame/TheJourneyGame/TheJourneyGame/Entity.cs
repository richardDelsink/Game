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
    class Entity
    {
        // deze is de super classe van de enemy en player
        #region variables

       // de variablies die de enemy en player allebij hebben
        // de player hoeft niet perse alle variables te gbruiken 
        // maar wordt wel ingeladen zoals direction die de speler niet gebruikt

        protected float moveSpeed, gravity; // snelheud dat je beweegt en de zwaarte kracht
        protected bool activateGravity, syncTilePosition, onTile; // kijken of het actief is
        protected int range, direction, health;

        protected ContentManager content;
        protected SSAnimation moveAnimation; // ssanimatie aanroepen
        protected Texture2D image;
        protected Vector2 position, velocity;
        protected Vector2 prevPosition;
        protected Vector2 destPosisition, originPosition;
        


        #endregion

        #region properties

        //properties
        public int Direction 
        
        { 
            get 

            { 
                return direction; 
            } 
            set 
            { 
                direction = value;
                destPosisition.X = (direction == 2) ? destPosisition.X = originPosition.X - range :
                    destPosisition.X = originPosition.X + range;
            } 
        }
       
        public Vector2 PrevPosition { get { return prevPosition; } }
        public SSAnimation MoveAnimation { get { return moveAnimation; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public BoundBox Box { get { return new BoundBox(position.X, position.Y, moveAnimation.FrameWidth, moveAnimation.FrameHeight); } }

        public bool OnTile { get { return onTile; } set { onTile = value; } }
        public bool ActivateGravity { set { activateGravity = value; } }
        public bool SyncTilePosition { get { return syncTilePosition; } set { syncTilePosition = value; } }

        #endregion

        #region constructor & methods

        #region Constructor
        // Consctructor 
        public virtual void LoadContent(ContentManager content,List<string> attributes, List<string> contents, InputManager input)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            moveAnimation = new SSAnimation();
            // voor het lezen van teks in bestanden gebruik ik loops
            for (int i = 0; i < attributes.Count; i++)
            {

                switch (attributes[i])
                {
                    case "Health":
                        health = int.Parse(contents[i]);
                        break;
                    case "Frames":
                        string[] frames = contents[i].Split(',');
                        moveAnimation.Frames = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                        break;
                    case "Image":
                        image = this.content.Load<Texture2D>(contents[i]);
                        break;
                    case "Position":
                        frames = contents[i].Split(',');
                        position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                        break;
                    case "MoveSpeed":
                        moveSpeed = float.Parse(contents[i]);
                        break;
                    case "Range":
                        range = int.Parse(contents[i]);
                        break;
                }
            }

            gravity = 100f;
            velocity = Vector2.Zero;
            activateGravity = true;
            syncTilePosition = false;
            onTile = false;
            moveAnimation.LoadContent(content, image, "", position);
        }

        #endregion

        #region Unload
        public virtual void UnloadContent()
        {
            content.Unload();
        }
        #endregion

        #region Update
        public virtual void Update(GameTime gameTime, InputManager inputManager, Layer layer)
        {
            // de enemie en player hebben dit allebij
            //TODO
           syncTilePosition = false;
           moveAnimation.IsActive = true;
           prevPosition = position;
 
        }
        #endregion
        #region OnCollsion
        public virtual void OnCollision(Entity e)
        {
           // wordt niks mee gedaan op dit moment
        }

        #endregion

        #region Draw
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //TODO
            // wordt niks mee gedaan op dit moment
        }

        #endregion

        #endregion
    }
}
