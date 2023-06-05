using System;
using UnityEngine;

namespace SpaceInvaders.Foundation.TinyGameplayFramework
{
    public class GameplayUnit : MonoBehaviour
    {
        public event EventHandler OnDestroyed;

        protected void DestroyItself()
        {
            Destroy(gameObject);
            OnDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }
}