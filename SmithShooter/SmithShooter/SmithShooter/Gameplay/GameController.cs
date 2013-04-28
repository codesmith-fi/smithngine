using Codesmith.SmithNgine.Gfx;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.MathUtil;
using Codesmith.SmithNgine.Particles;
using Codesmith.SmithNgine.Particles.Generators;
using Codesmith.SmithNgine.Particles.Modifiers;
using Codesmith.SmithNgine.View;
using Codesmith.SmithShooter.Entities;
using Codesmith.SmithShooter.Items;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Common.Decomposition;

namespace Codesmith.SmithShooter.Gameplay
{
    public class GameController
    {
        private Texture2D shipTexture;
        private Texture2D bulletTexture;
        private ParticleSystem particleSystem;
        private ParticleEffect shipEffect;
        private ParticleEffect shipEffect2;
        private Random random;
        private SoundEffect soundHit;
        public EnemyShip CurrentEnemy {get;set;}

        private ContentManager Content
        {
            get;
            set;
        }

        public Level CurrentLevel
        {
            get;
            set;
        }

        public Background Layers
        {
            get;
            set;
        }

        public BackgroundLayer EntityLayer
        {
            get;
            set;
        }
        
        public Camera PlayCamera
        {
            get;
            set;
        }

        public PlayerIndex? ControllingPlayer
        {
            set;
            get;
        }

        public GameController(ContentManager content, Camera camera)
        {
            Content = content;
            PlayCamera = camera;
            random = new Random();

            PlayCamera.Scale = 0.5f;
        }

        public void InitializeLevel( Rectangle bounds )
        {          
            CurrentLevel = new Level(bounds);
            shipTexture = Content.Load<Texture2D>("Images/ship2");
            bulletTexture = Content.Load<Texture2D>("Images/bullet1");

            SetupParticleSystem();

            Layers = new Background(PlayCamera);
            CreateBackground(Layers, 5);

            // Setup player
            EntityLayer = Layers.AddLayer(new Vector2(1.0f, 1.0f));
            CurrentLevel.EntityLayer = EntityLayer;
            AddPlanets(EntityLayer);

            CurrentLevel.SetPlayer(CreatePlayer(shipTexture), EntityLayer);
            soundHit = Content.Load<SoundEffect>("Samples/shot");

            ResetGame();
            EntityLayer.AddParticleEffect(shipEffect);
            EntityLayer.AddParticleEffect(shipEffect2);
        }

        private PlayerShip CreatePlayer(Texture2D shipTexture)
        {
            Body body = BodyFromTexture(shipTexture, WorldConversions.ShipDensity);
            PlayerShip ship = new PlayerShip(shipTexture, CurrentLevel.Bounds, 
                CurrentLevel.PhysicsWorld, body );

            Weapon weapon = new Weapon("Uberweapon", 
                Content.Load<SoundEffect>("Samples/laser"),
                bulletTexture);
            ship.Weapon = weapon;
            ship.RearThruster = new Engine("Thuster MkI", EngineDirection.Rear);
            ship.FrontThruster = new Engine("Thruster MkI", EngineDirection.Front);
            ship.RearThruster.Effect = shipEffect;
            ship.FrontThruster.Effect = shipEffect2;

            ship.Position = Vector2.Zero;
            return ship;
        }

        public void Update(GameTime gameTime)
        {
            particleSystem.Update(gameTime);
            CurrentLevel.Update(gameTime);

            PlayCamera.Position = CurrentLevel.Player.Position;
            PlayCamera.Rotation = -CurrentLevel.Player.Rotation;
//            PlayCamera.Zoom = 1.0f - ( CurrentLevel.Player.Velocity / CurrentLevel.Player.MaxVelocity * 0.2f );
        }

        public void ResetGame()
        {
            Body body = BodyFromTexture(shipTexture, WorldConversions.ShipDensity);
            CurrentEnemy = new EnemyShip(shipTexture, CurrentLevel.Bounds, 
                CurrentLevel.PhysicsWorld, body);
            CurrentEnemy.Body.IsStatic = false;
            CurrentEnemy.Position = new Vector2(0, 200);
            CurrentEnemy.Color = Color.Blue;
            CurrentEnemy.Rotation = (float)Math.PI/2;
//            CurrentEnemy.AI.Hate(CurrentLevel.Player, 0.1f);
            CurrentEnemy.RearThruster = new Engine("Thuster MkI", EngineDirection.Rear);
            CurrentEnemy.Weapon = new Weapon("Uberweapon",
                Content.Load<SoundEffect>("Samples/laser"),
                bulletTexture);

            CurrentLevel.AddEntity(CurrentEnemy, EntityLayer, false);
        }

