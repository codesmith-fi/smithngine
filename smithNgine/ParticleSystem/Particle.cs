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

        public Vector2 Position 
        { 
            get; 
            set; 
        }
        
        public Vector2 Velocity 
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

        public TimeSpan TimeToLive
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

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            Texture = texture;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Position = position;
            Velocity = velocity;
            Rotation = 0.0f;
            AngularVelocity = 0.0f;
            Color = Color.White;
            Scale = 1.0f;
            Depth = 0.0f;
            TimeToLive = TimeSpan.FromSeconds(5.0f);
        }
        #endregion

        #region New methods
        /// <summary>
        /// Update the particle, move, rotate etc. 
        /// </summary>
        /// <param name="gameTime">Current GameTime</param>
        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity;
            Rotation += AngularVelocity;
        }

        /// <summary>
        /// Draws the particle
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to be used for drawing. Begin must have been called!</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }
        #endregion
    }
}
