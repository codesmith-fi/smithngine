/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
namespace Codesmith.SmithNgine.General
{
    using System;
    using Microsoft.Xna.Framework;

    public abstract class GameObjectBase : IActivatableObject
    {
        #region Fields
        private bool objectIsActive;
        #endregion

        #region Properties
        public ITransitionSource TransitionSource
        {
            get;
            set;
        }

        public bool ObjectIsActive
        {
            get { return objectIsActive; }
        }

        #endregion

        #region Events
        public event EventHandler<EventArgs> ObjectActivated;
        public event EventHandler<EventArgs> ObjectDeactivated;
        #endregion

        public GameObjectBase()
        {
            TransitionSource = new DefaultTransitionSource();
        }

        public virtual void ActivateObject()
        {
            if (!ObjectIsActive && ObjectActivated != null)
            {
                ObjectActivated(this, EventArgs.Empty);
            }
            objectIsActive = true;
        }

        public virtual void DeactivateObject()
        {
            if (ObjectIsActive && ObjectDeactivated != null)
            {
                ObjectDeactivated(this, EventArgs.Empty);
            }
            objectIsActive = false;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Dismiss()
        {
            objectIsActive = false;
        }
    }
}
