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
