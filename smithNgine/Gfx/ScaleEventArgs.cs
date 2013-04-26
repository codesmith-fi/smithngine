/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    using System;

    public class ScaleEventArgs : EventArgs
    {
        public float oldScale = 0.0f;
        public float newScale = 0.0f;
        public ScaleEventArgs(float oldValue, float newValue)
        {
            this.oldScale = oldValue;
            this.newScale = newValue;
        }
    }
}
