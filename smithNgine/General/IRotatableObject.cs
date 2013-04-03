// ***************************************************************************
// ** SmithNgine.IRotatableObject                                           **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
using System;

namespace Codesmith.SmithNgine.General
{
    interface IRotatableObject
    {
        float Rotation
        {
            get;
            set;
        }

        event EventHandler<RotationEventArgs> RotationChanged;
    }
}
