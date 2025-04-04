using UnityEngine;

namespace PettyFrightlancer.Core.Utils
{
    /// <summary>
    /// Centralized logging utility with configurable levels.
    /// </summary>
    public static class Logger
    {
        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error
        }

        public static LogLevel CurrentLogLevel { get; set; } = LogLevel.Debug;

        public static void Debug(string message)
        {
            if (CurrentLogLevel <= LogLevel.Debug)
                UnityEngine.Debug.Log($"[DEBUG] {message}");
        }

        public static void Info(string message)
        {
            if (CurrentLogLevel <= LogLevel.Info)
                UnityEngine.Debug.Log($"[INFO] {message}");
        }

        public static void Warning(string message)
        {
            if (CurrentLogLevel <= LogLevel.Warning)
                UnityEngine.Debug.LogWarning($"[WARNING] {message}");
        }

        public static void Error(string message)
        {
            if (CurrentLogLevel <= LogLevel.Error)
                UnityEngine.Debug.LogError($"[ERROR] {message}");
        }
    }
}