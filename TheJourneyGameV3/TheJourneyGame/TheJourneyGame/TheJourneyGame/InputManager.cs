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
    class InputManager
    {
        /// <summary>
        /// Deze classe kijkt welke knoppen zijn ingedrukt en welke niet
        /// regelt de input
        /// </summary>
        #region Variables 
        private KeyboardState prevKeyState, keyState;
        #endregion

        #region properties
        // of de knoppen zijn ingedrukt of welke vorige knoppen waren ingedrukt
        public KeyboardState PrevKeyState {get { return prevKeyState; }set { prevKeyState = value; }}
        public KeyboardState KeyState {get { return keyState; } set { keyState = value; }}

        #endregion

        #region constructor

        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
        }

        #endregion

        #region methods

        #region key press
        /// <summary>
        /// als je een key indrukt Press
        /// </summary>
        
        public bool KeyPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }
        #endregion

        #region keyrelease

        // als je key los laat
        public bool KeyReleased(Keys key)
        {
            if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
        #endregion

        #region keydown 
        // als je key indrukt en ingedrukt houdt
        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
                return true;
            else
                return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
        #endregion

        #endregion
    }
 }


