/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    #region Using statements
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Codesmith.SmithNgine.View;
    #endregion

    /// <summary>
    /// Class implements a multi-layer background. Each layer can 
    /// have a parallax value which effects how much the layer moves.
    /// </summary>
    public class Background
    {
        #region Fields
        // Layers managed by this background system
        private List<BackgroundLayer> layers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Camera to be used
        /// </summary>
        public Camera Camera
        {
            get;
            set;
        }

        /// <summary>
        /// Get the count of layers managed by this system
        /// <value>(int)Layer count</value>
        /// </summary>
        public int Count
        {
            get { return layers.Count; }
        }

        /// <summary>
        /// Get a list of layers contained by this system
        /// </summary>
        public IList<BackgroundLayer> Layers
        {
            get { return layers.AsReadOnly(); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a Background and set the camera instance to be used
        /// </summary>
        /// <param name="cam">Camera to be used by the Background system</param>
        public Background( Camera cam )
        {
            layers = new List<BackgroundLayer>();
            Camera = cam;
        }
        #endregion

        #region New methods
        /// <summary>
        /// Add an empty layer with the given parallax
        /// </summary>
        /// <param name="parallax">Parallax factor, 1.0 = normal, 
        /// 2.0 = twice, 0.0 = no movement </param>
        /// <returns>Return the layer which was added to this Background system</returns>
        public BackgroundLayer AddLayer(Vector2 parallax)
        {
            BackgroundLayer layer = new BackgroundLayer(Camera, parallax); 
            layers.Add(layer);
            return layer;
        }

        /// <summary>
        /// Add layer, set parallax factor for it and add a list of sprites to the layer
        /// </summary>
        /// <param name="sprites">List of sprites to be added to the layer</param>
        /// <param name="parallax">Parallax factor for this layer</param>
        /// <returns>Return the layer which was added to this Background system</returns>
        public BackgroundLayer AddLayer(List<Sprite> sprites, Vector2 parallax)
        {
            BackgroundLayer layer = new BackgroundLayer(Camera, parallax);
            foreach (Sprite s in sprites)
            {
                layer.AddSprite(s);
            }
            layers.Add(layer);
            return layer;
        }

        /// <summary>
        /// Add layer, set parallax factor for it and add one Sprites to the layer
        /// </summary>
        /// <param name="sprite">Sprite to be added to the layer</param>
        /// <param name="parallax">Parallax factor for this layer</param>
        /// <returns>Return the layer which was added to this Background system</returns>
        public BackgroundLayer AddLayer(Sprite sprite, Vector2 parallax)
        {
            BackgroundLayer layer = new BackgroundLayer(Camera, parallax);
            layers.Add(layer);
            if (sprite != null)
            {
                layer.AddSprite(sprite);
            }
            return layer;
        }

        /// <summary>
        /// Get one layer from the system
        /// </summary>
        /// <param name="i">Index of the layer to be retrieved</param>
        /// <returns>Return the Layer with given index</returns>
        /// <exception cref="ArgumentOutOfRangeException">If index is out of range</exception>
        public BackgroundLayer GetLayer(int i)
        {
            if (i < 0 || i >= layers.Count)
            {
                throw new ArgumentOutOfRangeException("No such layer");
            }
            return layers[i];
        }

        /// <summary>
        /// Draw the Background system with the given SpriteBatch
        /// </summary>
        /// <remarks>
        /// Begin must not have been called before calling this Draw!
        /// </remarks>
        /// <param name="spriteBatch">SpriteBatch to be used, Begin() will be called</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BackgroundLayer layer in layers)
            {
                layer.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
