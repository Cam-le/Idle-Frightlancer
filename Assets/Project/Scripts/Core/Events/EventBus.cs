using System;
using System.Collections.Generic;
using UnityEngine;

namespace PettyFrightlancer.Core.Events
{
    /// <summary>
    /// Centralized event bus implementation for decoupled communication between systems.
    /// </summary>
    public class EventBus : IEventBus
    {
        // Dictionary mapping event types to lists of handlers
        private readonly Dictionary<Type, object> _subscribers = new Dictionary<Type, object>();

        // Debug tracking
        private bool _debugMode = false;

        public void Initialize()
        {
            // Enable debug mode in development builds
            _debugMode = Debug.isDebugBuild;
            Debug.Log("EventBus initialized.");
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent
        {
            var eventType = typeof(TEvent);

            if (!_subscribers.TryGetValue(eventType, out var handlers))
            {
                handlers = new List<Action<TEvent>>();
                _subscribers[eventType] = handlers;
            }

            var typedHandlers = (List<Action<TEvent>>)handlers;
            if (!typedHandlers.Contains(handler))
            {
                typedHandlers.Add(handler);

                if (_debugMode)
                {
                    Debug.Log($"Subscribed to {eventType.Name}, total subscribers: {typedHandlers.Count}");
                }
            }
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent
        {
            var eventType = typeof(TEvent);

            if (_subscribers.TryGetValue(eventType, out var handlers))
            {
                var typedHandlers = (List<Action<TEvent>>)handlers;

                if (typedHandlers.Remove(handler) && _debugMode)
                {
                    Debug.Log($"Unsubscribed from {eventType.Name}, remaining subscribers: {typedHandlers.Count}");
                }
            }
        }

        public void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent
        {
            var eventType = typeof(TEvent);

            if (_subscribers.TryGetValue(eventType, out var handlers))
            {
                var typedHandlers = (List<Action<TEvent>>)handlers;

                // Make a copy of the handlers list to avoid issues if handlers subscribe/unsubscribe during event processing
                var handlersCopy = new List<Action<TEvent>>(typedHandlers);

                foreach (var handler in handlersCopy)
                {
                    try
                    {
                        handler(gameEvent);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error in event handler for {eventType.Name}: {ex.Message}\n{ex.StackTrace}");
                    }
                }

                if (_debugMode)
                {
                    Debug.Log($"Published {eventType.Name} to {typedHandlers.Count} subscribers");
                }
            }
            else if (_debugMode)
            {
                Debug.Log($"Published {eventType.Name} but no subscribers");
            }
        }
    }
}