        private void AddPlanets(BackgroundLayer layer)
        {
            List<Texture2D> planets = new List<Texture2D>();
            planets.Add(Content.Load<Texture2D>("Images/sun"));
            planets.Add(Content.Load<Texture2D>("Images/earth"));
            planets.Add(Content.Load<Texture2D>("Images/jupiter"));

            Random r = new Random();
            Rectangle areaSize = CurrentLevel.Bounds;
            List<Entity> testList = new List<Entity>();
            const float minDistance = 1500;
            for (int i = 0; i < 20; i++)
            {
                Planet planet = new Planet(planets[r.Next(planets.Count)], areaSize, CurrentLevel.PhysicsWorld);
                bool found = false;
                while (!found)
                {
                    planet.Position = Interpolations.GetRandomRectanglePoint(areaSize, r);
                    int tooClose = 0;
                    foreach (Entity e in testList)
                    {
                        if(Vector2.Distance(planet.Position, e.Position) < minDistance)
                        {
                            tooClose++;
                        }
                    }

                    found = (tooClose == 0);
                }
                planet.Scale = 2.0f;
                planet.Rotation = (float)r.NextDouble() * MathHelper.PiOver2;
                planet.PlanetRotation = (float)r.NextDouble() * 0.0005f;
                CurrentLevel.AddEntity(planet, layer, true);
                testList.Add(planet);            
            }
        }

        private void CreateBackground(Background layers, int layerCount)
        {
            Sprite stars = new Sprite(Content.Load<Texture2D>("Images/nebula1"));
            stars.Color = Color.White * 0.2f;
            stars.Scale = 3.0f;
            layers.AddLayer(stars, new Vector2(0.02f, 0.02f));

            List<Texture2D> nebulas = new List<Texture2D>();
            nebulas.Add(Content.Load<Texture2D>("Images/nebula1"));
            nebulas.Add(Content.Load<Texture2D>("Images/nebula2"));

            Random r = new Random();
            float parallax = 1.0f;
            Rectangle areaSize = CurrentLevel.Bounds;
            for (int i = 0; i < layerCount; i++)
            {
                int spritesPerLayer = 5 + r.Next(100);
                List<Sprite> sprites = new List<Sprite>();
                for (int j = 0; j < spritesPerLayer; j++)
                {
                    int si = r.Next(100) % nebulas.Count;
                    Sprite neb = new Sprite(nebulas[si]);
                    neb.Position = Interpolations.GetRandomRectanglePoint(areaSize, r);
                    neb.Scale = (float)(r.NextDouble() * 2f) + 0.1f;
                    neb.Rotation = (float)r.NextDouble() * MathHelper.PiOver2;
                    sprites.Add(neb);
                }
                layers.AddLayer(sprites, new Vector2(parallax, parallax));
                parallax += (0.9f / layerCount);
            }
        }

        private void SetupParticleSystem()
        {
            particleSystem = new ParticleSystem();
            shipEffect = new ParticleEffect();
            Texture2D texture1 = Content.Load<Texture2D>("Images/smoke2");
            Texture2D texture2 = Content.Load<Texture2D>("Images/smoke3");

            ParticleEmitter shipEmitter = new ConeEmitter(Vector2.Zero,
                MathHelper.ToRadians(90));
            shipEmitter.AddTexture(texture1);
            shipEmitter.AddTexture(texture2);
            shipEmitter.Quantity = 10;
            shipEmitter.AddPropertyGenerator(new RandomSpeedGenerator(2.01f, 4.0f, 1.0f));
            shipEmitter.AddPropertyGenerator(new ConstantColorGenerator(Color.Green * 0.7f));
            shipEmitter.AddPropertyGenerator(new RandomTTLGenerator(2.0f, 5.0f, 1.0f));
            shipEmitter.AddParticleModifier(new ScaleModifier2(0.1f, 2.0f));
            shipEmitter.AddParticleModifier(new DepthModifier2(0.0f, 1.0f));
            shipEmitter.AddParticleModifier(new OpacityModifier2(0.6f, 0f));
            shipEmitter.AddParticleModifier(new DampingLinearVelocityModifier(0.99f));
            shipEmitter.AddParticleModifier(new RotationModifier2(-1.0f, 1.0f));
            shipEffect.AddEmitter(shipEmitter);
            particleSystem.AddEffect(shipEffect);

            shipEffect2 = new ParticleEffect(); 
            ParticleEmitter shipEmitter2 = new ConeEmitter(Vector2.Zero, 
                MathHelper.ToRadians(90));
            shipEmitter2.AddTexture(texture1);
            shipEmitter2.AddTexture(texture2);
            shipEmitter2.Quantity = 10;
            shipEmitter2.AddPropertyGenerator(new RandomSpeedGenerator(2.01f, 4.0f, 1.0f));
            shipEmitter2.AddPropertyGenerator(new ConstantColorGenerator(Color.Yellow * 0.7f));
            shipEmitter2.AddPropertyGenerator(new RandomTTLGenerator(2.0f, 5.0f, 1.0f));
            shipEmitter2.AddParticleModifier(
                new ScaleModifier2(0.1f, 2.0f));
            shipEmitter2.AddParticleModifier(
                new DepthModifier2(0.0f, 1.0f));
            shipEmitter2.AddParticleModifier(
                new OpacityModifier2(0.6f, 0f));
            shipEmitter2.AddParticleModifier(new DampingLinearVelocityModifier(0.99f));
            shipEmitter2.AddParticleModifier(new RotationModifier2(-1.0f, 1.0f));
            shipEffect2.AddEmitter(shipEmitter2);
            particleSystem.AddEffect(shipEffect2);

        }

