/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithNgine.Particles
{
    /// <summary>
    /// Particle
    /// </summary>
    public class Particle
    {
        #region Properties
        public Texture2D Texture
        {
            get;
            set;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public float Speed
        {
            get;
            set;
        }

        public Vector2 Position 
        { 
            get; 
            set; 
        }

        public float VelocityDamping
        {
            get;
            set;
        }

        public Vector2 LinearVelocity 
        { 
            get; 
            set; 
        }

        public float Rotation 
        { 
            get; 
            set; 
        }

        public float AngularVelocity 
        { 
            get; 
            set; 
        }

        public float Opacity
        {
            get;
            set;
        }

        public Color Color 
        { 
            get; 
            set; 
        }

        public float Scale 
        { 
            get; 
            set; 
        }

        public float Depth
        {
            get;
            set;
        }

        public float TTL
        {
            get;
            set;
        }

        public float TTLPercent
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public Particle(Texture2D texture) : this(texture, Vector2.Zero)
        {
        }

        public Particle(Texture2D texture, Vector2 position) : this(texture, position, Vector2.Zero)
        {
        }

        public Particle(ParticleGenerationParams p)
        {
            Texture = p.Texture;
            Color = p.Color;
            Opacity = p.OpacityRange.X;
            Rotation = p.RotationRange.X;
            Scale = p.ScaleRange.X;
            Depth = p.Depth;
            Speed = p.InitialSpeed;
            VelocityDamping = p.SpeedDamping;
            TTLPercent = 0.0f;
            TTL = p.TTL;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            Texture = texture;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Position = position;
            LinearVelocity = velocity;
            VelocityDamping = 1.0f;
            Rotation = 0.0f;
            AngularVelocity = 0.0f;
            Color = Color.White;
            Scale = 1.0f;
            Depth = 0.0f;
            Opacity = 1.0f;
            TTL = 500.0f;
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
    }
}
