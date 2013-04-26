/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using Microsoft.Xna.Framework;

    public class MenuEntryEventArgs : EventArgs
    {
        public PlayerIndex? ControllingPlayer
        {
            get;
            internal set;
        }

        public MenuEntryEventArgs(PlayerIndex? index)
        {
            ControllingPlayer = index;
        }
    }
}
