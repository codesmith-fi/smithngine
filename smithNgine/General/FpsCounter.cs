/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.General
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Implements a FPS (Frames Per Second) counter which samples
    /// framerate once every second. It can report Average frame time and
    /// the Fps.
    /// 
    /// Call Update() in Draw().
    /// 
    /// Use Fps and AvgTimePerFrame properties to get the Fps average time.
    /// </summary>
    public class FpsCounter
    {
        // List of sampled time values (seconds)
        private List<float> samples;
        // EventTrigger for sampling
        private EventTrigger trigger;

        /// <summary>
        /// Get the Fps
        /// </summary>
        public float Fps
        {
            get;
            internal set;
        }

        /// <summary>
        /// Get the average time per frame
        /// <value>frame time as seconds</value>
        /// </summary>
        public float AvgTimePerFrame
        {
            get;
            internal set;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FpsCounter()
        {
            AvgTimePerFrame = 0.0f;
            Fps = 0.0f;
            Reset();
        }

        /// <summary>
        /// Update the Fps Counter, needs to be called on each frame
        /// to get accurate calculations
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            float elapsedSeconds = (float)time.ElapsedGameTime.TotalSeconds;         
            samples.Add(elapsedSeconds);
            trigger.Update(time);
        }

        /// <summary>
        /// Reset the Fps Counter
        /// </summary>
        public void Reset()
        {
            samples = new List<float>();
            trigger = new EventTrigger(TimeSpan.FromSeconds(1.0f), true);
            trigger.EventTriggered += trigger_EventTriggered;
            trigger.Start();
        }

        /// <summary>
        /// For samping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trigger_EventTriggered(object sender, EventArgs e)
        {
            float avg = 0f;
            foreach (float s in samples)
            {
                avg += s;
            }
            AvgTimePerFrame = avg / samples.Count;
            Fps = samples.Count;
            samples.Clear();
        }
    }
}
