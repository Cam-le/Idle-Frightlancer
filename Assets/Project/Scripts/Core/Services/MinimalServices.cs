using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace PettyFrightlancer.Core.Services
{
    using PettyFrightlancer.Common.Enums;
    using PettyFrightlancer.Core.Utils;
    using PettyFrightlancer.Core.Events;

    /// <summary>
    /// Minimal implementation of ITimeManager.
    /// Will be replaced in Phase F-04 with full implementation.
    /// </summary>
    public class MinimalTimeManager : ITimeManager
    {
        private float _timeScale = 1.0f;

        public float TimeScale
        {
            get => _timeScale;
            set => _timeScale = Mathf.Clamp(value, 0f, 5f);
        }

        public void Initialize()
        {
            Logger.Info("MinimalTimeManager initialized.");
        }

        public int CreateTimer(float duration, Action onComplete, bool autoStart = true)
        {
            Logger.Warning("MinimalTimeManager.CreateTimer called - not fully implemented yet.");
            return -1; // Invalid timer ID
        }

        public void PauseTimer(int timerId)
        {
            Logger.Warning("MinimalTimeManager.PauseTimer called - not fully implemented yet.");
        }

        public void ResumeTimer(int timerId)
        {
            Logger.Warning("MinimalTimeManager.ResumeTimer called - not fully implemented yet.");
        }

        public void CancelTimer(int timerId)
        {
            Logger.Warning("MinimalTimeManager.CancelTimer called - not fully implemented yet.");
        }

        public float CalculateOfflineProgress(DateTime lastTimestamp)
        {
            // Minimal implementation - returns a fixed value
            return 60f; // 1 minute of progress
        }
    }

    /// <summary>
    /// Minimal implementation of IResourceManager.
    /// Will be replaced in Phase F-05 with full implementation.
    /// </summary>
    public class MinimalResourceManager : IResourceManager
    {
        // Minimal in-memory storage for testing
        private readonly Dictionary<ResourceType, float> _resources = new Dictionary<ResourceType, float>();
        private readonly Dictionary<ResourceType, float> _generationRates = new Dictionary<ResourceType, float>();
        private IEventBus _eventBus;

        public void Initialize()
        {
            // Initialize with some default values for testing
            _eventBus = ServiceLocator.Instance.Get<IEventBus>();

            // Initialize all resource types with some default values
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                _resources[type] = 100f;
                _generationRates[type] = 10f;
            }

            Logger.Info("MinimalResourceManager initialized.");
        }

        public float GetResource(ResourceType resourceType)
        {
            if (_resources.TryGetValue(resourceType, out float amount))
            {
                return amount;
            }
            return 0f;
        }

        public float AddResource(ResourceType resourceType, float amount)
        {
            if (!_resources.ContainsKey(resourceType))
            {
                _resources[resourceType] = 0f;
            }

            float previousAmount = _resources[resourceType];
            _resources[resourceType] += amount;

            // Publish event
            _eventBus?.Publish(new ResourceChangedEvent(resourceType, previousAmount, _resources[resourceType]));

            return _resources[resourceType];
        }

        public bool RemoveResource(ResourceType resourceType, float amount)
        {
            if (!_resources.ContainsKey(resourceType) || _resources[resourceType] < amount)
            {
                return false;
            }

            float previousAmount = _resources[resourceType];
            _resources[resourceType] -= amount;

            // Publish event
            _eventBus?.Publish(new ResourceChangedEvent(resourceType, previousAmount, _resources[resourceType]));

            return true;
        }

        public float GetGenerationRate(ResourceType resourceType)
        {
            if (_generationRates.TryGetValue(resourceType, out float rate))
            {
                return rate;
            }
            return 0f;
        }
    }

    /// <summary>
    /// Minimal implementation of ISaveManager.
    /// Will be replaced in Phase F-06 with full implementation.
    /// </summary>
    public class MinimalSaveManager : ISaveManager
    {
        private readonly Dictionary<string, ISaveDataProvider> _saveDataProviders = new Dictionary<string, ISaveDataProvider>();
        private readonly Dictionary<string, object> _saveData = new Dictionary<string, object>();
        private IEventBus _eventBus;

        public void Initialize()
        {
            _eventBus = ServiceLocator.Instance.Get<IEventBus>();
            Logger.Info("MinimalSaveManager initialized.");
        }

        public async Task SaveGameAsync()
        {
            // In a real implementation, this would save to disk
            // For now, we just collect the data and log

            foreach (var provider in _saveDataProviders)
            {
                try
                {
                    _saveData[provider.Key] = provider.Value.GetSaveData();
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error getting save data for {provider.Key}: {ex.Message}");
                }
            }

            Logger.Info($"MinimalSaveManager collected save data for {_saveData.Count} providers.");

            // Simulate async operation for testing
            await Task.Delay(100);

            _eventBus?.Publish(new GameSavedEvent(DateTime.Now));
        }

        public async Task<bool> LoadGameAsync()
        {
            // For minimal implementation, we just simulate loading
            Logger.Info("MinimalSaveManager.LoadGameAsync - not fully implemented yet.");

            // Simulate async operation
            await Task.Delay(100);

            _eventBus?.Publish(new GameLoadedEvent(true, DateTime.Now));
            return true;
        }

        public void RegisterSaveData(string key, ISaveDataProvider provider)
        {
            if (string.IsNullOrEmpty(key))
            {
                Logger.Error("Cannot register save data with null or empty key.");
                return;
            }

            if (provider == null)
            {
                Logger.Error($"Cannot register null save data provider for key {key}.");
                return;
            }

            _saveDataProviders[key] = provider;
        }

        public T GetSaveData<T>(string key) where T : class, new()
        {
            if (string.IsNullOrEmpty(key))
            {
                Logger.Error("Cannot get save data with null or empty key.");
                return new T();
            }

            if (_saveData.TryGetValue(key, out object data) && data is T typedData)
            {
                return typedData;
            }

            return new T();
        }
    }

    /// <summary>
    /// Minimal implementation of IUIManager.
    /// Will be replaced in Phase F-07 with full implementation.
    /// </summary>
    public class MinimalUIManager : IUIManager
    {
        private IEventBus _eventBus;

        public void Initialize()
        {
            _eventBus = ServiceLocator.Instance.Get<IEventBus>();
            Logger.Info("MinimalUIManager initialized.");
        }

        public void ShowScreen(UIScreenType screenType, object data = null)
        {
            Logger.Info($"MinimalUIManager.ShowScreen: {screenType} - not fully implemented yet.");
        }

        public void HideCurrentScreen()
        {
            Logger.Info("MinimalUIManager.HideCurrentScreen - not fully implemented yet.");
        }

        public void ShowDialog(string title, string message, Action onConfirm = null, Action onCancel = null)
        {
            Logger.Info($"MinimalUIManager.ShowDialog: '{title}' - not fully implemented yet.");
            Debug.Log($"Dialog: {title} - {message}");
        }

        public VisualElement GetRoot()
        {
            Logger.Warning("MinimalUIManager.GetRoot - not fully implemented yet.");
            return null;
        }
    }
}