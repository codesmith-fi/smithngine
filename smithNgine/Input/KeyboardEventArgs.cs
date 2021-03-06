﻿/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Input
{
    using System;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework;

    public class KeyboardEventArgs : EventArgs
    {
        // Array of pressed keys
        public Keys[] keys;
        public PlayerIndex? player;

        public KeyboardEventArgs(Keys[] keys, PlayerIndex? player)
        {
            this.keys = keys;
            this.player = player;
        }
    }
}
