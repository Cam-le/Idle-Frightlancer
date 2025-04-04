using UnityEngine;

namespace PettyFrightlancer.Core
{
    /// <summary>
    /// Initializes scene-specific components and connections.
    /// </summary>
    public class SceneBootstrapper : MonoBehaviour
    {
        private void Start()
        {
            InitializeScene();
        }

        private void InitializeScene()
        {
            // Find and initialize scene-specific components
            // Wire up event connections

            Debug.Log($"Scene {gameObject.scene.name} initialized.");
        }
    }
}