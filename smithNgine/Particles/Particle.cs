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
        public Texture2D Texture { get; set; }
        public Vector2 Origin { get; set; }
        public float InitialScale { get; set; }
        public float InitialSpeed { get; set; }
        public float InitialRotation { get; set; }
        public float InitialOpacity { get; set; }
        public float InitialAngularVelocity { get; set; }
        public float Speed { get; set; }
        public float SpeedDamping { get; set; }
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
        public Particle(Texture2D texture)
        {
            Texture = texture;
        }

        public Particle(ParticleGenerationParams p)
        {
            InitialOpacity = p.InitialOpacity;
            InitialRotation = p.InitialRotation;
            InitialScale = p.InitialScale;
            InitialSpeed = p.InitialSpeed;
            InitialAngularVelocity = p.InitialAngularVelocity;
            Texture = p.Texture;
            Color = p.Color;
            Opacity = InitialOpacity;
            Rotation = InitialOpacity;
            Speed = InitialSpeed;
            Scale = InitialScale;
            AngularVelocity = p.InitialAngularVelocity;
            Depth = p.Depth;
            SpeedDamping = p.SpeedDamping;
            TTLPercent = 0.0f;
            TTL = p.TTL;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
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
