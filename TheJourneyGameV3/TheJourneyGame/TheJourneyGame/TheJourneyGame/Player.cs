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
    class Player : Entity
    {
        // de speler 
        #region Variables
        private float jumpSpeed; // snelheid van het springen
        private Texture2D hpBar; 
        private ParticleManager particleManager;
        #endregion 

        // geen properties nodig
        
        #region Constructor / Load
        public override void  LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
            base.LoadContent(content, attributes, contents, input);
            hpBar = content.Load<Texture2D>("Content/Sprites/Heart");
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("Content/Sprites/Cloud"));
            particleManager = new ParticleManager(textures, new Vector2(400, 240));
            health = 3;
            jumpSpeed = 1500f;
        }
        #endregion

        #region method

        #region Unload
        public override void UnloadContent()
        {
            base.UnloadContent();
            hpBar = null;
            moveAnimation.UnloadContent();
        }
        #endregion

        #region Update / Collision

        #region update
        public override void Update(GameTime gameTime, InputManager inputManager, Layer layer)
        {
            //TODO
            // als je bepaalde knoppen indrukt krrijg je verschillende visuele feedback
            base.Update(gameTime, inputManager , layer);  
            if (inputManager.KeyDown( Keys.D))
            {
                if (inputManager.KeyDown(Keys.D) && inputManager.KeyDown(Keys.LeftShift))
                {
                    moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 3);
                    velocity.X = (moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) * 2;
                    particleManager.EmitterLocation = new Vector2(position.X + 10, position.Y + 60 );
                    particleManager.Refresh();
                }
                else
                {
                    moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
                    velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    particleManager.Clear();
                    
                }
            }

            else if (inputManager.KeyDown( Keys.A))
            {
                if (inputManager.KeyDown(Keys.A) && inputManager.KeyDown(Keys.LeftShift))
                {
                    moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 2);
                    velocity.X = (-moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) * 2;
                    particleManager.EmitterLocation = new Vector2(position.X + 45, position.Y + 60);
                    particleManager.Refresh();
                }
                else
                {
                    moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                    velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    particleManager.Clear();
                }
            }
            
            else
            {
                particleManager.Clear();
                moveAnimation.IsActive = false;
                velocity.X = 0;
            }

            // als gravity niet actief is en je hebt de jump key ingedrukt dan kan je springen
            if (inputManager.KeyDown(Keys.Space) && !activateGravity)
            {
                velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                activateGravity = true;
            }
          
            // als health 0 = dan restart het de level
            if (health == 0)
            {
                Type newClass = Type.GetType("TheJourneyGame." + "PlayScreen");
                ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), inputManager);
            }
                // als de groter dan de screen.Y is dan heeft het 0 hp
            else if (position.Y >= ScreenManager.Instance.Window.Y )

            {
                health = 0;    
            }
            // als gravity aan is ga je wer naar beneden
            if (activateGravity)
            {
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                velocity.Y = 0;
            }

            position += velocity;
            moveAnimation.Update(gameTime);
            moveAnimation.Position = position;
            
            // set camera focal point op karakter
            Camera.Instance.SetFocalPoint(new Vector2(position.X, ScreenManager.Instance.Window.Y / 2));      
        }
        #endregion

        #region Collsion
        public override void OnCollision(Entity e)
        {// als het collision heeft met entity dan wordt de positie gereset en health -1 gedaan
            Type type = e.GetType();
            if (type == typeof(Enemy))
            {
                health--;
                position.X = 64;
                position.Y = 512;
                
            }
        }
        #endregion
        #endregion

        #region draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            //TODO
            moveAnimation.Draw(spriteBatch);
            particleManager.Draw(spriteBatch);
            // voor elke int health word een hartje aangemaakt
            // deze heeft een ofset van 20 doormidel van int X wordt er steed 20 bij geteld als het doorheen looped
            int X = 0;
            for (int i = 0; i < health; i++)
            {
                spriteBatch.Draw(hpBar, new Vector2(position.X + X, position.Y - 15), Color.White);
                X += 20;
            }
        }
        #endregion

        #endregion
    }
}
