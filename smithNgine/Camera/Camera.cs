/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithNgine.Camera
{
    /// <summary>
    /// Camera with Rotation, Scale and Parallax
    /// </summary>
    public class Camera : SimpleCamera
    {
        #region Fields
        // Viewport rectangle
        private Rectangle viewPortRect;
        // View matrix 
        private Matrix viewMatrix;
        #endregion

        #region Properties
        public Vector2 Origin
        {
            get;
            set;
        }

        /// <summary>
        /// Zoom factor of the camera
        /// <value>1.0 is normal zoom, smaller values zoom out</value>
        /// </summary>
        public float Zoom
        {
            get;
            set;
        }

        /// <summary>
        /// Rotation of the camera
        /// <value>Angle in radians</value>
        /// </summary>
        public float Rotation
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Sets camera position and the viewport size
        /// </summary>
        /// <param name="position">Position of the camera</param>
        /// <param name="viewPort">Viewport rectangle(size)</param>
        public Camera(Vector2 position, Rectangle viewPort) 
            : base(position)
        {
            // Set origin to the center of the viewport
            Origin = new Vector2(viewPort.Width * 0.5f, viewPort.Height * 0.5f);
            Zoom = 1.0f;
            Rotation = 0.0f;
            viewPortRect = viewPort;
        }
        #endregion

        #region New methods
        /// <summary>
        /// Get view matrix for rendering 2d stuff
        /// Origin of the translation is the center point of the viewport
        /// </summary>
        /// <param name="parallax">Parallax effect value</param>
        /// <returns>2D view matrix</returns>
        public Matrix GetViewMatrix(Vector2 parallax)
        {
            viewMatrix = Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                   // Rotation translation
                   Matrix.CreateRotationZ(Rotation) *
                   // Scaling translation
                   Matrix.CreateScale(Zoom, Zoom, 1) *
                   // Translate around center of the viewport
                   Matrix.CreateTranslation(
                    new Vector3(Origin, 0.0f));
            return viewMatrix;
        }

        /// <summary>
        /// Get view matrix from the point of given origin. 
        /// Rotation point is the given origin.
        /// </summary>
        /// <param name="parallax">Parallax effect value</param>
        /// <param name="origin">Origin of translations</param>
        /// <returns></returns>
        public Matrix GetViewMatrix(Vector2 parallax, Vector2 origin)
        {
            viewMatrix = Matrix.CreateTranslation(
                new Vector3(-Position * parallax, 0.0f)) *
                    Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                    // Rotation translation
                    Matrix.CreateRotationZ(Rotation) *
                    // Scaling translation
                    Matrix.CreateScale(Zoom, Zoom, 1) *
                    Matrix.CreateTranslation(new Vector3(origin, 0.0f));
            return viewMatrix;
        }
        #endregion
    }
}

