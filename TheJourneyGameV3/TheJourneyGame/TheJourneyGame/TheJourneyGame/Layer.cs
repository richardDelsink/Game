using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyGame
{
    class Layer
    {
        /// <summary>
        /// het inladen van de layers
        /// eerst had ik de alle layers in een keer ingeladen
        /// met deze classe kan je aperte layers inladen
        /// </summary>
        #region Variables
        private List<Tile> tiles; // lijst met tiles
        private List<List<string>> attributes, contents; // content en attributen 
        private List<string> motion, solid; // deze geeft aan of het een beweging heeft en of je er doorheen kan of niet
        private FileManager fileManager;
        private ContentManager content;
        private Texture2D tileSheet; //  deze leest de afb
        private string[] getMotion;
        private string nullTile; // geeft aan of het leeg is deze wordt aangeven met [---] het het tekstbestand
        // anders maakt het lege tiles aan die je niet eens gebruikt
        #endregion

        #region properties
        // dimensie van de tiles
        public static Vector2 TileDimensions { get { return new Vector2(64,64); } }
        #endregion

        #region constructor / loadcontent
        public void LoadContent(Level1 map, string layerID)
        {
            tiles = new List<Tile>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            motion = new List<string>();
            solid = new List<string>();
            fileManager = new FileManager();
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            fileManager.LoadContent("Load/Maps/" + map.Id + ".txt", attributes, contents, layerID);

            int indexY = 0; // inladen van de attributes beginnen op de Y as op 0

            // list in een list wordt hier gebruikt
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "TileSet":
                            tileSheet = content.Load<Texture2D>("TileSets/" + contents[i][j]);
                            break;
                        case "Solid":
                            solid.Add(contents[i][j]);
                            break;
                        case "Motion":
                            motion.Add(contents[i][j]);
                            break;
                        case "NullTile":
                            nullTile =  contents[i][j];
                            break;
                        case "StartLayer":
                            Tile.Motion tempMotion = Tile.Motion.Static;
                            Tile.State tempState;

                            for (int k = 0; k < contents[i].Count; k++)
                            {// als het geen nulltile is dan wordt er iets toegevoegd
                                if (contents[i][k] != nullTile)
                                {
                                    string[] split = contents[i][k].Split(',');
                                    tiles.Add(new Tile());
                                    // wordt gekeken of het begweing of solid heeft
                                    if (solid.Contains(contents[i][k]))
                                    {
                                        tempState = Tile.State.Solid;
                                    }
                                    else
                                    {
                                        tempState = Tile.State.Passive;
                                    }

                                    foreach (string m in motion)
                                    {// getmotion heeft 2 properties [2,0:Horizontal]
                                        // vandaar dat het op deze manier is gesplit
                                        getMotion = m.Split(':');
                                        if (getMotion[0] == contents[i][k])
                                        {
                                            tempMotion = (Tile.Motion)Enum.Parse(typeof(Tile.Motion), getMotion[1]);
                                            break;
                                        }
                                    }
                                    // hier moet inplaats van 64 TileDiemnsion komen
                                    tiles[tiles.Count -1].SetTile(tempState, tempMotion, new Vector2(k * 64, indexY * 64), tileSheet,
                                        new Rectangle(int.Parse(split[0]) * 64, int.Parse(split[1]) * 64, 64, 64));
                                }
                            }

                            
                            indexY++;
                            break;
                    }
                }
            }
        }

        #endregion

        #region UpdateCollsion
        // update de collision van de entity op de Tile
        public void UpdateCollision(ref Entity e)
        {
            // voor elke tile die je in de list heb met referentie naar entity
            for (int i = 0; i < tiles.Count; i++)
            {
                  tiles[i].UpdateCollision(ref e);
            }
        }
        #endregion

        #region Update

        //voor elke tile dat getekent is updaten
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                  tiles[i].Update(gameTime);     
            }
        }

        #endregion

        #region Draw
        // voor elke tile wordt het getekent
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
              
                    tiles[i].Draw(spriteBatch);
            }
        }
        #endregion 
    }
    
}
