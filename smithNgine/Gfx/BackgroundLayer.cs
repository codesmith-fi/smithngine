/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Codesmith.SmithNgine.Particles;
using Codesmith.SmithNgine.View;
using System.Diagnostics;

namespace Codesmith.SmithNgine.Gfx
{
    /// <summary>
    /// Implements one layer in the Background system.
    /// Each layer can have a separate parallax setting which controls
    /// the relative movement of the layer when camera moves.
    /// Each layer can contain Sprites and ParticleEffects
    /// 
    /// Layer draws sprites first (in order of addition) then
    /// Particle Effects (in order of addition).
    /// So depth of the sprites and effects are not obeyed at the moment,
    /// this is a TODO-item.
    /// 
    /// Parallax factor controls how this layer moves when camera moves.
    /// 1.0 is normal movement, 0.5 is half the movement of camera, 2.0 is double speed.
    /// </summary>
    public class BackgroundLayer
    {
        #region Fields
        // List of sprites contained in this layer
        private List<Sprite> sprites = new List<Sprite>();
        // List of Particle effects contained in this layer
        private List<ParticleEffect> effects = new List<ParticleEffect>();
        // Parallax factor of this layer
        private Vector2 parallax = Vector2.Zero;
        // Camera which is used on this layer
        private Camera camera;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a Backgroundlayer and sets camera and the parallax factor
        /// </summary>
        /// <param name="cam">Camera</param>
        /// <param name="prlx">Parallax factor (Vector2). X and Y parallax in vector</param>
        public BackgroundLayer(Camera cam, Vector2 prlx)
        {
            parallax = prlx;
            camera = cam;
        }
        #endregion

        #region New methods
        /// <summary>
        /// Clear the layer, removes sprites and particle effects from the 
        /// layer
        /// </summary>
        public void Clear()
        {
            sprites.Clear();
            effects.Clear();
        }

        /// <summary>
        /// Add a particle effect to this layer
        /// Effects are drawn after sprites are drawn
        /// </summary>
        /// <remarks>
        /// A ParticleEffect instance can be added to one layer only 
        /// once.
        /// </remarks>
        /// <param name="effect">Particle effect to be displayed on this layer</param>
        public void AddParticleEffect(ParticleEffect effect)
        {
            Debug.Assert(!effects.Contains(effect), "ParticleEffect already exists on this layer");
            effects.Add(effect);
        }

        /// <summary>
        /// Add a Sprite to this layer.
        /// Sprites are drawn first in the order of addition at the moment
        /// </summary>
        /// <remarks>
        /// A Sprite instance can be added to one layer only 
        /// once.
        /// </remarks>
        /// <param name="sprite"></param>
        public void AddSprite(Sprite sprite)
        {
            Debug.Assert(!sprites.Contains(sprite), "Sprite already exists on this layer");
            sprites.Add(sprite);
        }

        /// <summary>
        /// Remove a ParticleEffect from this layer
        /// </summary>
        /// <param name="effect">ParticleEffect to be removed</param>
        public void RemoveEffect(ParticleEffect effect)
        {
            effects.Remove(effect);
        }

        /// <summary>
        /// Remove a Sprite from this layer
        /// </summary>
        /// <param name="sprite">Sprite to be removed</param>
        public void RemoveSprite(Sprite sprite)
        {
            sprites.Remove(sprite);
        }

        /// <summary>
        /// Draws this layer.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to be used for drawing. 
        /// Begin() will be called by this method.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                null, null, null, null, null, camera.GetViewMatrix(parallax));
            foreach (Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                null, null, null, null, camera.GetViewMatrix(parallax)); ;
            foreach (ParticleEffect eff in effects)
            {
                eff.Draw(spriteBatch, true);
            }
            spriteBatch.End();

        }
        #endregion
    }
}
