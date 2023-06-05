using UnityEngine;

namespace SpaceInvaders.Gameplay.Units.Factories
{
    [CreateAssetMenu(menuName = "Gameplay/Factories/SimpleProjectileFactory", fileName = "SimpleProjectileFactory")]
    public class SimpleProjectileFactory : ProjectileFactoryBase
    {
        [SerializeField] private Projectile _projectilePrefab;

        public override Projectile Create(string ignoreTag, Vector3 position, Quaternion direction)
        {
            var projectile = Instantiate(_projectilePrefab, position, direction);
            projectile.Init(ignoreTag);

            return projectile;
        }
    }
}