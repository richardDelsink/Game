using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheJourneyGame
{/// Voor deze classe wordt er een particle aangeroepen deze wordt hier gemaakt.
 /// verder gaat wordt deze aangeroepen in de particlemanager classe
    class Particle
    {
        #region properties auto get/set  
        ///data, omdat deze variables steeds opnieuw worden gemaakt gaan we automatic
        ///properties gebruiken. Deze wordt steeds weggegooid en opnieuw gemaakt.
        ///Het is net als tijdelijke data
        ///Daar is deze beter voor dan de normale
        ///De data velden zijn gelijk omgezet naar properties
        ///Ik heb dieper naar get en set gezocht op internet en kwam ik deze zin tegen
        ///"automatic properties are more suited for quickly implementing throwaway objects or temporary data"
        ///"Where you don't need much logic."
        ///Dit past heel goed bij de particle effect want het maakt heel veel sprites aan die een effect geven en 
        ///het is niet ingewikkeld maar er moeten er wel veel komen



        ///Data en automatic properties
        public Texture2D Texture { get; private set; }                      // De texture (sprite, object) dat getekent wordt. Deze moet het effect voorstellen
        public Vector2 Position { get; private set; }                       // De huidige positie van de het effect
        public Vector2 Velocity { get; private set; }                       // De snelheid van het effect van het huidige object
        public float Rotation { get; private set; }                         // De huidige rotatie van het effect
        public float RotationVelocity { get; private set; }                 // de snelheid van de verandering van de rotatie van het huidige object
        public Color Color { get; private set; }                            // De kleur van het effect
        public float Size { get; private set; }                             // De grootte van het effect/sprite
        public int Life { get; private set; }                               // Hoelang het effect bestaat
        #endregion

        #region constructor
        ///De constructor
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float rotation, float rotationVelocity, Color color, float size, int life)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
            RotationVelocity = rotationVelocity;
            Color = color;
            Size = size;
            Life = life;
        }
        #endregion

        #region methods

        ///Verversen van de huidige effect
        public void Refresh()
        {
            Life--;
            Position += Velocity;
            Rotation += RotationVelocity;
        }

        ///Teken van het effect/sprite/object
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                Rotation, origin, Size, SpriteEffects.None, 0f);
        }
        #endregion
    }
}
