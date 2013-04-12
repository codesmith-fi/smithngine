using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using Codesmith.SmithNgine.MathUtil;
using Codesmith.SmithNgine.Primitives;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithNgine.Particles
{
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
        private Vector2 quantity;
        private Vector2 angularvelocity;
        private float speedDamping;
        private Vector2 ttl;
        private List<Texture2D> textures;

        // Initial velocity for new particles, range X=min, Y=max

        public Vector2 InitialSpeedRange
        {
            get { return speed; }
            set { speed = value; }
        }

        public float InitialSpeed
        {
            get
            {
                return (int)MathHelper.Lerp(speed.X, speed.Y,
                    (float)random.NextDouble());
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

        // Opacity range for new particles, x=min, y=max
        public Vector2 OpacityRange
        {
            get { return opacity; }
            set { opacity = value; }
        }

        // Initial rotation range for new particles, x=min, y=max
        public Vector2 RotationRange
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 ScaleRange
        {
            get { return scale; }
            set { scale = value; }
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
            rotation = Vector2.Zero;
            opacity = new Vector2(1.0f, 1.0f);
            quantity = Vector2.Zero;
            Color = Color.White;
            textures = new List<Texture2D>();
            ttl = new Vector2(500.0f, 500.0f);
        }

        public void AddTexture(Texture2D tex)
        {
            textures.Add(tex);
        }
    }
}
