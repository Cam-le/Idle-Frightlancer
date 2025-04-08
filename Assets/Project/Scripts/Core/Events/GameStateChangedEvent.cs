namespace PettyFrightlancer.Core.Events
{
    using PettyFrightlancer.Core.States;

    /// <summary>
    /// Event fired when the game state changes.
    /// </summary>
    public class GameStateChangedEvent : IGameEvent
    {
        /// <summary>
        /// The previous game state.
        /// </summary>
        public GameState PreviousState { get; }

        /// <summary>
        /// The new game state.
        /// </summary>
        public GameState NewState { get; }

        /// <summary>
        /// Creates a new GameStateChangedEvent.
        /// </summary>
        /// <param name="previousState">The previous game state.</param>
        /// <param name="newState">The new game state.</param>
        public GameStateChangedEvent(GameState previousState, GameState newState)
        {
            PreviousState = previousState;
            NewState = newState;
        }
    }
}