using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyGame
{/// het bijhouden welke scherm nou actief is
 /// eigen screenmanager 
 /// de game1 wordt bijna of nauwelijke niet gebruikt
    class ScreenManager
    {
        
        #region Variables

        /// Gebruiken van region zodat we makkelijk kunnen aanpassen
        /// BV ik wil alleen naar variables toe gaan, deze kan je dan makkelijker
        /// gebruiken om aan te passen
        
        /// instance
        private static ScreenManager instance;
        private GameScreen currentScreen;
        // Hoef niet is mogelijk 
        private GameScreen newScreen;
        // eigen conten manager
        private ContentManager content;

        /// <summary>
        /// Zodat we kunnen navigeren van naar welke scherm we willen gaan
        /// BV je zit bij het optie menu en je wilt terug gaan de stack die heeft dan de vorige scherm 
        /// bewaard en kan dus terug gaan naar het vorige scherm
        /// </summary>
        private Stack<GameScreen> screenStack = new Stack<GameScreen>();

        /// <summary>
        /// Scherm grootte
        /// </summary>
        private Vector2 window;
        private bool transition;
        private FadeAnimation fade = new FadeAnimation();
        private Texture2D fadeTexture;
        private InputManager inputManager;
        private Texture2D nullImage;
        

        #endregion

        #region Properties

        /// <summary>
        /// Heb er een public static van gemaakt omdat we er maar een container nodig hebebn
        /// Die input parameters hanteerd, ze hoeven niks te setten of getten
        /// </summary>
        public static ScreenManager Instance
        { 
            get
            {
                if (instance == null)
                instance = new ScreenManager();
                return instance;
            }
        }

        public Vector2 Window { get { return window; } set { window = value; } }
        public ContentManager Content { get { return content; } }
        public Texture2D NullImage { get { return nullImage; } }

        #endregion 

        #region Methods / constructor

        #region constructor
        /// zetten default value
        /// toevoegen van een scherm
        public void AddScreen(GameScreen screen, InputManager inputManager)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.Alpha = 1.0f;
            fade.ActivatieValue = 1.0f;
            this.inputManager = inputManager;
        }

        #endregion

        #region initialize

        public void Initialize() 
        {
            currentScreen = new SplashScreen();
            fade = new FadeAnimation();
            inputManager = new InputManager();
        }

        #endregion

        #region load
        public void LoadContent(ContentManager Content) 
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            nullImage = this.content.Load<Texture2D>("Content/Background/null");
            currentScreen.LoadContent(Content, this.inputManager);
            fadeTexture = this.content.Load<Texture2D>("Content/Sprites/testfader");
            fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            fade.Scale = window.X;     
        }

        #endregion

        #region Update
        //upaten van de scherm
        public void UpDate(GameTime gameTime) 
        {
            if (!transition)
            {
                currentScreen.Update(gameTime);
            }
            else
            {
                Transition(gameTime);
            }
            Camera.Instance.Update();
        }

        #endregion

        #region Update
        // het maken van de scherm
        public void Draw(SpriteBatch spriteBatch) 
        {// als er trnasitie is dan komt er een fade
            currentScreen.Draw(spriteBatch);
            if (transition)
            {
                fade.Draw(spriteBatch);
            } 
        }
        #endregion

        #region Transition
        private void Transition(GameTime gameTime)
        {//  er een fade tussen de scherm waar je nu bent en op  de scherm ie moet komen 
            fade.Update(gameTime);
            if (fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent(content, this.inputManager);
            }

            else if (fade.Alpha == 0.0f)
            {
                transition = false;
                fade.IsActive = false;
            }
        }

        #endregion
        #endregion

    }
}
