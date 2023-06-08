using UnityEngine;

namespace SpaceInvaders.Gameplay.Units.Factories
{
    [CreateAssetMenu(menuName = "Gameplay/Factories/RandomProjectileFactory", fileName = "RandomProjectileFactory")]
    public class RandomProjectileFactory : ProjectileFactoryBase
    {
        [SerializeField] private Projectile[] _projectilePrefabs;

        public override Projectile Create(string ignoreTag, Vector3 position, Quaternion direction)
        {
            int projectilePrefabIndex = Random.Range(0, _projectilePrefabs.Length);
            var projectilePrefab = _projectilePrefabs[projectilePrefabIndex];

            var projectile = Instantiate(projectilePrefab, position, direction);
            projectile.Init(ignoreTag);

            return projectile;
        }
    }
}