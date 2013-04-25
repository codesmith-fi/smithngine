using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.Gfx;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Codesmith.SmithShooter.Gameplay;

namespace Codesmith.SmithShooter.Entities
{
    public abstract class Entity : Sprite
    {
        protected Body entityBody;
        protected World entityWorld;
        protected Level leven;

        public Level Level
        {
            get;
            set;
        }

        public Entity Owner
        {
            get;
            set;
        }

        public Body Body
        {
            get { return entityBody; }
            private set { entityBody = value; }
        }

        public World World
        {
            get { return entityWorld; }
            protected set { entityWorld = value; }
        }

        public Rectangle LevelBounds
        {
            set;
            get;
        }

        public float MaxAngularVelocity { get; set; }
        public float MaxLinearVelocity { get; set; }

        public Vector2 BodyPosition
        {
            get { return Body.Position; }
            set { Body.Position = value; }
        }

        public new Vector2 Position
        {
            get { return WorldConversions.ConvertFromWorld(entityBody.Position); }
            set 
            {
                entityBody.Position = WorldConversions.ConvertFromPixels(value);
            }
        }

        public new float Rotation
        {
            get { return entityBody.Rotation; }
            set 
            {
                entityBody.Rotation = value;
            }
        }

        public float Velocity
        {
            get
            {
                return Body.LinearVelocity.Length();
            }
        }

        public float TorqueForce
        {
            get;
            set;
        }

        public float Damage
        {
            get;
            set;
        }
  
        public Entity(Texture2D tex, Rectangle bounds, World world, Body body = null, float density = 10.0f)
            : base(tex)
        {
            entityWorld = world;
            if (body == null)
            {
                entityBody = BodyFactory.CreateCircle(world,
                    WorldConversions.ConvertFromPixels(tex.Width / 2), density);
            }
            else
            {
                entityBody = body;
            }
            // Set the Entity instance as userdata so we can reference it later 
            // e.g. in collisions
            Body.UserData = this;
            entityBody.BodyType = BodyType.Dynamic;
            entityBody.AngularDamping = 0.1f;
            float mass = entityBody.Mass;
            MaxAngularVelocity = 2.0f;
            LevelBounds = bounds;
            TorqueForce = 30.0f;
            Damage = 0f;
        }

        /// <summary>
        /// Entity will take damage through this method
        /// </summary>
        /// <param name="amount">Amount of damage to do</param>
        /// <returns><value>false</value> if damage destroyed the entity</returns>
        public virtual bool TakeDamage(float amount)
        {
            return true;
        }

        /// <summary>
        /// Handle collision with another entity
        /// </summary>
        /// <param name="other">The other entity which collides</param>
        /// <returns><value>true</value> if collision was handled</returns>
        public virtual bool CollideWith(Entity other)
        {
            return false;
        }

        public virtual bool UpdateEntity(GameTime time)
        {
            base.Position = WorldConversions.ConvertFromWorld(entityBody.Position);
            base.Rotation = entityBody.Rotation;

            // Reset the velocity damping
            if (Velocity < MaxLinearVelocity)
            {
                Body.LinearDamping = 0;
            }

            return true;
        }
    }
}
