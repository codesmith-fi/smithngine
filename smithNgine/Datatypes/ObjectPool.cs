/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */

namespace Codesmith.SmithNgine.Datatypes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Tries to implement a pool for preallocated objects.
    /// 
    /// Pool has 
    /// - maximum size (will not grow over this)
    /// - GrowTreshold (more objects are added to the pool when there are 
    ///   less objects than this in the pool)
    /// - GrowAmount (how many objects are added when the pool needs to grow)
    /// 
    /// Use Get() to get a object out from the pool
    /// Use Insert() to put a object back to the pool
    /// Or you can also use the the property Object to get and set.
    /// </summary>
    public class ObjectPool<TObject> : IDisposable
    {
        #region Fields
        // Pool grows by PoolGrowAmount if we are under this threshold
        private readonly int PoolGrowTreshold;
        // How much to grow when under the threshold
        private readonly int PoolGrowAmount;
        // The pool, FIFO queue.
        private Queue<TObject> cache;
        // Initial size of the cache
        private int initialCacheSize;
        // Maximum size limit, does not grow over this
        private int maximumCache;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set object in the pool
        /// <value>The object to get or set</value>
        /// </summary>
        public TObject Object
        {
            get
            {
                return Get();
            }

            set
            {
                Insert(value);
            }

        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a cache for 100 Objects and maximum cache 
        /// set to 1000 objects.
        /// </summary>
        public ObjectPool()
            : this(100, 1000)
        {
        }

        /// <summary>
        /// Initializes a cache for objects with initial amount
        /// and maximum cache size.
        /// </summary>
        /// <param name="initialCache"></param>
        /// <param name="maxLimit"></param>
        public ObjectPool(int initialCache, int maxLimit)
        {
            Debug.Assert(initialCache <= maxLimit, "Initial pool size is bigger than maxLimit");
            PoolGrowAmount = initialCache < 100 ? initialCache : 100;
            PoolGrowTreshold = maxLimit < 1000 ? maxLimit : 1000;
            initialCacheSize = initialCache;
            maximumCache = maxLimit;
            cache = new Queue<TObject>(initialCacheSize);
            Grow(initialCacheSize);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Put a object back to the cache if there is room
        /// </summary>
        /// <param name="p">Object to be pooled</param>
        public void Insert(TObject p)
        {
            Debug.Assert(p != null, "Trying to add null object to ParticlePool");
            if (cache.Count < maximumCache)
            {
                cache.Enqueue(p);
            }
        }

        /// <summary>
        /// Get a object from the pool
        /// </summary>
        /// <returns>The object</returns>
        public TObject Get()
        {
            TObject p = default(TObject);
            // Need to add more particles if we are under the treshold
            if (cache.Count < PoolGrowTreshold)
            {
                Grow(PoolGrowAmount - cache.Count);
            }

            // Get a first free radical (err. object) from the pool 
            p = cache.Dequeue();
            Debug.Assert(p != null, "ObjectPool retrieved a null Object, thats certainly a free radical!:)");
            return p;
        }
        #endregion

        #region From IDisposable
        /// <summary>
        /// Disposes this
        /// </summary>
        public virtual void Dispose()
        {
            cache.Clear();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Grow the cache by the given amount.
        /// Uses Activator to create new instance of the templated object
        /// using the default constructor.
        /// </summary>
        /// <param name="amount"></param>
        private void Grow(int amount)
        {
            Debug.Assert(cache != null, "ObjectPool's queue is not instantiated!");
            for (int i = 0; i < amount && cache.Count < maximumCache; ++i)
            {
                // Create new instance of the templated object with the default constructor
                TObject obj = (TObject)Activator.CreateInstance(typeof(TObject));
                Debug.Assert(obj != null, "Activator created a null instance of tempate object.");
                cache.Enqueue(obj);
            }
        }
        #endregion
    }
}
