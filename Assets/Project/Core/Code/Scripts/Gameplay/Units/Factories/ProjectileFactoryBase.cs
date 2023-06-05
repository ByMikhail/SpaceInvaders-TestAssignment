using UnityEngine;

namespace SpaceInvaders.Gameplay.Units.Factories
{
    public abstract class ProjectileFactoryBase : ScriptableObject
    {
        public abstract Projectile Create(string ignoreTag, Vector3 position, Quaternion direction);
    }
}