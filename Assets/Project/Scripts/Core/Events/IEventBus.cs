using PettyFrightlancer.Core.Services;
using System;

namespace PettyFrightlancer.Core.Events
{
    /// <summary>
    /// Central event bus for game-wide communication.
    /// </summary>
    public interface IEventBus : IService
    {
        /// <summary>
        /// Subscribes to an event of type TEvent.
        /// </summary>
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent;

        /// <summary>
        /// Unsubscribes from an event of type TEvent.
        /// </summary>
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent;

        /// <summary>
        /// Publishes an event of type TEvent.
        /// </summary>
        void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent;
    }


}