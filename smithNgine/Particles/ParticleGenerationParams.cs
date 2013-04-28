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
    [Serializable]
    public class ParticleGenerationParams
    {
        private PseudoRandom random;
        private Vector2 depth;
        private Vector2 quantity;
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

        public ParticleGenerationParams()
        {
            random = new PseudoRandom();
            ParticleBudget = -1;
            depth = new Vector2(1.0f, 1.0f);
            quantity = Vector2.Zero;
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
