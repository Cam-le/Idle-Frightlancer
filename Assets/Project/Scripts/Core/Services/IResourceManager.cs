namespace PettyFrightlancer.Core.Services
{
    using PettyFrightlancer.Common.Enums;
    /// <summary>
    /// Manages game resources (Soul Essence, Grave Dust, etc.).
    /// </summary>
    public interface IResourceManager : IService
    {
        /// <summary>
        /// Gets the current amount of a resource.
        /// </summary>
        /// <param name="resourceType">Type of resource.</param>
        /// <returns>Current amount of the resource.</returns>
        float GetResource(ResourceType resourceType);

        /// <summary>
        /// Adds an amount to a resource.
        /// </summary>
        /// <param name="resourceType">Type of resource.</param>
        /// <param name="amount">Amount to add.</param>
        /// <returns>New amount of the resource.</returns>
        float AddResource(ResourceType resourceType, float amount);

        /// <summary>
        /// Removes an amount from a resource.
        /// </summary>
        /// <param name="resourceType">Type of resource.</param>
        /// <param name="amount">Amount to remove.</param>
        /// <returns>True if successful, false if insufficient resources.</returns>
        bool RemoveResource(ResourceType resourceType, float amount);

        /// <summary>
        /// Gets the resource generation rate.
        /// </summary>
        /// <param name="resourceType">Type of resource.</param>
        /// <returns>Generation rate in units per minute.</returns>
        float GetGenerationRate(ResourceType resourceType);
    }
}
