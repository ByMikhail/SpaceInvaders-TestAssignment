using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Units.AbilityInterfaces;
using SpaceInvaders.Tools.Extensions;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Units
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : GameplayUnit
    {
        [SerializeField] private float _movementSpeed;

        private string _ignoreTag;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private Renderer[] _renderers;

        public void Init(string ignoreTag)
        {
            _ignoreTag = ignoreTag;
        }

        #region Unity lifecycle

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            _renderers = GetComponentsInChildren<Renderer>();
        }

        private void Update()
        {
            if (_renderers.EveryoneIsInvisible())
            {
                DestroyItself();
            }
        }

        private void FixedUpdate()
        {
            var newPosition = _rigidbody.position + (Vector2)_transform.up * (_movementSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            var attachedRigidbody = otherCollider.attachedRigidbody;
            if (attachedRigidbody)
            {
                if (attachedRigidbody.CompareTag(_ignoreTag)) return;

                var killableEntity = attachedRigidbody.GetComponent<IKillable>();

                if (killableEntity != null)
                {
                    killableEntity.Kill();
                    DestroyItself();
                }
            }
        }

        #endregion
    }
}