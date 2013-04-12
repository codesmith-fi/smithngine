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
        private Line linearvelocity;
        private Vector2 opacity;
        private Vector2 rotation;
        private Vector2 scale;
        private Vector2 quantity;
        private Vector2 angularvelocity;
        private List<Texture2D> textures;

        // Initial velocity for new particles, range X=min, Y=max
        public Line LinearVelocityRange
        {
            get { return linearvelocity; }
            set { linearvelocity = value; }
        }

        public Vector2 LinearVelocity
        {
            get
            {
                return linearvelocity.Lerp((float)random.NextDouble());
            }
        }

        // Speed of rotation
        public Vector2 AngularVelocityRange
        {
            get { return angularvelocity; }
            set { angularvelocity = value; }
        }

        public float AngularVelocity
        {
            get
            {
                return MathHelper.Lerp(angularvelocity.X, angularvelocity.Y, 
                    (float)random.NextDouble());
            }
        }

        // Opacity range for new particles, x=min, y=max
        public Vector2 OpacityRange
        {
            get { return opacity; }
            set { opacity = MathValidator.ValidateRange(value); }
        }

        public float Opacity
        {
            get
            {
                return MathHelper.Lerp(opacity.X, opacity.Y,
                    (float)random.NextDouble());
            }
        }

        // Initial rotation range for new particles, x=min, y=max
        public Vector2 RotationRange
        {
            get { return rotation; }
            set { opacity = value; }
        }

        public float Rotation
        {
            get
            {
                return MathHelper.Lerp(rotation.X, rotation.Y,
                    (float)random.NextDouble());
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

        public ParticleGenerationParams()
        {
            random = new Random();
            linearvelocity = new Line(Vector2.Zero, Vector2.Zero);
            angularvelocity = new Vector2(0.0f, 0.0f);
            scale = new Vector2(1.0f, 1.0f);
            rotation = new Vector2(0.0f, 0.0f);
            opacity = new Vector2(1.0f, 1.0f);
            quantity = new Vector2(0.0f, 0.0f);
            Color = Color.White;
            textures = new List<Texture2D>();
        }

        public void AddTexture(Texture2D tex)
        {
            textures.Add(tex);
        }
    }
}
