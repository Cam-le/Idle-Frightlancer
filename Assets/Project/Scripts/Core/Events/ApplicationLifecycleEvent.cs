namespace PettyFrightlancer.Core.Events
{
    using System;

    /// <summary>
    /// Base class for application lifecycle events.
    /// </summary>
    public abstract class ApplicationLifecycleEvent : IGameEvent
    {
        /// <summary>
        /// The timestamp when the event occurred.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Creates a new ApplicationLifecycleEvent with the current timestamp.
        /// </summary>
        protected ApplicationLifecycleEvent()
        {
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Event fired when the application is paused.
    /// </summary>
    public class ApplicationPausedEvent : ApplicationLifecycleEvent { }

    /// <summary>
    /// Event fired when the application is resumed.
    /// </summary>
    public class ApplicationResumedEvent : ApplicationLifecycleEvent { }

    /// <summary>
    /// Event fired when the application is about to quit.
    /// </summary>
    public class ApplicationQuittingEvent : ApplicationLifecycleEvent { }
}