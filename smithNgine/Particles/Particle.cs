/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.Particles
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Implements a particle
    /// </summary>
    public class Particle : IDisposable
    {
        #region Fields
        private Texture2D texture;
        #endregion

        #region Properties
        public Texture2D Texture 
        {
            get { return texture; }
            set
            {
                texture = value;
                Origin = (texture != null) ? 
                    ( new Vector2(texture.Width / 2, texture.Height / 2)) 
                    : Vector2.Zero;
            }
        }
        public Vector2 Origin { get; set; }
        public float InitialScale { get; set; }
        public float InitialSpeed { get; set; }
        public float InitialRotation { get; set; }
        public float InitialOpacity { get; set; }
        public float InitialAngularVelocity { get; set; }
        public float InitialDepth { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float AngularVelocity { get; set; }
        public float Opacity { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }
        public float Depth { get; set; }
        public float TTL { get; set; }
        public Vector2 LinearVelocity { get; set; }
        public float TTLPercent { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs the particle from given parameters
        /// 
        /// TODO: This will change a lot in the future. I intend
        /// to make PropertyGenerators to make it easier for extending
        /// this functionality instead of using this huge monolithic
        /// ParticleGenerationParams which contains everything!
        /// 
        /// </summary>
        /// <param name="p"></param>
        public Particle(Texture2D texture)
        {
            Texture = texture;
            Reset();
        }

        #endregion

        #region New methods
        /// <summary>
        /// Draws the particle
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to be used for drawing. Begin must have been called!</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, 
                Color * Opacity, Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }
        #endregion

        public void Dispose()
        {
            Texture.Dispose();
            Texture = null;
        }

        public void Reset()
        {
            InitialSpeed = 0.0f;
            InitialScale = 1.0f;
            InitialOpacity = 1.0f;
            InitialRotation = 0.0f;
            InitialAngularVelocity = 0.0f;
            InitialDepth = 0.0f;
            Depth = 0.0f;
            Color = Color.White;
            Opacity = InitialOpacity;
            Rotation = InitialRotation;
            Scale = InitialScale;
            AngularVelocity = InitialAngularVelocity;
            TTL = 1.0f;
            TTLPercent = 0.0f;
        }
    }
}
