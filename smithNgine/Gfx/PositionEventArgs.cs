/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using Microsoft.Xna.Framework;

    public class PositionEventArgs : EventArgs
    {
        public Vector2 oldPosition = Vector2.Zero;
        public Vector2 newPosition = Vector2.Zero;
        public PositionEventArgs(Vector2 oldPosition, Vector2 newPosition)
        {
            this.oldPosition = oldPosition;
            this.newPosition = newPosition;
        }
    }
}
