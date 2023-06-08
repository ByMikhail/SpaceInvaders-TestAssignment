using System;
using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Units.AbilityInterfaces;
using SpaceInvaders.Gameplay.Units.Factories;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Units
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Spaceship : GameplayUnit, IKillable
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private ProjectileFactoryBase _projectileFactory;

        public event EventHandler OnKilled;

        private Rigidbody2D _rigidbody;
        private Projectile _lastFiredProjectile;

        #region Unity lifecycle

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            ResetLastFiredProjectile();
        }

        #endregion

        #region Interface methods

        public void Move(Vector2 direction)
        {
            var newPosition = _rigidbody.position + direction.normalized * (_speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }

        public void Fire()
        {
            bool canFire = !_lastFiredProjectile;
            if (!canFire) return;

            _lastFiredProjectile = _projectileFactory.Create(tag, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
            _lastFiredProjectile.OnDestroyed += Projectile_OnDestroyed;
        }

        public void Kill()
        {
            DestroyItself();

            OnKilled?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private void ResetLastFiredProjectile()
        {
            if (_lastFiredProjectile)
            {
                _lastFiredProjectile.OnDestroyed -= Projectile_OnDestroyed;
            }

            _lastFiredProjectile = null;
        }

        #region Event listeners

        private void Projectile_OnDestroyed(object _, EventArgs __)
        {
            ResetLastFiredProjectile();
        }

        #endregion
    }
}