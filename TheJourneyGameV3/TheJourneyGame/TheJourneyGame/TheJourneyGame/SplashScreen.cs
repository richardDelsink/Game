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
    /// de splashscreen
    /// In een text file word gezegd welke images het moet pakken voor deze scherm
    class SplashScreen : GameScreen
    {
        #region data
        
        // het bijhouden van een lijst van de fade animatie
        private List<FadeAnimation> fade; 
        // een lisjt voor het bijden vvan de plaatjes die ingeladen wordt
        private List<Texture2D> images;
        // aanroepen van de filemanager
        private FileManager filemanager;
        // een counter het faden naar een andere scherm
       private int imageNumber;

        #endregion

        #region methods / constructor

       #region Load/Constructor
       public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            // hier worden de attributen gemaakt
            base.LoadContent(Content, inputManager);
            imageNumber = 0;
            filemanager = new FileManager();
            fade = new List<FadeAnimation>();
            images = new List<Texture2D>();
            filemanager.LoadContent("Load/Splash.txt", attributes, contents);

            // hier wordt een loop aangeroepn dat voor elke image een fade heeft  hierbij
            // hierbij is gebruikt gemaakt van lijst in een lijst omdat er verschillende attributen tegelijkertijd moet worden bijgehouden
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents[i][j]));
                            fade.Add(new FadeAnimation());
                            break;
                    }
                }
            }
       
            // hier wordt isActive op true gezet als de images zijn ingeladen, 
            //alleen pas als de fade kleinter is dan I

            for (int i = 0; i < fade.Count; i++)
            {
                fade[i].LoadContent(content, images[i], "", Vector2.Zero);
                fade[i].IsActive = true;
            }
         }
       #endregion

       #region Unload
       public override void UnloadContent()
        {
            // hier word alles uitgeladen en de filemanager wordt weer leeggehaald
            base.UnloadContent();
            filemanager = null;
        }

       #endregion

       #region update
       public override void Update(GameTime gameTime)
        {
            // voor elke update wordt hiernaar gekeken met de fade 
           
           fade[imageNumber].Update(gameTime);

           if (fade[imageNumber].Alpha == 0.0f)
            {
                imageNumber++;
            }

           if (imageNumber >= fade.Count - 1 )
           {
               ScreenManager.Instance.AddScreen(new MenuScreen(), inputManager);
           }
            
        }

       #endregion

       #region Draw
       public override void Draw(SpriteBatch spriteBatch)
        {
            // hier wordt het getekent
            fade[imageNumber].Draw(spriteBatch);
        }

       #endregion

        #endregion
    }
}
