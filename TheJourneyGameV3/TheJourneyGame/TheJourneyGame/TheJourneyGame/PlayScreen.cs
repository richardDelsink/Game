using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TheJourneyGame
{
    class PlayScreen : GameScreen
    {// het speel scherm 
        #region variables
        private EntityManager player, enemies;
        private Level1 map;
        private Song music;
        #endregion

        #region Constructor / Load
        public override void LoadContent(ContentManager content, InputManager input)
        {
            base.LoadContent(content, input);
            player = new EntityManager();
            enemies = new EntityManager();
            map = new Level1();
            frame = new Rectangle(0, 0, 4800, 640);

            map.LoadContent(content, map, "Level1");
            player.LoadContent("Player", content, "Load/Player.txt","", input);
            enemies.LoadContent("Enemy", content, "Load/Enemies.txt", "Level1", input);
            background = content.Load<Texture2D>("Content/Background/sky2");
            music = content.Load<Song>("Content/Sound/LW");

            MediaPlayer.Volume = 0.04f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);  
        }

        #endregion

        #region methods

        #region Unload
        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            enemies.UnloadContent();
            map.UnloadContent();
            music = null;
            background = null;    
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
          
           inputManager.Update();
           player.Update(gameTime);
           enemies.Update(gameTime);
           map.Update(gameTime);
           player.EntityCollision(enemies); // enitity collision met enemy en of player
           enemies.EntityCollision(player);

            // voor elke enitity(player,enemy) dat getekent wordt
           Entity e;
                for(int i = 0; i < player.Entities.Count; i++)
                {
                    e = player.Entities[i];
                    map.UpdateCollision(ref e);
                    player.Entities[i] = e;
                }
                for (int i = 0; i < enemies.Entities.Count; i++)
                {
                    e = enemies.Entities[i];
                    map.UpdateCollision(ref e);
                    enemies.Entities[i] = e;
                }         
        }

        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, frame, Color.White);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemies.Draw(spriteBatch);
        }
        #endregion

        #endregion
    }
}
