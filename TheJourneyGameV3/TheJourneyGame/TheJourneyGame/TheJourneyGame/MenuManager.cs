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
    class MenuManager
    {// regelt de menu

        #region Variables

        private List<string> menuItems, linkType, linkID; // list van menu items
        private List<Texture2D> menuImages; // als je imagese wilt inplaats van font voor de menuitems
        private List<string> animationTypes; // animatie types
        private List<List<Animation>> animation; // verschillende menu items kunnen meerdere soorten animaties hebben
        private ContentManager content; // content
        private FileManager fileManager; /// lezen
        private Vector2 position; 
        private List<List<string>> attributes, contents; // attributen lezen en conet
        private List<Animation> tempAnimation;
        private int axis, itemNumber; // nummer van waar je bent op de menu
        private SpriteFont font;
        private Rectangle source;
        private string align; // alignen van menuitems

        #endregion

        #region methods and construtor
        #region menuItems
        private void SetMenuItems()
        {// voor elke image
            for( int i = 0; i < menuItems.Count; i++)
            {
                if (menuImages.Count == i)
                { 
                    menuImages.Add(ScreenManager.Instance.NullImage); 
                }
            }

            // voor elke items(font)
            for (int i = 0; i < menuImages.Count; i++)
            {
                if (menuItems.Count == 1)
                {
                    menuItems.Add("");
                }

            }
        }
        #endregion

        #region Set animation and align
        private void SetAnimations()
        {
            Vector2 dimensions = Vector2.Zero;

            Vector2 addPos = Vector2.Zero;

            ///Het alignen van de menu items
            ///als de align naar center is gezet dan wordt er gekeken hoe groot het scherm is
            ///deze word berekent zodat de menu items in het midden komt van het scherm
            ///
            if (align.Contains("Center"))
            {
                for (int i = 0; i < menuItems.Count; i++)
                {
                    //voor elke item  
                    dimensions.Y += font.MeasureString(menuItems[i]).Y + menuImages[i].Height;
                    dimensions.X += font.MeasureString(menuItems[i]).X + menuImages[i].Width;
                }

                // align center op de X as van het hele menu
                if (axis == 1)
                {
                    addPos.X = (ScreenManager.Instance.Window.X - dimensions.X) / 2;
                }
                    // align center op de Y as van het hele menu
                else if (axis == 2)
                {
                    
                    addPos.Y = (ScreenManager.Instance.Window.Y - dimensions.Y) / 2;
                    
                }
            }
            else
            {
                addPos = position;
            }

            tempAnimation = new List<Animation>();
            

            for (int i = 0; i < menuImages.Count; i++)
            {
                dimensions = new Vector2(font.MeasureString(menuItems[i]).X + menuImages[i].Width, font.MeasureString(menuItems[i]).Y + menuImages[i].Height);

                // allign op de y as van de menu items ten opzichte van het scherm
                if (axis == 1)
                {
                    addPos.Y = (ScreenManager.Instance.Window.Y - dimensions.Y) / 2;
                }
                    // allign op de x as van de menu items
                else
                 {
                    addPos.X = (ScreenManager.Instance.Window.X - dimensions.X) / 2;
                 }
                
                for (int j = 0; j < animationTypes.Count; j++)
                {
                    switch (animationTypes[j])
                    {
                            //we hebben 1 animatie type de fade 
                        case "Fade":
                            tempAnimation.Add(new FadeAnimation());
                            tempAnimation[tempAnimation.Count - 1].LoadContent(content,menuImages[i], menuItems[i], addPos);
                            tempAnimation[tempAnimation.Count - 1].Font = font;
                            break;
                    }
                }

                if (tempAnimation.Count >= 0)
                {
                    animation.Add(tempAnimation);
                }

                tempAnimation = new List<Animation>();
                // alignen van de menuItems
                if (axis == 1)
                {
                   // addPos.X += dimensions.X;
                }
                else
                {
                    addPos.Y += dimensions.Y;
                }
            }
        }

        #endregion

        #region constructor / load
        public void LoadContent(ContentManager Content, string id)
        {
            
            this.content = new ContentManager(Content.ServiceProvider, "Content");
            menuItems = new List<string>();
            menuImages = new List<Texture2D>();
            animationTypes = new List<string>();
            animation = new List<List<Animation>>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            itemNumber = 0;
            position = Vector2.Zero;
            fileManager = new FileManager();
            linkType = new List<string>();
            linkID = new List<string>();
            
            fileManager.LoadContent("Load/Menu.txt", attributes, contents, id);
            // de filemaner leest
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Font":
                            font = this.content.Load<SpriteFont>(contents[i][j]);
                            break;
                        case "Item":
                            menuItems.Add(contents[i][j]);
                            break;
                        case "Image":
                            menuImages.Add(this.content.Load<Texture2D>(contents[i][j]));
                            break;
                        case "Axis":
                            axis = int.Parse(contents[i][j]);
                            break;
                        case "Position":
                            string[] temp = contents[i][j].Split(' ');
                            position = new Vector2(float.Parse(temp[0]),float.Parse(temp[1]));
                            break;
                        case "Source":
                            temp = contents[i][j].Split(' ');
                            source = new Rectangle(int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2]), int.Parse(temp[3]));
                            break;
                        case "Animation":
                            animationTypes.Add(contents[i][j]);
                            break;
                        case "Align":
                            align = contents[i][j];
                            break;
                        case "LinkType":
                            linkType.Add(contents[i][j]);
                            break;
                        case "LinkID":
                            linkID.Add(contents[i][j]);
                            break;
                    }
                }
            }
            SetMenuItems(); // szet je de menu items 
            SetAnimations(); // zet je de animatie
        }
        #endregion

        #region Unload
        // haalt alles weg
        public void UnloadContent()
        {
            
            content.Unload();
            fileManager = null;
            position = Vector2.Zero;
            animation.Clear();
            menuItems.Clear();
            menuImages.Clear();
            animationTypes.Clear();
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime, InputManager inputManager)
        {
            
            // als je inout geeft 
            inputManager.Update();
            if (axis == 1)
            {// itemNumber van menuiitems krijg - 1 of -2 
                // hierdoor kan je naar boven of beneden
                if (inputManager.KeyPressed(Keys.Right, Keys.D))
                {
                    itemNumber++;
                }
                else if (inputManager.KeyPressed(Keys.Left, Keys.A))
                {
                    itemNumber--;
                }
            }
            else
            {
                if (inputManager.KeyPressed(Keys.Down, Keys.S))
                {
                    itemNumber++;
                }
                else if (inputManager.KeyPressed(Keys.Up, Keys.W))
                {
                    itemNumber--;
                }
 
            }

            // als je enter drukt gaat het naar een andere scherm

            if (inputManager.KeyPressed(Keys.Enter))
            {
                if (linkType[itemNumber] == "Screen")  
                {
                    Type newClass = Type.GetType("TheJourneyGame." + linkID[itemNumber]);
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), inputManager);
                }
            }

            if (itemNumber < 0)
            {   
                itemNumber = 0; 
            }

            else if (itemNumber > menuItems.Count - 1)
            {
                itemNumber = menuItems.Count - 1;
            }

        // voor elke animatie /  menuitems
            for(int i = 0; i < animation.Count; i++)
            {
                for (int j = 0; j < animation[i].Count; j++)
                {
                    if (itemNumber == i)
                    {
                        animation[i][j].IsActive = true;
                        
                        
                    }

                    else
                    {
                        animation[i][j].IsActive = false;
                       
                    }
                    animation[i][j].Update(gameTime);

                }
            }
        }

        #endregion

        #region
        public void Draw(SpriteBatch spriteBatch)
        {
               // het wordt de aniamtie wordt hier getekent
            for (int i = 0; i < animation.Count; i++)
            {
                for (int j = 0; j < animation[i].Count; j++)
                {
                    animation[i][j].Draw(spriteBatch);
                }
            }
        }
        #endregion
        #endregion
    }
}
