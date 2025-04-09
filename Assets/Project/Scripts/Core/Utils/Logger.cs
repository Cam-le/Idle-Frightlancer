using System;
using System.Collections.Generic;
using UnityEngine;

namespace PettyFrightlancer.Core.Utils
{
    /// <summary>
    /// Centralized logging utility with configurable levels and contexts.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Log severity levels
        /// </summary>
        public enum LogLevel
        {
            Debug,   // Detailed information for development/debugging
            Info,    // General operational information
            Warning, // Potential issues that don't affect core functionality
            Error    // Critical issues that impact functionality
        }

        /// <summary>
        /// Current log level threshold. Messages below this level are not logged.
        /// </summary>
        public static LogLevel CurrentLogLevel { get; set; } = LogLevel.Debug;

        /// <summary>
        /// Whether to include timestamps in log messages
        /// </summary>
        public static bool IncludeTimestamps { get; set; } = true;

        /// <summary>
        /// Whether to include the calling method in log messages
        /// </summary>
        public static bool IncludeCallingMethod { get; set; } = true;

        /// <summary>
        /// A list of message patterns to ignore (for reducing noise)
        /// </summary>
        private static readonly List<string> _ignoredMessages = new List<string>();

        /// <summary>
        /// Adds a message pattern to the ignore list
        /// </summary>
        public static void AddIgnoredMessage(string messagePattern)
        {
            if (!string.IsNullOrEmpty(messagePattern) && !_ignoredMessages.Contains(messagePattern))
            {
                _ignoredMessages.Add(messagePattern);
            }
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        public static void Debug(string message, UnityEngine.Object context = null)
        {
            if (CurrentLogLevel <= LogLevel.Debug && !ShouldIgnore(message))
            {
                UnityEngine.Debug.Log(FormatMessage("[DEBUG]", message), context);
            }
        }

        /// <summary>
        /// Logs an info message
        /// </summary>
        public static void Info(string message, UnityEngine.Object context = null)
        {
            if (CurrentLogLevel <= LogLevel.Info && !ShouldIgnore(message))
            {
                UnityEngine.Debug.Log(FormatMessage("[INFO]", message), context);
            }
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        public static void Warning(string message, UnityEngine.Object context = null)
        {
            if (CurrentLogLevel <= LogLevel.Warning && !ShouldIgnore(message))
            {
                UnityEngine.Debug.LogWarning(FormatMessage("[WARNING]", message), context);
            }
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        public static void Error(string message, UnityEngine.Object context = null)
        {
            if (CurrentLogLevel <= LogLevel.Error && !ShouldIgnore(message))
            {
                UnityEngine.Debug.LogError(FormatMessage("[ERROR]", message), context);
            }
        }

        /// <summary>
        /// Logs an exception with stack trace
        /// </summary>
        public static void Exception(Exception exception, string additionalInfo = null, UnityEngine.Object context = null)
        {
            if (CurrentLogLevel <= LogLevel.Error && exception != null && !ShouldIgnore(exception.Message))
            {
                string message = string.IsNullOrEmpty(additionalInfo)
                    ? exception.Message
                    : $"{additionalInfo}: {exception.Message}";

                UnityEngine.Debug.LogException(exception, context);

                // Log additional info if provided
                if (!string.IsNullOrEmpty(additionalInfo))
                {
                    Error(additionalInfo, context);
                }
            }
        }

        /// <summary>
        /// Logs a formatted message with optional object values
        /// </summary>
        public static void InfoFormat(string format, params object[] args)
        {
            if (CurrentLogLevel <= LogLevel.Info)
            {
                try
                {
                    Info(string.Format(format, args));
                }
                catch (FormatException ex)
                {
                    Error($"Error formatting log message: {ex.Message}\nFormat: {format}");
                }
            }
        }

        /// <summary>
        /// Formats a log message with optional timestamp and calling method
        /// </summary>
        private static string FormatMessage(string prefix, string message)
        {
            string timestamp = IncludeTimestamps ? $"[{DateTime.Now:HH:mm:ss.fff}] " : "";
            string callingMethod = "";

            if (IncludeCallingMethod)
            {
                var stackFrame = new System.Diagnostics.StackFrame(2, false);
                var method = stackFrame.GetMethod();
                if (method != null)
                {
                    var type = method.DeclaringType;
                    callingMethod = $"[{type?.Name ?? "Unknown"}.{method.Name}] ";
                }
            }

            return $"{timestamp}{prefix} {callingMethod}{message}";
        }

        /// <summary>
        /// Checks if a message should be ignored based on the ignore list
        /// </summary>
        private static bool ShouldIgnore(string message)
        {
            if (string.IsNullOrEmpty(message)) return false;

            foreach (var pattern in _ignoredMessages)
            {
                if (message.Contains(pattern))
                {
                    return true;
                }
            }

            return false;
        }
    }
}