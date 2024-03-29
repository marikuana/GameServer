﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore
{
    public abstract class GameObject : IPosition
    {
        public Guid Id { get; set; }
        public Vector3 Position {
            get
            {
                return position;
            }
            set
            {
                if (position == value)
                    return;
                position = value;
                OnChangePosition?.Invoke(this);
            }
        }
        public Vector3 Pos => Position;
        public Vector3 Rotation { get; set; }

        public event Action<GameObject>? OnDestroy;
        public event Action<GameObject>? OnChangePosition;

        protected ILogger _logger;
        private Vector3 position;

        public GameObject(ILogger logger)
        {
            _logger = logger;
        }

        internal protected virtual void Update(TimeSpan time)
        {

        }

        public virtual void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
    }
}
