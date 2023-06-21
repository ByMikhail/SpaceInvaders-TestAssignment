using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace SpaceInvaders.Foundation.ServiceLocator
{
    public class ServiceLocator
    {
        public static readonly ServiceLocator Default = new();

        private readonly Dictionary<Type, object> _services = new();

        public void Register<TService>(TService service)
            where TService : class
        {
            if (service == null || service.Equals(null))
            {
                throw new ArgumentNullException($"[{GetType().Name}] The '{nameof(service)}' argument must not be null.");
            }

            _services[typeof(TService)] = service;
        }

        public void Unregister<TService>()
        {
            Unregister(typeof(TService));
        }

        public void Unregister(IEnumerable<Type> serviceTypes)
        {
            foreach (var serviceType in serviceTypes)
            {
                Unregister(serviceType);
            }
        }

        public void Unregister(Type serviceType)
        {
            if (!_services.ContainsKey(serviceType))
            {
                throw new Exception($"[{GetType().Name}] A service for '{serviceType.Name}' type has not been registered.");
            }

            _services.Remove(serviceType);
        }

        public TService GetService<TService>()
            where TService : class
        {
            Type serviceType = typeof(TService);

            if (!_services.ContainsKey(serviceType))
            {
                throw new Exception($"[{GetType().Name}] A service for '{serviceType.Name}' type has not been registered.");
            }

            var service = _services[serviceType];

            if (service is Object unityObjectService && !unityObjectService)
            {
                throw new Exception($"[{GetType().Name}] A service for '{serviceType.Name}' type has been destroyed.");
            }

            return (TService)service;
        }
    }
}