/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Gfx
{
    /// <summary>
    /// EventArgs for reporting Drag delta position
    /// Used by e.g. Sprite
    /// </summary>
    public class DragEventArgs : EventArgs
    {
        /// <summary>
        /// Get the delta position vector of movement during drag
        /// </summary>
        public Vector2 PositionDelta
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="delta">Delta position vector</param>
        public DragEventArgs(Vector2 delta)
        {
            PositionDelta = delta;
        }
    }
}
