﻿/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

//
// TODO! This is a work in progress and mostly not even needed at the moment
// because of Farseer physics.
//
// No object currently implements ICollidable!!!
//

namespace Codesmith.SmithNgine.Collision
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Codesmith.SmithNgine.General;

    public class CollisionManager : GameObjectBase
    {
        private List<ICollidableObject> objects = new List<ICollidableObject>();
        private List<ICollidableObject> collidingObjects = new List<ICollidableObject>();
        public event EventHandler<CollisionEventArgs> ObjectsCollided;

        public CollisionManager()
        {
            
        }

        public void AddCollidable(ICollidableObject collidable)
        {
            if (!objects.Contains(collidable))
            {
                this.objects.Add(collidable);
            }
        }

        public void RemoveCollidable(ICollidableObject collidable)
        {
            objects.Remove(collidable);
            collidingObjects.Remove(collidable);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for(int i=0; i<objects.Count; ++i)
            {
                ICollidableObject obj1 = objects[i];
                for( int j=i+1; j<objects.Count; j++)
                {
                    ICollidableObject obj2 = objects[j];
                    if (obj1.CheckCollision(obj2))
                    {
                        if (!collidingObjects.Contains(obj1))
                        {
                            collidingObjects.Add(obj1);
                            collidingObjects.Add(obj2);

                            if (this.ObjectsCollided != null)
                            {
                                CollisionEventArgs args = new CollisionEventArgs(obj1, obj2);
                                this.ObjectsCollided(this, args);
                            }
                        }

                    }
                    else
                    {
                        collidingObjects.Remove(obj1);
                        collidingObjects.Remove(obj2);
                    }

                }
            }
        }
    }
}
