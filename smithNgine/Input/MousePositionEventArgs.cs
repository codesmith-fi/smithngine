// ***************************************************************************
// ** SmithNgine.Input.MousePositionEventArgs                               **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

using System;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.Input
{
    public class MousePositionEventArgs : EventArgs
    {
        public Vector2 oldPosition;
        public Vector2 newPosition;

        public MousePositionEventArgs(int oldX, int oldY, int newX, int newY)
        {
            oldPosition = new Vector2(oldX, newX);
            newPosition = new Vector2(newX, newY);
        }

        private MousePositionEventArgs() { }
    }
}
