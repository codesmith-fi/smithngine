/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.General
{
    using System;
    using Microsoft.Xna.Framework;

    public class HoverEventArgs : EventArgs
    {
        public Vector2 position;

        public HoverEventArgs()
        {
        }
    }
}
