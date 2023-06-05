using UnityEngine;

namespace SpaceInvaders.Foundation.TinyGameplayFramework
{
    public class GameplayUnitController<T> : MonoBehaviour
        where T : GameplayUnit
    {
        [SerializeField] private T _target;

        public T Target
        {
            get => _target;
            set => _target = value;
        }

        public bool TargetIsAssigned => _target != null;

        protected virtual void Update()
        {
            if (!TargetIsAssigned) return;

            TargetBasedUpdate();
        }

        protected void FixedUpdate()
        {
            if (!TargetIsAssigned) return;

            TargetBasedFixedUpdate();
        }

        protected virtual void TargetBasedUpdate() { }

        protected virtual void TargetBasedFixedUpdate() { }
    }
}