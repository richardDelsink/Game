using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyGame
{/// Het aanroepen en beheren van de particle classe
    class ParticleManager
    {
        #region Variables
        
        private Random random;                                              // random emitten van sprites/effect
        private List<Particle> particles;                                   //lijst van particles
        private List<Texture2D> textures;                                   // lijst van 2d textures
 
        #endregion

        #region properties

        public List<Particle> Particles { get { return particles; } }
        public Vector2 EmitterLocation { get; set; }                        // positie waar die komen

        #endregion

        #region constructor

        // de constructor
        public ParticleManager(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            particles = new List<Particle>();
            random = new Random();
        }

        #endregion 

        #region methods

        #region Clear particles
        public void Clear()
         {
             particles.Clear();
            //TODO misschien nog verbeteren zoals langzaam uit beeld verdwijnen inplaats van opeens alles weg
            // als er tijd voor is 
         }
        #endregion

        #region refresh
        // het verversen van de effecten en hoeveel 
        public void Refresh()
        {
            int total = 1;

            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Refresh();
                if (particles[particle].Life <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        #endregion

        #region generate
        // het maken van het effect/sprite
        private Particle GenerateNewParticle()
        {
            // het geven van snelheid/rotatie/scale/kleur en leven
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(1f * (float)(random.NextDouble() * 2 - 1), 1f * (float)(random.NextDouble() * 2 - 1));
            float rotation = 0;
            float rotationVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(238, 210, 154);
            float size = (float)random.NextDouble();
            int life = 10 + random.Next(30);

            return new Particle(texture, position, velocity, rotation, rotationVelocity, color, size, life);
        }

        #endregion

        #region Draw
        // zichtbaar maken voor de gebruiken begin en eind aangeven 

        public void Draw(SpriteBatch spriteBatch)
        { // elke particle tekenen
           
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }

        #endregion
        #endregion
    }
}
