using UnityEngine;

namespace PettyFrightlancer.Core
{
    using PettyFrightlancer.Core.Services;
    using PettyFrightlancer.Core.Events;

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

            // Create and register services
            // Note: In a real implementation, we would create concrete implementations of these interfaces
            // and register them here. For now, we'll just show the pattern.

            // Example (commented out until implementations are created):
            // serviceLocator.Register<IEventBus>(new EventBus());
            // serviceLocator.Register<ITimeManager>(new TimeManager());
            // serviceLocator.Register<ISaveManager>(new SaveManager());
            // serviceLocator.Register<IResourceManager>(new ResourceManager());
            // serviceLocator.Register<IUIManager>(new UIManager());

            // Once all services are registered, initialize them
            serviceLocator.InitializeServices();

            Debug.Log("Game Manager initialized all services.");
        }

        private void OnApplicationQuit()
        {
            // Perform cleanup or save operations
            var saveManager = ServiceLocator.Instance.Get<ISaveManager>();
            // In actual implementation: await saveManager.SaveGameAsync();

            Debug.Log("Game exiting, data saved.");
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // Game is pausing, save data
                var saveManager = ServiceLocator.Instance.Get<ISaveManager>();
                // In actual implementation: await saveManager.SaveGameAsync();

                Debug.Log("Game paused, data saved.");
            }
            else
            {
                // Game is resuming
                Debug.Log("Game resumed.");
            }
        }
    }
}