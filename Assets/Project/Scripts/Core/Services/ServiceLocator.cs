using System;
using System.Collections.Generic;
using UnityEngine;

namespace PettyFrightlancer.Core.Services
{
    using PettyFrightlancer.Core.Utils;

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

        // This is private to enforce singleton pattern
        private ServiceLocator() { }

        /// <summary>
        /// Registers a service instance.
        /// </summary>
        /// <typeparam name="T">Type of service interface.</typeparam>
        /// <param name="service">Service instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if service is null.</exception>
        public void Register<T>(T service) where T : IService
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service), $"Cannot register null service for type {typeof(T).Name}");
            }

            var type = typeof(T);

            if (_services.ContainsKey(type))
            {
                Logger.Warning($"Service {type.Name} is already registered. Ignoring registration.");
                return;
            }

            _services[type] = service;
            Logger.Debug($"Registered service: {type.Name}");
        }

        /// <summary>
        /// Gets a service instance.
        /// </summary>
        /// <typeparam name="T">Type of service interface.</typeparam>
        /// <returns>Service instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the requested service is not registered.</exception>
        public T Get<T>() where T : IService
        {
            var type = typeof(T);

            if (!_services.TryGetValue(type, out var service))
            {
                // Improved error message with registered services list for debugging
                var registeredServices = string.Join(", ", _services.Keys);
                throw new InvalidOperationException(
                    $"Service {type.Name} is not registered. Registered services: {registeredServices}");
            }

            return (T)service;
        }

        /// <summary>
        /// Checks if a service is registered.
        /// </summary>
        /// <typeparam name="T">Type of service interface.</typeparam>
        /// <returns>True if the service is registered, false otherwise.</returns>
        public bool IsRegistered<T>() where T : IService
        {
            return _services.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Initializes all registered services.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if services are already initialized.</exception>
        public void InitializeServices()
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Services are already initialized. Cannot initialize twice.");
            }

            Logger.Info($"Initializing {_services.Count} services...");

            // Track initialization errors to report them all together
            var initializationErrors = new List<string>();

            foreach (var kvp in _services)
            {
                try
                {
                    kvp.Value.Initialize();
                    Logger.Debug($"Initialized service: {kvp.Key.Name}");
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Failed to initialize {kvp.Key.Name}: {ex.Message}";
                    Logger.Error(errorMessage);
                    initializationErrors.Add(errorMessage);
                }
            }

            if (initializationErrors.Count > 0)
            {
                throw new AggregateException(
                    $"Failed to initialize {initializationErrors.Count} services: {string.Join("; ", initializationErrors)}");
            }

            _initialized = true;
            Logger.Info("All services initialized successfully.");
        }

        /// <summary>
        /// Tries to get a service without throwing an exception if it's not found.
        /// </summary>
        /// <typeparam name="T">Type of service interface.</typeparam>
        /// <param name="service">Out parameter that will contain the service if found.</param>
        /// <returns>True if the service was found, false otherwise.</returns>
        public bool TryGet<T>(out T service) where T : IService
        {
            var type = typeof(T);

            if (_services.TryGetValue(type, out var serviceObj))
            {
                service = (T)serviceObj;
                return true;
            }

            service = default;
            return false;
        }
    }
}