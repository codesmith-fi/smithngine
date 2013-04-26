/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.General
{
    #region Using statements
    using System;
    using Codesmith.SmithNgine.General;
    using Microsoft.Xna.Framework;
    using System.Diagnostics;
    #endregion

    /// <summary>
    /// EventTimer class, encapsulates functionality to trigger event after
    /// given TimeSpan. This uses XNA GameTime and TimeSpan, not real timers
    /// 
    /// Update() must be called in game loop. If added to GameState or GameCanvas
    /// update will be automatically called.
    /// 
    /// Please add a listener to the event before using Start()
    /// 
    /// <example>
    /// <code>
    ///     EventTrigger myTrigger = new EventTrigger(TimeSpan.FromSeconds(1.0f));
    ///     myTrigger.Repeat = true;
    ///     myTrigger.EventTriggered += myTrigger_EventTriggered;
    ///     myTrigger.Start();
    ///
    /// </code>
    /// Then in update loop:
    /// <code>
    ///     myTrigger.Update(gameTime);
    ///     // Use the transition value (0.0 - 1.0) if you want
    ///     float transitionValue = myTrigger.Transition;
    /// </code>
    /// In event callback:
    /// <code>
    ///     void myTrigger_EventTriggered(object obj, EventArgs e)
    ///     {
    ///         // do your kinky stuffz
    ///         // restart the trigger if you want (can be done even it's not
    ///         // autorepeating)
    ///         myTrigger.Start(TimeSpan.FromSeconds(10.0f));
    ///         
    ///         // It will restart automatically with the initial interval if 
    ///         // Repeat property is true
    ///         
    ///         // If you want to stop the trigger, call Cancel(). This will 
    ///         // stop even automatically repeating trigger
    ///         myTrigger.Cancel();
    ///     }
    /// </code>
    /// </example>
    /// Event can be set to repeat or (by default) trigger only once 
    /// Use Event EventTriggered to listen for completion
    /// </summary>
    public class EventTrigger : GameObjectBase
    {
        #region Fields
        const float TransitionStartValue = 0.0f;
        const float TransitionEndValue = 1.0f;
        // The time period configured for this event
        TimeSpan eventPeriod;
        // How much time is still left
        TimeSpan timeLeft;
        // Are we set to repeat or trigger only once
        bool repeat;
        // Transition value that goes from 0.0f to 1.0f during the time of the interval
        float currentTransitionValue;
        #endregion

        #region Events
        /// <summary>
        /// Event triggered when the given time has passed
        /// </summary>
        public event EventHandler<EventArgs> EventTriggered;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the Repeat mode. 
        /// <value><c>true</c> the event repeats, <c>false</c>event triggers once</value>
        /// </summary>
        public bool Repeat
        {
            get { return this.repeat; }
            set { this.repeat = value; }
        }
        /// <summary>
        /// Get the current transition value between 0.0f .. 1.0f
        /// Value linearly goes from 0 to 1 during the event interval period
        /// </summary>
        public float Transition
        {
            get { return currentTransitionValue; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, one second, no repeate
        /// </summary>
        public EventTrigger()
            : this(TimeSpan.FromSeconds(1.0f), false)
        {
        }

        /// <summary>
        /// Constructor which configures a custom period and repeat mode
        /// </summary>
        /// <param name="period">Time interval until the event triggers (after Start())</param>
        /// <param name="repeatedEvent">If true, repeats</param>
        public EventTrigger(TimeSpan period, bool repeatedEvent = false)
        {
            this.eventPeriod = period;
            this.timeLeft = period;
            this.repeat = repeatedEvent;
        }
        #endregion

        #region New methods
        /// <summary>
        /// Start the event timer
        /// If no listener is addded to the eventhandler this will fail
        /// </summary>
        public void Start()
        {
            Debug.Assert(EventTriggered != null, 
                "Can't start EventTimer because event listener is null!");
            this.timeLeft = eventPeriod;
            this.currentTransitionValue = TransitionStartValue;
            ActivateObject();
        }

        /// <summary>
        /// Start the event timer and give a new interval
        /// </summary>
        /// <param name="newEventTime">New time interval</param>
        public void Start(TimeSpan newEventTime)
        {
            this.eventPeriod = newEventTime;
            Start();
        }

        /// <summary>
        /// Cancel the event timer
        /// </summary>
        public void Cancel()
        {
            DeactivateObject();
        }

        /// <summary>
        /// Updates the timer and causes event to trigger if time interval has passed
        /// </summary>
        /// <param name="gameTime">GameTime from Update loop</param>
        public override void Update(GameTime gameTime)
        {
            if (ObjectIsActive)
            {
                timeLeft -= gameTime.ElapsedGameTime;

                double elapsedMs = gameTime.ElapsedGameTime.TotalMilliseconds;
                double transitionMs = eventPeriod.TotalMilliseconds;

                float delta = (float)(elapsedMs / transitionMs);
                currentTransitionValue = MathHelper.Clamp(
                    currentTransitionValue + delta, TransitionStartValue, TransitionEndValue);

                if (timeLeft <= TimeSpan.Zero)
                {
                    // If set to repeat, restart the timer
                    if (repeat)
                    {
                        Start();
                    }
                    else
                    {
                        Cancel();
                    }
                    // Call listeners
                    if (EventTriggered != null)
                    {
                        EventTriggered(this, EventArgs.Empty);
                    }
                }
            }
            base.Update(gameTime);
        }
        #endregion
    }
}
