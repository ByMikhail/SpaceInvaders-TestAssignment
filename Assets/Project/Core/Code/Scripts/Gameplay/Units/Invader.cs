using System;
using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Units.AbilityInterfaces;
using SpaceInvaders.Gameplay.Units.Factories;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Units
{
    public class Invader : GameplayUnit, IKillable
    {
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private ProjectileFactoryBase _projectileFactory;

        public bool IsKilled { get; private set; }

        public event EventHandler OnKilled;

        public void Fire()
        {
            _projectileFactory.Create(tag, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
        }

        public void Kill()
        {
            IsKilled = true;
            OnKilled?.Invoke(this, EventArgs.Empty);

            DestroyItself();
        }
    }
}