/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Collision
{
    using Microsoft.Xna.Framework;

    public interface ICollidableObject
    {
        Rectangle CollisionBounds
        {
            get;
        }

        bool CheckCollision(ICollidableObject another);
    }
}
