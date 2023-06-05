using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Units.AbilityInterfaces;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Units
{
    public class Projectile : GameplayUnit
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private string _absorberTag;

        private Transform _transform;
        private string _ignoreTag;

        public void Init(string ignoreTag)
        {
            _ignoreTag = ignoreTag;
        }

        #region Unity lifecycle

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            _transform.Translate(_transform.up * (_movementSpeed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.CompareTag(_ignoreTag)) return;

            var killableEntity = otherCollider.gameObject.GetComponent<IKillable>();

            if (killableEntity != null)
            {
                killableEntity.Kill();
                DestroyItself();
            }
            else if (otherCollider.gameObject.CompareTag(_absorberTag))
            {
                DestroyItself();
            }
        }

        #endregion
    }
}