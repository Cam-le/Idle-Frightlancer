using UnityEngine;

namespace PettyFrightlancer.Core
{
    using PettyFrightlancer.Core.Services;
    using PettyFrightlancer.Core.Events;
    using PettyFrightlancer.Core.Utils;

    /// <summary>
    /// Central game controller and service initializer.
    /// Acts as the application entry point and manages the game lifecycle.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _persistAcrossScenes = true;

        private static GameManager _instance;

        public static GameManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            if (_persistAcrossScenes)
            {
                DontDestroyOnLoad(gameObject);
            }

            InitializeServices();
        }

        private void InitializeServices()
        {
            // Register all core services
            var serviceLocator = ServiceLocator.Instance;

            // Create and register essential services with minimal implementations
            // These implementations will be expanded in future development phases
            serviceLocator.Register<IEventBus>(new EventBus());

            // TODO: Phase F-04 - Implement TimeManager 
            serviceLocator.Register<ITimeManager>(new MinimalTimeManager());

            // TODO: Phase F-05 - Implement ResourceManager
            serviceLocator.Register<IResourceManager>(new MinimalResourceManager());

            // TODO: Phase F-06 - Implement SaveManager
            serviceLocator.Register<ISaveManager>(new MinimalSaveManager());

            // TODO: Phase F-07 - Implement UIManager
            serviceLocator.Register<IUIManager>(new MinimalUIManager());

            // Once all services are registered, initialize them
            serviceLocator.InitializeServices();

            Logger.Info("Game Manager initialized all services.");
        }

        private void OnApplicationQuit()
        {
            // Perform cleanup or save operations
            var saveManager = ServiceLocator.Instance.Get<ISaveManager>();

            try
            {
                // In actual implementation this would be awaited
                saveManager.SaveGameAsync();
                Logger.Info("Game exiting, data saved.");
            }
            catch (System.Exception ex)
            {
                Logger.Error($"Failed to save game on exit: {ex.Message}");
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // Game is pausing, save data
                var saveManager = ServiceLocator.Instance.Get<ISaveManager>();
                try
                {
                    // In actual implementation this would be awaited
                    saveManager.SaveGameAsync();
                    Logger.Info("Game paused, data saved.");
                }
                catch (System.Exception ex)
                {
                    Logger.Error($"Failed to save game on pause: {ex.Message}");
                }
            }
            else
            {
                // Game is resuming
                Logger.Info("Game resumed.");
            }
        }
    }
}