        public void HandleInput(InputManager input)
        {
            float VelocityMultiplier = 1.0f;
            float RotationMultiplier = 1.0f;
            PlayerIndex player = PlayerIndex.One;
            Vector2 moveDelta = Vector2.Zero;

            if (ControllingPlayer.HasValue)
            {
                HandleGamePadInput(input, ControllingPlayer);
            }

            if (input.IsKeyPressed(Keys.Space, null, out player)) 
            {
                Shoot(CurrentLevel.Player);
            }

            else if (input.IsKeyHeld(Keys.Space, null, out player))
            {
                Shoot(CurrentLevel.Player);
            }

            if (input.IsKeyHeld(Keys.Down, null, out player))
            {
                CurrentLevel.Player.Deaccelerate(VelocityMultiplier);
            }

            if (input.IsKeyHeld(Keys.Up, null, out player))
            {
                CurrentLevel.Player.Accelerate(VelocityMultiplier);
            }

            if (input.IsKeyHeld(Keys.Left, null, out player))
            {
//                CurrentLevel.Player.Rotation -= RotationDelta;
                CurrentLevel.Player.Steer(-RotationMultiplier);
            }

            if (input.IsKeyHeld(Keys.Right, null, out player))
            {
//                CurrentLevel.Player.Rotation += RotationDelta;
                CurrentLevel.Player.Steer(RotationMultiplier);
            }

            if (input.IsKeyHeld(Keys.Back, null, out player))
            {
                CurrentLevel.Player.Stop();
            }

            if (input.IsKeyHeld(Keys.PageUp, null, out player))
            {
                PlayCamera.Scale += 0.003f;
            }

            if (input.IsKeyHeld(Keys.PageDown, null, out player))
            {
                PlayCamera.Scale -= 0.0031f;
            }
        }

        private void HandleGamePadInput(InputManager input, PlayerIndex? player)
        {
            GamePadState gps = input.GetGamePadState(player);
            if (gps.IsConnected)
            {
                PlayerIndex idx;
                if (input.IsGamePadButtonPressed(Buttons.RightTrigger, ControllingPlayer, out idx))
                {
                    Shoot(CurrentLevel.Player);
                }
                Vector2 delta = gps.ThumbSticks.Left;
                if (delta.X != 0)
                {
                    CurrentLevel.Player.Steer(delta.X);
                }
                if (delta.Y > 0)
                {
                    CurrentLevel.Player.Accelerate(delta.Y);
                }
                else if (delta.Y < 0)
                {
                    CurrentLevel.Player.Deaccelerate(-delta.Y);
                }

            }
        }

        private void Shoot(Ship entity)
        {
            Bullet b = entity.Shoot();
            if (b != null)
            {
                CurrentLevel.AddEntity(b, CurrentLevel.EntityLayer);
            }
        }

/*
        private void world_OnBroadPhaseCollision(ref FixtureProxy proxyA, ref FixtureProxy proxyB)
        {
            // Get the collided entities
            Entity entityA = proxyA.Fixture.Body.UserData as Entity;
            Entity entityB = proxyB.Fixture.Body.UserData as Entity;
            if (entityA == null || entityB == null)
            {
                // fail in debug builds
                Debug.Assert(entityA != null && entityB != null);
                return;
            }
            entityA.CollideWith(entityB);
        }
*/
        private Body BodyFromTexture(Texture2D texture, float density)
        {
            //Create an array to hold the data from the texture
            uint[] data = new uint[texture.Width * texture.Height];

            //Transfer the texture data to the array
            texture.GetData(data);

            //Find the vertices that makes up the outline of the shape in the texture
            Vertices textureVertices = PolygonTools.CreatePolygon(data, texture.Width, false);

            //The tool return vertices as they were found in the texture.
            //We need to find the real center (centroid) of the vertices for 2 reasons:

            //1. To translate the vertices so the polygon is centered around the centroid.
            Vector2 centroid = -textureVertices.GetCentroid();
            textureVertices.Translate(ref centroid);

            //2. To draw the texture the correct place.
            Vector2 _origin = -centroid;

            //We simplify the vertices found in the texture.
            textureVertices = SimplifyTools.ReduceByDistance(textureVertices, 4f);

            //Since it is a concave polygon, we need to partition it into several smaller convex polygons
            List<Vertices> list = BayazitDecomposer.ConvexPartition(textureVertices);

            //scale the vertices from graphics space to sim space
            Vector2 vertScale = new Vector2(WorldConversions.ConvertFromPixels(1));
            foreach (Vertices vertices in list)
            {
                vertices.Scale(ref vertScale);
            }

            //Create a single body with multiple fixtures
            Body body = BodyFactory.CreateCompoundPolygon(
                CurrentLevel.PhysicsWorld, list, density, BodyType.Dynamic);
            return body;
        }
    }
}
