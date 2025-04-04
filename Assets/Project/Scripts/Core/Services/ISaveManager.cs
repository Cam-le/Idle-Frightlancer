namespace PettyFrightlancer.Core.Services
{
    /// <summary>
    /// Handles saving and loading game state.
    /// </summary>
    public interface ISaveManager : IService
    {
        /// <summary>
        /// Saves the game state asynchronously.
        /// </summary>
        /// <returns>Task that completes when the save operation finishes.</returns>
        System.Threading.Tasks.Task SaveGameAsync();

        /// <summary>
        /// Loads the game state asynchronously.
        /// </summary>
        /// <returns>Task that completes when the load operation finishes.</returns>
        System.Threading.Tasks.Task<bool> LoadGameAsync();

        /// <summary>
        /// Registers a component to be saved.
        /// </summary>
        /// <param name="key">Unique key for the component.</param>
        /// <param name="provider">Provider for the component's save data.</param>
        void RegisterSaveData(string key, ISaveDataProvider provider);

        /// <summary>
        /// Gets save data for a component.
        /// </summary>
        /// <typeparam name="T">Type of save data.</typeparam>
        /// <param name="key">Key of the component.</param>
        /// <returns>Save data for the component, or default if not found.</returns>
        T GetSaveData<T>(string key) where T : class, new();
    }

    /// <summary>
    /// Provider for save data.
    /// </summary>
    public interface ISaveDataProvider
    {
        /// <summary>
        /// Gets the data to save.
        /// </summary>
        /// <returns>Data to save.</returns>
        object GetSaveData();

        /// <summary>
        /// Loads data from a save.
        /// </summary>
        /// <param name="data">Data to load.</param>
        void LoadSaveData(object data);
    }
}