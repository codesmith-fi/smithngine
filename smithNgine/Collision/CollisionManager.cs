using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.Collision
{
    public class CollisionManager : ObjectBase
    {
        private List<ICollidableObject> objects = new List<ICollidableObject>();
        public event EventHandler<CollisionEventArgs> ObjectsCollided;

        public CollisionManager()
        {
        }

        public void AddCollidable(ICollidableObject collidable)
        {
            this.objects.Add(collidable);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach(ICollidableObject obj1 in this.objects)
            {
                List<ICollidableObject> targets = new List<ICollidableObject>();
                foreach (ICollidableObject obj2 in this.objects)
                {
                    if (obj1 == obj2)
                    {
                        continue;
                    }

                    if (obj1.CheckCollision(obj2))
                    {                        
                        targets.Add(obj2);
                    }
                }

                if (this.ObjectsCollided != null && targets.Count > 0)
                {
                    CollisionEventArgs args = new CollisionEventArgs(obj1, targets);
                    this.ObjectsCollided(this, args);
                }
            }
        }
    }
}
