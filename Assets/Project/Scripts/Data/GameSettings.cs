using UnityEngine;

namespace PettyFrightlancer.Core.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "PettyFrightlancer/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Resource Generation")]
        public float baseResourceGenerationRate = 1.0f;

        [Header("Time Settings")]
        public float maxOfflineProgressTime = 24.0f; // hours

        [Header("UI Settings")]
        public float uiAnimationSpeed = 0.3f;

        // Add more game settings as needed
    }
}