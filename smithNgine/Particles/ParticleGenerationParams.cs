/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Particles
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Codesmith.SmithNgine.MathUtil;

    /// <summary>
    /// Enumeration flags for Emitter modes
    /// </summary>
    [Flags]
    public enum EmitterModes : int
    {
        None = 0,
        // Emitter spawns particles from random position, e.g. CircleEmitter
        RandomPosition   = 1,
        // Movement direction of new particles is random
        RandomDirection  = 1 << 1,
        // Emitter position is absolute instead of relative to the effect position
        PositionAbsolute = 1 << 2,
        // Emitter position is relative to the effect position
        PositionRelative = 1 << 3,
        // Emitter rotation is absolute instead of relative to the effect rotation
        RotationAbsolute = 1 << 4,
        // Emitter rotation is relative to the effect rotation
        RotationRelative = 1 << 5,
        // Emitter uses the given budget only and stops generating stuff
        UseBudgetOnly    = 1 << 6,
        // Emitter generates stuff automatically when ParticlSystem is updated
        AutoGenerate     = 1 << 7 
    }

    /// <summary>
    /// Configuration parameters for ParticleEmitter
    /// </summary>
    public class ParticleGenerationParams
    {
        private Random random;
        private Vector2 speed;
        private Vector2 opacity;
        private Vector2 rotation;
        private Vector2 scale;
        private Vector2 depth;
        private Vector2 quantity;
        private Vector2 angularvelocity;
        private float speedDamping;
        private float speedVariation;
        private float rotationSpeedVariation;
        private float rotationVariation;
        private float opacityVariation;
        private float scaleVariation;
        private Vector2 ttl;
        private List<Texture2D> textures;

        // Initial velocity for new particles, range X=min, Y=max
        public EmitterModes Flags
        {
            get;
            set;
        }

        public int ParticleBudget
        {
            get;
            set;
        }

        /// <summary>
        /// Range for initial speed
        /// Units per second
        /// </summary>
        public Vector2 SpeedRange
        {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// Initial speed variation, between 0.0f and 1.0f, will be clamped
        /// <value>if 0 returns the start of the speed range</value>
        /// </summary>
        public float InitialSpeedVariation
        {
            get { return speedVariation; }
            set { speedVariation = MathHelper.Clamp(value, 0.0f, 1.0f); }
        }

        /// <summary>
        /// Get initial speed for a particle
        /// <see cref="InitialSpeedVariation"/>
        /// <see cref="InitialSpeedRange"/>
        /// </summary>
        public float InitialSpeed
        {
            get
            {
                return (float)MathHelper.Lerp(speed.X, speed.Y,
                    (float)random.NextDouble() * InitialSpeedVariation);
            }
        }

        /// <summary>
        /// Speed damping, how much the particle slows down during update
        /// </summary>
        public float SpeedDamping
        {
            get { return speedDamping; }
            set { speedDamping = value; }
        }

        /// <summary>
        /// Get or set the range for rotation speed, radians per second
        /// </summary>
        public Vector2 AngularVelocityRange
        {
            get { return angularvelocity; }
            set { angularvelocity = value; }
        }

        /// <summary>
        /// Get or set the variation for initial angle
        /// </summary>
        public float InitialAngularVelocityVariation
        {
            get { return rotationSpeedVariation; }
            set { rotationSpeedVariation = MathHelper.Clamp(value, 0.0f, 1.0f); }
        }

        /// <summary>
        /// Get initial rotation speed.
        /// <see cref="AngularVelocityRange"/>
        /// <see cref="InitialAngularVelocityVariation"/>
        /// </summary>
        public float InitialAngularVelocity
        {
            get
            {
                return MathHelper.Lerp(angularvelocity.X, angularvelocity.Y,
                    (float)random.NextDouble() * InitialAngularVelocityVariation);
            }
        }

        // Opacity range for new particles, x=min, y=max
        public Vector2 OpacityRange
        {
            get { return opacity; }
            set { opacity = value; }
        }

        public float InitialOpacityVariation
        {
            get { return opacityVariation; }
            set { opacityVariation = MathHelper.Clamp(value, 0.0f, 1.0f); }
        }

        public float InitialOpacity
        {
            get
            {
                return MathHelper.Lerp(opacity.X, opacity.Y,
                    (float)random.NextDouble() * InitialOpacityVariation);
            }
        }

        public float InitialRotationVariation
        {
            get { return rotationVariation; }
            set { rotationVariation = MathHelper.Clamp(value, 0.0f, 1.0f); }
        }

        // Initial rotation range for new particles, x=min, y=max
        public Vector2 RotationRange
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public float InitialRotation
        {
            get
            {
                return MathHelper.Lerp(rotation.X, rotation.Y,
                    (float)random.NextDouble() * InitialRotationVariation);
            }
        }

        public Vector2 DepthRange
        {
            get { return depth; }
            set { depth = value; }
        }

        public float Depth
        {
            get
            {
                return MathHelper.Lerp(depth.X, depth.Y,
                    (float)random.NextDouble());
            }
        }

        public float InitialScaleVariation
        {
            get { return scaleVariation; }
            set { scaleVariation = MathHelper.Clamp(value, 0.0f, 1.0f); }
        }

        public Vector2 ScaleRange
        {
            get { return scale; }
            set { scale = value; }
        }

        public float InitialScale
        {
            get
            {
                return MathHelper.Lerp(scale.X, scale.Y,
                    (float)random.NextDouble() * InitialScaleVariation);
            }
        }

        public Color ColorRangeStart
        {
            get;
            set;
        }

        public Color ColorRangeEnd
        {
            get;
            set;
        }

        // Color of the created particle
        public Color Color
        {
            get { return Color.Lerp(ColorRangeStart, ColorRangeEnd, (float)random.NextDouble()); }
            set 
            { 
                ColorRangeStart = value;
                ColorRangeEnd = value;
            }
        }

        // range for quantity of particles to create per trigger
        public Vector2 QuantityRange
        {
            get { return quantity; }
            set { quantity = Validators.ValidateRange(value); }
        }

        public int Quantity
        {
            get
            {
                return (int)MathHelper.Lerp(quantity.X, quantity.Y,
                    (float)random.NextDouble());
            }
        }

        public List<Texture2D> Textures
        {
            get { return this.textures; }
            set { this.textures = value; }
        }

        public Texture2D Texture
        {
            get
            {
                return ( textures.Count > 0 ) ? 
                    textures[random.Next(textures.Count)] : null;
            }
        }

        public Vector2 TTLRange
        {
            get { return ttl; }
            set { ttl = Validators.ValidateRange(value); }
        }

        // milliseconds
        public float TTL
        {
            get
            {
                return MathHelper.Lerp(ttl.X, ttl.Y, (float)random.NextDouble());
            }
        }

        public ParticleGenerationParams()
        {
/*
            ep.SpeedRange = new Vector2(30.0f, 30.0f);
            ep.InitialSpeedVariation = 1.0f;
            ep.ScaleRange = new Vector2(0.4f, 1.0f);
            ep.InitialScaleVariation = 1.0f;
            ep.RotationRange = new Vector2(0.0f, MathHelper.TwoPi);
            ep.InitialRotationVariation = 1.0f;
            ep.AngularVelocityRange = new Vector2(-1f, 1.0f);
            ep.InitialAngularVelocityVariation = 1.0f;
            ep.OpacityRange = new Vector2(0.5f, 0.0f);
            ep.InitialOpacityVariation = 1.0f;
            ep.TTLRange = new Vector2(500f, 2000f);         
            ep.SpeedDamping = 1.02f;
*/ 
            random = new Random();
            ParticleBudget = -1;
            SpeedRange = new Vector2(1.0f, 1.0f);
            speedDamping = 1.0f;
            angularvelocity = Vector2.Zero;
            scale = new Vector2(1.0f, 1.0f);
            depth = new Vector2(1.0f, 1.0f);
            InitialSpeedVariation = 0.0f;
            InitialAngularVelocityVariation = 0.0f;
            InitialRotationVariation = 0.0f;
            InitialScaleVariation = 0.0f;
            InitialOpacityVariation = 0.0f;
            ColorRangeEnd = Color.White;
            ColorRangeStart = Color.White;
            rotation = Vector2.Zero;
            opacity = new Vector2(1.0f, 1.0f);
            quantity = Vector2.Zero;
            Color = Color.White;
            textures = new List<Texture2D>();
            ttl = new Vector2(500.0f, 500.0f);
            Flags = EmitterModes.PositionRelative | EmitterModes.RotationRelative;
        }

        public void AddTexture(Texture2D tex)
        {
            textures.Add(tex);
        }
    }
}
