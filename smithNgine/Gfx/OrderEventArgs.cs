/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;

namespace Codesmith.SmithNgine.Gfx
{
    public class OrderEventArgs : EventArgs
    {
        public float order = 0.0f;
        public float oldOrder = 0.0f;
        public OrderEventArgs(float oldOrder, float newOrder)
        {
            this.order = newOrder;
            this.oldOrder = oldOrder;
        }
    }
}
