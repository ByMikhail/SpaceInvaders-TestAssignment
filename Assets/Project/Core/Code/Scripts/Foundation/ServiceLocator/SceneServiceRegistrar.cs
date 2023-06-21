using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Foundation.ServiceLocator
{
    public abstract class SceneServiceRegistrar : MonoBehaviour
    {
        private HashSet<Type> _registeredTypes = new();

        #region Unity lifecycle

        private void Awake()
        {
            RegisterServices();
        }

        private void OnDestroy()
        {
            ServiceLocator.Default.Unregister(_registeredTypes);
        }

        #endregion

        protected void Register<TService>(TService service)
            where TService : class
        {
            ServiceLocator.Default.Register(service);
            _registeredTypes.Add(typeof(TService));
        }

        protected abstract void RegisterServices();
    }
}