using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithShooter.Entities.AI;

namespace Codesmith.SmithShooter.Entities
{
    public class EnemyShip : Ship
    {
        public EnemyShip(Texture2D tex, Rectangle bounds, World world, Body body)
            : base(tex, bounds, world, body)
        {
            AI = new FollowingAI();
            Body.AngularDamping = 0.5f;
            Body.UserData = this;
        }

        public override bool UpdateEntity(GameTime time)
        {
            AI.UpdateAI(time);
            return base.UpdateEntity(time);
        }

        public override bool CollideWith(Entity other)
        {
            PlayerShip ps = other as PlayerShip;
            if (ps != null)
            {
                AI.Hate(ps, 1f);
                return true;
            }
            return base.CollideWith(other);
        }
    }
}
