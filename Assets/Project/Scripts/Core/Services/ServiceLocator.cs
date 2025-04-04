using System;
using System.Collections.Generic;
using UnityEngine;

namespace PettyFrightlancer.Core.Services
{
    /// <summary>
    /// Simple service locator for dependency injection.
    /// Used primarily for truly global services like IEventBus and ITimeManager.
    /// </summary>
    public class ServiceLocator
    {
        private static ServiceLocator _instance;

        /// <summary>
        /// Singleton instance of the service locator.
        /// </summary>
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceLocator();
                }
                return _instance;
            }
        }

        private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
        private bool _initialized = false;

        private ServiceLocator() { }

        /// <summary>
        /// Registers a service instance.
        /// </summary>
        /// <typeparam name="T">Type of service interface.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IService
        {
            var type = typeof(T);

            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"Service {type.Name} is already registered.");
                return;
            }

            _services[type] = service;
        }

        /// <summary>
        /// Gets a service instance.
        /// </summary>
        /// <typeparam name="T">Type of service interface.</typeparam>
        /// <returns>Service instance.</returns>
        public T Get<T>() where T : IService
        {
            var type = typeof(T);

            if (!_services.TryGetValue(type, out var service))
            {
                Debug.LogError($"Service {type.Name} is not registered.");
                return default;
            }

            return (T)service;
        }

        /// <summary>
        /// Initializes all registered services.
        /// </summary>
        public void InitializeServices()
        {
            if (_initialized)
            {
                Debug.LogWarning("Services are already initialized.");
                return;
            }

            foreach (var service in _services.Values)
            {
                service.Initialize();
            }

            _initialized = true;
            Debug.Log("All services initialized.");
        }
    }
}