/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;

namespace Codesmith.SmithNgine.GameState
{
    /// <summary>
    /// EventArgs for State Status changes.
    /// Reports the previous status and new status.
    /// </summary>
    public class GameStatusEventArgs : EventArgs
    {
        public GameStateStatus oldStatus;
        public GameStateStatus newStatus;
        public GameStatusEventArgs(GameStateStatus oldStatus, GameStateStatus newStatus)
        {
            this.oldStatus = oldStatus;
            this.newStatus = newStatus;
        }
    }
}
