namespace PettyFrightlancer.Core.Services
{
    /// <summary>
    /// Base interface for all services in the game.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Initializes the service. Called after all services are registered.
        /// </summary>
        void Initialize();
    }
}