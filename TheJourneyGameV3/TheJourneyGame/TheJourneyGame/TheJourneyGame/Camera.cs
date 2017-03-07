using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheJourneyGame
{
    class Camera
    {
        /// <summary>
        /// Deze classe zorgt ervoor dat er een camere wordt gemaakt en dat wij deze camera bepaalde methodes kunnen meegeven en aanroepen
        /// </summary>

        #region Variables

        private static Camera instance;
        private Vector2 position;
        private Matrix viewMatrix;

        // de matrix wordt hier gebruikt omdat XNA automatisch een 3 dimensionale camere gebruikt waarbij je de x,y en z as moet aangeven
        // voor deze game hebben wij alleen de x en y as nodig dus de z kunnen we al op 0 zetten
        #endregion

        #region Properties
        // als er iets gebeurd in de getter of setter dan hou ik ervan om het overzichtelijk te houden
        // anders doe ik normaal {get{return ......;}} achter elkaar
        public Matrix ViewMatrix { get { return viewMatrix; } }

        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }
                return instance;
            }

        }

        #endregion

        #region Method
        // zoals eerder gezegt willen we alleen de x en y weten en deze wordt hier bepaald
        // omdat we alleen de op de x as willen scrollen moeten het midden bepalen 

        #region Point of interest
        public void SetFocalPoint(Vector2 focalPosition)
        {
            position = new Vector2(focalPosition.X - ScreenManager.Instance.Window.X / 2,
                focalPosition.Y - ScreenManager.Instance.Window.Y / 2);

            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
            }

         
          
        }

        #endregion

        #region Update
        // dit stukje code hoeft niet perse in een methode Update te staan deze kan ook in de vorige methode staan 
        // maar ik vind dit wat overzichtelijker, hier word de z as op 0 gezet en als de karakter in het midden is scrolled het naar rechts
        public void Update()
        {
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }

        #endregion

        #endregion
    }
}
