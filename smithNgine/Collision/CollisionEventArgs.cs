// ***************************************************************************
// ** SmithNgine.Collision.CollisionEventArgs                               **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
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

        public List<ICollidableObject> CollidedObjects
        {
            get;
            protected set;
        }

        public CollisionEventArgs(ICollidableObject source, List<ICollidableObject> objects)
        {
            this.SourceObject = source;
            this.CollidedObjects = objects;
        }
    }
}
