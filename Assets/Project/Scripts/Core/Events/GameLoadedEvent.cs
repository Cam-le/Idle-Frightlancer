using System;

namespace PettyFrightlancer.Core.Events
{
    /// <summary>
    /// Event fired when the game state is loaded.
    /// </summary>
    public class GameLoadedEvent : IGameEvent
    {
        public bool Success { get; }
        public DateTime SaveTime { get; }

        public GameLoadedEvent(bool success, DateTime saveTime)
        {
            Success = success;
            SaveTime = saveTime;
        }
    }
}