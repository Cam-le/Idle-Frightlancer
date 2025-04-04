using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PettyFrightlancer.Core
{
    /// <summary>
    /// Manages scene loading with transitions.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private float _fadeInDuration = 0.5f;
        [SerializeField] private float _fadeOutDuration = 0.5f;

        private static SceneLoader _instance;

        public static SceneLoader Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Loads a scene asynchronously.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        /// <param name="onComplete">Callback when loading completes.</param>
        public void LoadScene(string sceneName, Action onComplete = null)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, onComplete));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName, Action onComplete)
        {
            // Fade out
            // In actual implementation, this would control a UI fade overlay
            float fadeTime = 0;
            while (fadeTime < _fadeOutDuration)
            {
                fadeTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(fadeTime / _fadeOutDuration);
                // Update fade overlay alpha
                yield return null;
            }

            // Load scene asynchronously
            var asyncOp = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOp.isDone)
            {
                yield return null;
            }

            // Fade in
            fadeTime = 0;
            while (fadeTime < _fadeInDuration)
            {
                fadeTime += Time.deltaTime;
                float alpha = 1 - Mathf.Clamp01(fadeTime / _fadeInDuration);
                // Update fade overlay alpha
                yield return null;
            }

            onComplete?.Invoke();
        }
    }
}