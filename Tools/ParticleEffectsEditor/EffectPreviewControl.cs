using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.WinFormsExample;
using Codesmith.SmithNgine.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;

namespace Codesmith.smithNgine.ParticleEffectsEditor
{
    using FpsTimer = System.Timers.Timer;
    using Codesmith.SmithNgine.Primitives;
    public class EffectPreviewControl : GraphicsDeviceControl
    {
        
        private ParticleSystem particleSystem;
        private SpriteBatch spriteBatch;
        private ContentManager content;
        private Viewport viewport;
        private FpsTimer timer;
        private TimeSpan totalTimeSpan;
        private GameTime gameTime;

        public ParticleEffect Effect
        {
            get;
            set;
        }

        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewport = GraphicsDevice.Viewport;

            Texture2D tex1 = content.Load<Texture2D>("Images/smoke2");

            particleSystem = new ParticleSystem();
            Effect = new ParticleEffect();
            Effect.Rotation = 0f;
            Effect.Position = new Vector2(viewport.Width / 2, viewport.Height/2);
            ParticleEmitter emitter = new CircleEmitter(new Circle(100.0f, Vector2.Zero));
            emitter.AutoGenerate = true;
            ParticleGenerationParams config = new ParticleGenerationParams();
            
            config.AddTexture(tex1);
            config.ColorRangeStart = Color.Red;
            config.ColorRangeEnd = Color.Yellow;
            config.QuantityRange = new Vector2(10, 100);
            config.Flags = EmitterCastStyle.None;
            config.ScaleRange = new Vector2(0.1f, 1.1f);
            config.DepthRange = new Vector2(0.0f, 1.0f);
            config.OpacityRange = new Vector2(0.6f, 0f);
            config.InitialSpeedRange = new Vector2(0.1f, 2.0f);
            config.SpeedDamping = 0.99f;
            config.RotationRange = new Vector2(-1.0f, 1.0f);
            config.InitialRotationVariation = 1.0f;
            config.TTLRange = new Vector2(500.0f, 4000.0f); emitter.Configuration = config;            
            Effect.AddEmitter(emitter);
            particleSystem.AddEffect(Effect);

            // Start the animation timer.
//            timer = Stopwatch.StartNew();
            timer = new FpsTimer(1.0f / 20);
            totalTimeSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            gameTime = new GameTime(TimeSpan.Zero, TimeSpan.Zero);

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
//            double ms = e.SignalTime.Millisecond - previous.Millisecond;
//            TimeSpan elapsed = TimeSpan.FromMilliseconds(ms);
//            this.totalTime.Add(elapsed);
            TimeSpan now = TimeSpan.FromTicks(e.SignalTime.Ticks);
            TimeSpan elapsed = now - totalTimeSpan;
            totalTimeSpan = TimeSpan.FromTicks(e.SignalTime.Ticks);
            gameTime = new GameTime(totalTimeSpan, elapsed);
            Invalidate();
        }

        protected void Update(TimeSpan elapsed)
        {
//            particleSystem.Update(elapsed);
        }

        protected override void Draw()
        {
            particleSystem.Update(gameTime);
            GraphicsDevice.Clear(Color.Black);
            particleSystem.Draw(spriteBatch);
        }
    }
}
