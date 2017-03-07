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
    class Enemy : Entity
    {
        /// dit is de enemy classe hier wordt de attributen van de enemie bepaald
        /// alle variables zijn in de Entity te zien met de properties
        
        #region Constructor / methods

        #region Constructor/loadcontent
        // hier wordt de content van de enemy ingeladen
        public override void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {

            base.LoadContent(content, attributes, contents, input); 
            direction = 1;
            originPosition = position;
            moveAnimation.IsActive = true;
            health = 1;

            if (direction == 1)
            {
                destPosisition.X = originPosition.X + range;
            }
            else
            {
                destPosisition.X = originPosition.X - range;
            }
            
        }

        #endregion

        #region 
        //leeghalen van content
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        #endregion

        #region MovementUpdate

        // hier wordt de de beweging van de eenemie bepaald
        // in de loadcontent hebben al gezet dat animatie true is
        public override void Update(GameTime gameTime, InputManager inputManager, Layer layer)
        {
            base.Update(gameTime, inputManager, layer);
            //TODO
            // als het een bepaalde richting heeft geven wij een snelheid in de richting
            // op het einde zeggen we ook welke animatie het moet afspelen
            // de range bepaald tot hoever het kan lopen
            //moveAnimation.CurrentFrame.X geeft aan: op de x as en dan zetten de bv een 0 erbij dan op welke Y as index 
            if (direction == 1)
            {
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
            }

            else if (direction == 2)
            {
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
            }

            if (direction == 1 && position.X >= destPosisition.X)
            {
                direction = 2;
                destPosisition.X = originPosition.X - range;
            }

            else if (direction == 2 && position.X <= destPosisition.X)
            {
                direction = 1;
                destPosisition.X = originPosition.X + range;
            }
             
            position += velocity;
            moveAnimation.Update(gameTime);
            moveAnimation.Position = position;
                 
        }

        #endregion

        #region OnCollison
        public override void OnCollision(Entity e)
        {
           // zegt wat er moet gebeuren als het een collision heetf met een entiteit
            // op dit moment niks 
        }
        #endregion

        #region Draw
        // het tekenen
        public override void Draw(SpriteBatch spriteBatch)
        {
            //TODO
            moveAnimation.Draw(spriteBatch);
        }
        #endregion

        #endregion
    }
}
