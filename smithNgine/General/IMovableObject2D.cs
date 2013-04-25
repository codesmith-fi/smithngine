﻿/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;
using Codesmith.SmithNgine.Gfx;

namespace Codesmith.SmithNgine.General
{
    public interface IMovableObject2D
    {
        // Movable 2D object has a position
        Vector2 Position
        {
            get;
            set;
        }

        // Event triggered when the position changes
        event EventHandler<PositionEventArgs> PositionChanged;
    }
}
