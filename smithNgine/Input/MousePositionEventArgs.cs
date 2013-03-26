// ***************************************************************************
// ** SmithNgine.Input.MousePositionEventArgs                               **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

using System;

namespace Codesmith.SmithNgine.Input
{
    public class MousePositionEventArgs : EventArgs
    {
        public int X;
        public int Y;
        public int oldX;
        public int oldY;

        public MousePositionEventArgs(int oldX, int oldY, int newX, int newY)
        {
            this.X = newX;
            this.Y = newY;
            this.oldX = oldX;
            this.oldY = oldY;
        }

        private MousePositionEventArgs() { }
    }
}
