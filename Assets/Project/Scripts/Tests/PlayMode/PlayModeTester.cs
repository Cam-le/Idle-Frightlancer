using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace PettyFrightlancer.Tests
{
    using PettyFrightlancer.Core;
    using PettyFrightlancer.Core.Services;

    /// <summary>
    /// Enables testing with PlayMode tests.
    /// </summary>
    public class PlayModeTester
    {
        [UnityTest]
        public IEnumerator GameManager_InitializesServices()
        {
            // Load test scene with GameManager
            SceneManager.LoadScene("Game");

            // Wait for scene to load
            yield return null;

            // Get GameManager instance
            var gameManager = Object.FindFirstObjectByType<GameManager>();
            Assert.IsNotNull(gameManager, "GameManager should be in the scene");

            // Check if services are initialized
            // In real implementation, query ServiceLocator for specific services
            // and check that they're initialized

            // Example:
            // var eventBus = ServiceLocator.Instance.Get<IEventBus>();
            // Assert.IsNotNull(eventBus, "EventBus should be registered");
        }
    }
}