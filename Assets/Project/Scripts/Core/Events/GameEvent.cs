using System;

namespace PettyFrightlancer.Core.Events
{
    /// <summary>
    /// Base class for game events with generic data.
    /// </summary>
    /// <typeparam name="T">Type of event data.</typeparam>
    public abstract class GameEvent<T> : IGameEvent
    {
        public T Data { get; }

        protected GameEvent(T data)
        {
            Data = data;
        }
    }
}