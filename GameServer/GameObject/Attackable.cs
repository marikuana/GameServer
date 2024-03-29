﻿using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class Attackable : GameObject
    {
        public float Health { get; private set; }

        public bool Avaliable => Health > 0;

        public event Action<Attackable, float>? OnDamaged;
        public event Action<Attackable, float>? OnHeal;
        public event Action<Attackable>? OnDeath;
        public event Action<Attackable, float>? OnHealthChange;

        public Attackable(ILogger logger) : base(logger)
        {
        }

        public void Damage(float value)
        {
            OnDamaged?.Invoke(this, value);
            SetHealth(Health - value);
        }

        public void Kill()
        {
            Damage(Health);
        }

        public void Heal(float value)
        {
            OnHeal?.Invoke(this, value);
            SetHealth(Health + value);
        }

        public void SetHealth(float value)
        {
            if (Health == value)
                return;

            Health = value;
            OnHealthChange?.Invoke(this, Health);
            if (!Avaliable)
                OnDeath?.Invoke(this);
        }
    }
}