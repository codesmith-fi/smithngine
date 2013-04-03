// ***************************************************************************
// ** SmithNgine.RotationEventArgs                                          **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
using System;

namespace Codesmith.SmithNgine.General
{
    public class RotationEventArgs : EventArgs
    {
        public float oldRotation = 0.0f;
        public float rotation = 0.0f;
        public RotationEventArgs(float oldRotation, float newRotation)
        {
            this.oldRotation = oldRotation;
            this.rotation = newRotation;
        }
    }
}
