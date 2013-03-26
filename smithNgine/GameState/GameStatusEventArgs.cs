// ***************************************************************************
// ** SmithNgine.GameState.GameStatusEventArgs                              **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
using System;

namespace Codesmith.SmithNgine.GameState
{
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
