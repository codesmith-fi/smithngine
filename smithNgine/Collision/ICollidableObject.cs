/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.Collision
{
    public interface ICollidableObject
    {
        Rectangle CollisionBounds
        {
            get;
        }

        bool CheckCollision(ICollidableObject another);
    }
}
