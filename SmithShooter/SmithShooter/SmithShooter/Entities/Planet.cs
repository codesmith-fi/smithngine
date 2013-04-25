using Codesmith.SmithShooter.Gameplay;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithShooter.Entities
{
    class Planet : Entity
    {
        public float PlanetRotation
        {
            get;
            set;
        }

        public Planet(Texture2D texture, Rectangle bounds, World world)
            : base(texture, bounds, world, null, WorldConversions.PlanetDensity)
        {
            Body.IsStatic = true;
            Body.BodyType = BodyType.Static;
//            Body.Mass = 10000;
            PlanetRotation = 0.001f;
            Damage = WorldConversions.PlanetCollisionDamage;
        }

        public override bool CollideWith(Entity other)
        {
            return other.CollideWith(this);
        }

        public override bool UpdateEntity(GameTime time)
        {
            this.Body.Rotation += PlanetRotation;
            return base.UpdateEntity(time);
        }
    }
}
