/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.Gfx
{
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
