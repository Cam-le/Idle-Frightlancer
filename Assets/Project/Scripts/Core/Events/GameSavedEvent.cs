using System;

namespace PettyFrightlancer.Core.Events
{
    /// <summary>
    /// Event fired when the game state is saved.
    /// </summary>
    public class GameSavedEvent : IGameEvent
    {
        public DateTime SaveTime { get; }

        public GameSavedEvent(DateTime saveTime)
        {
            SaveTime = saveTime;
        }
    }
}