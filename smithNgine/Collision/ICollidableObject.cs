// ***************************************************************************
// ** SmithNgine.Collision.ICollidableObject                                **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************
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
