namespace PettyFrightlancer.Core.Events
{
    /// <summary>
    /// Event fired when a timer completes.
    /// </summary>
    public class TimerCompletedEvent : IGameEvent
    {
        public int TimerId { get; }

        public TimerCompletedEvent(int timerId)
        {
            TimerId = timerId;
        }
    }
}