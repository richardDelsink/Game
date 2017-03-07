using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyGame
{
    class Tile
    {// zorgt voor het level
        #region Variables

        private State state; // geeft aan wat het is bv solid
        private Motion motion; // geeft aan of het beweging heeft bv horizontal
        private Vector2 position, prevPosition, velocity; // positie snelheid en vorige positie omdat je wilt weten waar het object was
        private Texture2D tileImage; // inladen van de hele imgae 
        private Animation animation; // animatie
        private bool containsEntity; // als het oook een etnity heeft 
        private float counter, range, moveSpeed; 
        private bool increase; // true or false
      
        #endregion

        #region Prop

        public enum State { Solid, Passive , Death};
        public enum Motion { Static, Horizontal, Vertical };

        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

        #endregion

        #region constructor / methods

        #region cropimage

        // we laden eerst de hele image in dit zien wij niet
        // verder gaan we de image knippen in aantal stukken wat jij hebt opgegven
        // deze bewaren wij allemaal zodat we het maar 1x hoeven te doen
        // ik had eerst dat het steeds de hele image inlaad en elke keer moet bij knippen
        // nu is dat niet zo, je hoeft maar 1x de image inladen en alles wat je knipt wordt onthouden

        private Texture2D CropImage(Texture2D tileSheet, Rectangle tileArea)
        {
            Texture2D croppedImage = new Texture2D(tileSheet.GraphicsDevice, tileArea.Width, tileArea.Height);

            Color[] tileSheetData = new Color[tileSheet.Width * tileSheet.Height];
            Color[] croppedImageData = new Color[croppedImage.Width * croppedImage.Height];

            tileSheet.GetData<Color>(tileSheetData);

            int index = 0;
            for (int y = tileArea.Y; y < tileArea.Y + tileArea.Height; y++)
            {
                for (int x = tileArea.X; x < tileArea.X + tileArea.Width; x++)
                {
                    croppedImageData[index] = tileSheetData[y * tileSheet.Width + x];
                    index++;
                }
            }

            croppedImage.SetData<Color>(croppedImageData);

            return croppedImage;
        }
        #endregion

        #region constructor
        public void SetTile(State state, Motion motion, Vector2 position, Texture2D tileSheet, Rectangle tileArea)
        {
            this.state = state;
            this.motion = motion;
            this.position = position;
            increase = true;
            containsEntity = false;
            velocity = Vector2.Zero;
 
            tileImage = CropImage(tileSheet, tileArea);
            range = 75; // kan maar tot zover bewegen als het animatie heeft
            counter = 0;

            moveSpeed = 100f; // snelheid van de tiles als het een animatie heeft
            animation = new Animation();
            animation.LoadContent(ScreenManager.Instance.Content, tileImage, "", position);
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {// hier wordt een movespeed gegevn aan de tiles als de counter tot een bepaald aantal komt
            // beweegt tot zover van de range

            counter += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            prevPosition = position;

            if (counter >= range)
            {
                counter = 0;
                increase = !increase;
            }

            // als het een beweging heeft voor verticaal en horizontaal
            if (motion == Motion.Horizontal)
            {
                if (increase)
                {
                    velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else if (motion == Motion.Vertical)
            {
                if (increase)
                {
                    velocity.Y = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    velocity.Y = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            position += velocity;
            animation.Position = position;

      }
        #endregion

        #region Collsion

        public void UpdateCollision(ref Entity e)
        {// hier wordt de bounbox classe gebruikt
            // voor elke tile komt een boundingbox
            // elke enititeit heeft ook een boundingbox
           BoundBox box = new BoundBox(position.X, position.Y, Layer.TileDimensions.X, Layer.TileDimensions.Y);
          
           // als een entitiet op een tile is en het heeft een entiteit
              if (e.OnTile && containsEntity )
               {// als een entiteit niet op een ynsctylepositie staat dan wordt het gesynced
                   if (!e.SyncTilePosition)
                   {
                       e.Position += velocity;
                       e.SyncTilePosition = true;
                   }
                  // als niet in aanraking komt met een tile
                   if (e.Box.Right < box.Left || e.Box.Left > box.Right || e.Box.Bottom != box.Top)
                   {
                       e.OnTile = false;
                       containsEntity = false;
                       e.ActivateGravity = true;
                   }
               }
           // als een entiteit met bounding box op een solid tile staat
            if (e.Box.Intersects(box) && state == State.Solid)
            {
                // er wordt een vorige boundingbox de speler aangemaakt en een vorige boundingbox voor de tile

                BoundBox prevPlayer = new BoundBox(e.PrevPosition.X, e.PrevPosition.Y, e.MoveAnimation.FrameWidth, e.MoveAnimation.FrameHeight);
                BoundBox prevTile = new BoundBox(prevPosition.X, prevPosition.Y, Layer.TileDimensions.X, Layer.TileDimensions.Y);
                
                // hier wordt het het gekeken of de eniteit op een tile staat
                if (e.Box.Bottom >= box.Top && prevPlayer.Bottom <= prevTile.Top)
                {
                    e.Position = new Vector2(e.Position.X, position.Y - e.MoveAnimation.FrameHeight);
                    e.ActivateGravity = false;
                    e.OnTile = true;
                    containsEntity = true;
                }
                    // anders niet en wordt gravity true en valt de entiteit naar beneden
                else if (e.Box.Top <= box.Bottom && prevPlayer.Top >= prevTile.Bottom)
                {
                    e.Position = new Vector2(e.Position.X, position.Y + Layer.TileDimensions.Y);
                    e.Velocity = new Vector2(e.Velocity.X, 0);
                    e.ActivateGravity = true;
                }
                    // deze is voornamelijk voor de enemie als dat een richting van 1 heeft wordt het omgezet naar 2 anders 1
               else if(e.Box.Right >= box.Left && prevPlayer.Right <= prevTile.Left)
                {
                    e.Position = new Vector2(position.X - e.MoveAnimation.FrameWidth, e.Position.Y);
                    if (e.Direction == 1)
                    {
                        e.Direction = 2;
                    }

                    else
                    {
                        e.Direction = 1;
                    }
                }

                else if (e.Box.Left <= box.Right && prevPlayer.Left >= prevTile.Left)
                {
                    e.Position = new Vector2(position.X + Layer.TileDimensions.X, e.Position.Y);
                    if (e.Direction == 1)
                    {
                        e.Direction = 2;
                    }

                    else
                    {
                        e.Direction = 1;
                    }
                }
              
            }

            e.MoveAnimation.Position = e.Position;
        }

        # endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }

        #endregion

        #endregion
    }
}
