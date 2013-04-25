/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Codesmith.SmithNgine.Gfx;

namespace Codesmith.SmithNgine.General
{
    public interface IOrderableObject
    {
        // Drawing order
        float Order
        {
            get;
            set;
        }

        // Event triggered when the order changes
        event EventHandler<OrderEventArgs> OrderChanged;
    }
}
