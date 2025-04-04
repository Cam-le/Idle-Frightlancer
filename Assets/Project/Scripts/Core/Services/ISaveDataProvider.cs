namespace PettyFrightlancer.Core.Services
{
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