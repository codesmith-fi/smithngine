﻿/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.General
{
    using System;

    public interface IHoverableObject
    {
        bool IsHovered
        {
            get;
        }

        event EventHandler<HoverEventArgs> BeingHovered;
    }
}
