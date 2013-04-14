/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.MathUtil;

namespace Codesmith.SmithNgine.Particles
{
    [Flags]
    public enum EmitterCastStyle : int
    {
        None = 0,
        RandomPosition = 1,
        RandomDirection = RandomPosition << 1
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
        private Vector2 ttl;
        private List<Texture2D> textures;

        // Initial velocity for new particles, range X=min, Y=max
        public EmitterCastStyle Flags
        {
            get;
            set;
        }

        public Vector2 InitialSpeedRange
        {
            get { return speed; }
            set { speed = value; }
        }

        public float InitialSpeedVariation
        {
            get;
            set;
        }

        public float InitialSpeed
        {
            get
            {
                return (int)MathHelper.Lerp(speed.X, speed.Y,
                    (float)random.NextDouble() * InitialSpeedVariation);
            }
        }

        public float SpeedDamping
        {
            get { return speedDamping; }
            set { speedDamping = value; }
        }

        // Speed of rotation
        public Vector2 AngularVelocityRange
        {
            get { return angularvelocity; }
            set { angularvelocity = value; }
        }

        public float InitialAngularVelocityVariation
        {
            get;
            set;
        }

        public float InitialAngularVelocity
        {
            get
            {
                return (int)MathHelper.Lerp(angularvelocity.X, angularvelocity.Y,
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
            get;
            set;
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
            get;
            set;
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
            get;
            set;
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


        // Color of the created particle
        public Color Color
        {
            get;
            set;
        }

        // range for quantity of particles to create per trigger
        public Vector2 QuantityRange
        {
            get { return quantity; }
            set { quantity = MathValidator.ValidateRange(value); }
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
            set { ttl = MathValidator.ValidateRange(value); }
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
            random = new Random();
            speed = Vector2.Zero;
            speedDamping = 1.0f;
            angularvelocity = Vector2.Zero;
            scale = new Vector2(1.0f, 1.0f);
            depth = new Vector2(1.0f, 1.0f);
            InitialSpeedVariation = 0.0f;
            InitialAngularVelocityVariation = 0.0f;
            InitialRotationVariation = 0.0f;
            InitialScaleVariation = 0.0f;
            InitialOpacityVariation = 0.0f;
            rotation = Vector2.Zero;
            opacity = new Vector2(1.0f, 1.0f);
            quantity = Vector2.Zero;
            Color = Color.White;
            textures = new List<Texture2D>();
            ttl = new Vector2(500.0f, 500.0f);
            Flags = EmitterCastStyle.RandomDirection | EmitterCastStyle.RandomPosition;
        }

        public void AddTexture(Texture2D tex)
        {
            textures.Add(tex);
        }
    }
}
