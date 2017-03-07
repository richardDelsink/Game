using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace TheJourneyGame
{
    class FileManager
    {
        // het lezen van files 
        #region variables

        /// <summary>
        ///  het bijhouden van de files die je inlaad, en kijken welke attriibuten ze hebben
        /// </summary>
        private enum LoadType { Attributes, Contents };
        private LoadType type;
        private List<string> tempAttributes, tempContents;
        private bool identifierFound = false;
        
        #endregion

        #region Constructor/Loadcontent

        #region start reading

        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {// voor alles wat met Load= begint, hiervoor word een tekstbestand gebruikt
                    string line = reader.ReadLine();
                    if (line.Contains("Load="))
                    {
                        tempAttributes = new List<string>();
                        line = line.Remove(0, line.IndexOf("=") + 1);
                        type = LoadType.Attributes;
                    }
                    else
                    {
                        type = LoadType.Contents;
                    }

                    tempContents = new List<string>();
                    // ze worden gesplit door hieronder aangeven tekens
                    string[] lineArray = line.Split(']');
                    foreach (string l in lineArray)
                    {
                        string newLine = l.Trim('[', ' ', ']');
                        if (newLine != String.Empty)
                        {
                            if (type == LoadType.Contents)
                                tempContents.Add(newLine);
                            else
                                tempAttributes.Add(newLine);
                        }
                    }

                    if (type == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(tempAttributes);
                    }
                }
            }
        }

        #endregion

        #region end reading
        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, string identifier)
        {
            using (StreamReader reader = new StreamReader(filename))
            {// voor endload in tekstbestanden 
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    else if (line.Contains("Load=") && line.Contains(identifier))
                    { // hier kijkt het als het waaar is dan gaat het ook verder want 
                        // voor de rest gebeurd er niks vandaar continue
                        identifierFound = true;
                        continue;
                    }

                    if (identifierFound)
                    {
                        if (line.Contains("Load="))
                        {
                            tempAttributes = new List<string>();
                            line = line.Remove(0, line.IndexOf("=") + 1);
                            type = LoadType.Attributes;
                        }
                        else
                        {
                            tempContents = new List<string>();
                            type = LoadType.Contents;
                        }

                        string[] lineArray = line.Split(']');
                        foreach (string l in lineArray)
                        {
                            string newLine = l.Trim('[', ' ', ']');
                            if (newLine != String.Empty)
                            {
                                if (type == LoadType.Contents)
                                    tempContents.Add(newLine);
                                else
                                    tempAttributes.Add(newLine);
                            }
                        }

                        if (type == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(tempAttributes);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
