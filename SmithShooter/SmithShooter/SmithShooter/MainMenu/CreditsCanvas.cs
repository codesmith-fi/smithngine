using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.General;

namespace Codesmith.SmithShooter.MainMenu
{
    public class CreditsEntry
    {
        private List<String> stringLines;

        public IList<String> Lines
        {
            get { return stringLines.AsReadOnly(); }
        }

        public TimeSpan DisplayPeriod
        {
            get;
            set;
        }

        public TimeSpan FadeInPeriod
        {
            get;
            set;
        }

        public TimeSpan FadeOutPeriod
        {
            get;
            set;
        }

        public CreditsEntry()
        {
            stringLines = new List<String>();
            DisplayPeriod = TimeSpan.FromSeconds(5.0f);
            FadeInPeriod = TimeSpan.FromSeconds(1.0f);
            FadeOutPeriod = TimeSpan.FromSeconds(2.0f);
        }

        public CreditsEntry(List<String> lines) : base()
        {
            stringLines = lines;
        }

        public CreditsEntry(String line) 
            : this()
        {
            stringLines.Add(line);
        }


        public void AddLine(String strLine)
        {
            stringLines.Add(strLine);
        }
    }

    public class CreditsCanvas : GameCanvas
    {
        private enum CreditsState
        {
            CreditsIdle,
            CreditsFadeIn,
            CreditsDisplaying,
            CreditsFadeOut
        }

        private List<CreditsEntry> credits = new List<CreditsEntry>();
        private Vector2 textPosition;
        private Vector2 textOrigin;
        private int current = 0;
        private CreditsState state;
        private float textScale;
        private SpriteFont creditsFont;
        private EventTrigger trigger;

        public CreditsCanvas()
        {
            trigger = new EventTrigger();
            trigger.Repeat = false;
            trigger.EventTriggered += trigger_EventTriggered;

            CreditsEntry entry = new CreditsEntry();
            entry.AddLine("SmithShooter");
            credits.Add(entry);
            entry = new CreditsEntry();
            entry.AddLine("It's the first game based on");
            entry.AddLine("smithNgine");
            credits.Add(entry);
            entry = new CreditsEntry();
            entry.AddLine("XNA/Monogame based 2D Gaming Framework");
            entry.AddLine("smithNgine uses Farseer Physics by Ian Qvist");
            credits.Add(entry);
            entry = new CreditsEntry();
            entry.AddLine("(C)2013 by Erno Pakarinen @ www.codesmith.fi");
            entry.AddLine("All Rights Reserved.");
            credits.Add(entry);
            entry = new CreditsEntry();
            entry.AddLine("Please read my blog");
            entry.AddLine("@");
            entry.AddLine("blog.codesmith.fi");
            entry.DisplayPeriod = TimeSpan.FromSeconds(8.0f);
            credits.Add(entry);
            entry = new CreditsEntry();
            entry.AddLine("Have phun!!!!!");
            credits.Add(entry);
        }

        public void Restart()
        {
            if (credits == null || credits.Count == 0)
            {
                Debug.Fail("CreditsCanvas: No credits added");
                return;
            }

            current = 0;
            textOrigin = Vector2.Zero;
            // Find out the area size of the biggest credit entry (widest and tallest)
            foreach (CreditsEntry ce in credits)
            {
                foreach( String str in ce.Lines)
                {
                    Vector2 size = creditsFont.MeasureString(str) / 2;
                    if (size.X > textOrigin.X)
                    {
                        textOrigin.X = size.X;
                    }
                    textOrigin.Y += size.Y;
                }
            }
            textPosition.X = textOrigin.X + 40;
            textPosition.Y = StateManager.GraphicsDevice.Viewport.Height - textOrigin.Y / 2;

            Bounds = new Rectangle((int)textPosition.X, (int)textPosition.Y,
                (int)textOrigin.X * 2, (int)textOrigin.Y * 2);

            SwitchToState(CreditsState.CreditsFadeIn);
        }

        public override void UnloadContent()
        {
            creditsFont = null;
            base.UnloadContent();
        }

        public override void LoadContent()
        {
            creditsFont = StateManager.Content.Load<SpriteFont>("Fonts/credits");
            Restart();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            int direction = ( state == CreditsState.CreditsFadeOut) ? -1 : 1;
            trigger.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Color color = Color.White;
            if (state == CreditsState.CreditsFadeIn)
            {
                color *= trigger.Transition;
            }
            else if (state == CreditsState.CreditsFadeOut)
            {
                color *= 1.0f - trigger.Transition;
            }

            if (state != CreditsState.CreditsFadeOut && state != CreditsState.CreditsFadeIn)
            {
                textScale = 1.0f + trigger.Transition * 0.1f;
            }
            Vector2 tmpPos = textPosition;
            foreach (String str in credits[current].Lines)
            {
                Vector2 strsize = creditsFont.MeasureString(str) / 2;
                spriteBatch.DrawString(creditsFont, str, tmpPos, color, 0f, 
                    new Vector2(strsize.X, strsize.Y), textScale, SpriteEffects.None, 1.0f);
                tmpPos.Y += creditsFont.LineSpacing;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SwitchToState(CreditsState nextState)
        {
            Debug.WriteLine(String.Format("Switching from state {0} to state {1}", 
                state.ToString(), nextState.ToString()));
            state = nextState;
            switch (nextState)
            {
                case CreditsState.CreditsIdle:
                    {
                        break;
                    }
                case CreditsState.CreditsFadeIn:
                    {
                        textScale = 1.0f;
                        trigger.Start(credits[current].FadeInPeriod);
                        break;
                    }
                case CreditsState.CreditsFadeOut:
                    {
                        trigger.Start(credits[current].FadeOutPeriod);
                        break;
                    }
                case CreditsState.CreditsDisplaying:
                    {
                        trigger.Start(credits[current].DisplayPeriod);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        void trigger_EventTriggered(object sender, EventArgs e)
        {
            switch (state)
            {
                case CreditsState.CreditsIdle:
                    {
                        // Do nothing 
                        trigger.Cancel();
                        break;
                    }
                case CreditsState.CreditsDisplaying:
                    {
                        SwitchToState(CreditsState.CreditsFadeOut);
                        break;
                    }
                case CreditsState.CreditsFadeIn:
                    {
                        SwitchToState(CreditsState.CreditsDisplaying);
                        break;
                    }

                case CreditsState.CreditsFadeOut:
                    {
                        if (credits.Count > current + 1)
                        {
                            current++;
                            SwitchToState(CreditsState.CreditsFadeIn);
                        }
                        else
                        {
                            Restart();
                        }
                        break;
                    }
            }
        }

    }
}
