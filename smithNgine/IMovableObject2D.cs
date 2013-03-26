// ***************************************************************************
// ** SmithNgine.IMovableObject2D                                           **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
using System;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine
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
