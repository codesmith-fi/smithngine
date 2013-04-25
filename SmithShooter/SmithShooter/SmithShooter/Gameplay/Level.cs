using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Controllers;
using Codesmith.SmithNgine.Gfx;
using Codesmith.SmithShooter.Entities;

namespace Codesmith.SmithShooter.Gameplay
{
    public class Level
    {
        private PlayerShip player;
        private List<Entity> entities;
        private World physicsWorld;
        private GravityController gravitycontroller;

        public BackgroundLayer EntityLayer
        {
            get;
            set;
        }

        public List<Entity> NewEntities
        {
            get;
            set;
        }

        public World PhysicsWorld
        {
            get { return physicsWorld; }
            private set { physicsWorld = value; }
        }

        public PlayerShip Player
        {
            get { return player; }
            private set { player = value; }
        }

        public Rectangle Bounds
        {
            get;
            set;
        }

        public Level(Rectangle bounds)
        {
            Bounds = bounds;
            NewEntities = new List<Entity>();
            SetupPhysics();
            ResetLevel();
        }

        public void Update(GameTime time)
        {
            float timeStep = time.ElapsedGameTime.Milliseconds * 0.001f;
            physicsWorld.Step(timeStep);
            // Create a list of entities to be destroyed after level update
            // We don't want to modify the contents of entity list directly during foreach
            List<Entity> destroyEntities = new List<Entity>();
            foreach (Entity entity in this.entities)
            {
                if (!entity.UpdateEntity(time))
                {
                    destroyEntities.Add(entity);
                }
            }

            foreach (Entity entity in destroyEntities)
            {
                DestroyEntity(entity);
            }

            foreach (Entity entity in NewEntities)
            {
                AddEntity(entity, EntityLayer);
            }
            NewEntities.Clear();
        }

        public void ResetLevel()
        {
            entities = new List<Entity>();
        }

        public void SetupPhysics()
        {
            FarseerPhysics.Settings.EnableDiagnostics = false;
            FarseerPhysics.Settings.UseFPECollisionCategories = true;
            physicsWorld = new World(Vector2.Zero);
            gravitycontroller = new GravityController(
                WorldConversions.PlanetGravityStrenght, WorldConversions.PlanetGravityRadius, 1);
            gravitycontroller.GravityType = GravityType.DistanceSquared;
            physicsWorld.AddController(gravitycontroller);
        }

        public void SetPlayer(PlayerShip player, BackgroundLayer layer)
        {
            Player = player;
            AddEntity(player, layer);
        }

        public void AddEntity(Entity entity, BackgroundLayer layer, bool isPointGravity = false)
        {
            entity.Level = this;
            this.entities.Add(entity);
            layer.AddSprite(entity);
            if (isPointGravity)
            {
                gravitycontroller.AddBody(entity.Body);
            }

            entity.Body.OnCollision += Body_OnCollision;
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            // Get the collided entities
            Entity entityA = fixtureA.Body.UserData as Entity;
            Entity entityB = fixtureB.Body.UserData as Entity;
            if (entityA == null || entityB == null)
            {
                // fail in debug builds, should not happen ever
                Debug.Assert(entityA != null && entityB != null);
                return false;
            }

            // Allow the entity itself to handle the collision
            return entityA.CollideWith(entityB);
        }

        public void DestroyEntity(Entity entity)
        {
            physicsWorld.RemoveBody(entity.Body);
            this.EntityLayer.RemoveSprite(entity);
            this.entities.Remove(entity);
        }
    }
}
