namespace PettyFrightlancer.Core.Services
{
    /// <summary>
    /// Manages game time, timers, and offline progress.
    /// </summary>
    public interface ITimeManager : IService
    {
        /// <summary>
        /// Current game time scale.
        /// </summary>
        float TimeScale { get; set; }

        /// <summary>
        /// Creates a new timer with the specified duration.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="onComplete">Callback when timer completes.</param>
        /// <param name="autoStart">Whether to start the timer immediately.</param>
        /// <returns>Unique identifier for the timer.</returns>
        int CreateTimer(float duration, System.Action onComplete, bool autoStart = true);

        /// <summary>
        /// Pauses a timer.
        /// </summary>
        /// <param name="timerId">ID of the timer to pause.</param>
        void PauseTimer(int timerId);

        /// <summary>
        /// Resumes a paused timer.
        /// </summary>
        /// <param name="timerId">ID of the timer to resume.</param>
        void ResumeTimer(int timerId);

        /// <summary>
        /// Cancels a timer.
        /// </summary>
        /// <param name="timerId">ID of the timer to cancel.</param>
        void CancelTimer(int timerId);

        /// <summary>
        /// Calculates offline progress since the last save.
        /// </summary>
        /// <param name="lastTimestamp">Timestamp of the last save.</param>
        /// <returns>Time elapsed in seconds.</returns>
        float CalculateOfflineProgress(System.DateTime lastTimestamp);
    }
}