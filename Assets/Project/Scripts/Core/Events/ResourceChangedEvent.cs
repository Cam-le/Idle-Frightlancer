namespace PettyFrightlancer.Core.Events
{
    /// <summary>
    /// Event fired when a resource amount changes.
    /// </summary>
    public class ResourceChangedEvent : IGameEvent
    {
        public Services.ResourceType ResourceType { get; }
        public float PreviousAmount { get; }
        public float NewAmount { get; }
        public float Delta { get; }

        public ResourceChangedEvent(Services.ResourceType resourceType, float previousAmount, float newAmount)
        {
            ResourceType = resourceType;
            PreviousAmount = previousAmount;
            NewAmount = newAmount;
            Delta = newAmount - previousAmount;
        }
    }
}