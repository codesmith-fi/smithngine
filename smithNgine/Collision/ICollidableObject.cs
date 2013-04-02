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
