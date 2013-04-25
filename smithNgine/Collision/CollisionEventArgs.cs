/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;

namespace Codesmith.SmithNgine.Collision
{
    public class CollisionEventArgs : EventArgs
    {
        public ICollidableObject SourceObject
        {
            get;
            protected set;
        }

        public ICollidableObject TargetObject
        {
            get;
            protected set;
        }

        public CollisionEventArgs(ICollidableObject source, ICollidableObject target)
        {
            this.SourceObject = source;
            this.TargetObject = target;
        }
    }
}
