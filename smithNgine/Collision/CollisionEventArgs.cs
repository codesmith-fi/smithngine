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
