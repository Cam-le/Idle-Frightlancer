namespace PettyFrightlancer.Core.States
{
    /// <summary>
    /// Defines possible game states for the state machine.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// The game is initializing core systems.
        /// </summary>
        Initializing,

        /// <summary>
        /// A scene or assets are loading.
        /// </summary>
        Loading,

        /// <summary>
        /// The player is in the main menu.
        /// </summary>
        MainMenu,

        /// <summary>
        /// Active gameplay is in progress.
        /// </summary>
        Playing,

        /// <summary>
        /// The game is paused.
        /// </summary>
        Paused
    }
}