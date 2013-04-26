/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.General
{
    using System;

    public interface IFocusableObject
    {
        bool HasFocus
        {
            get;
        }

        void GainFocus();
        void LooseFocus();

        event EventHandler<EventArgs> FocusGained;
        event EventHandler<EventArgs> FocusLost;
    }
}
