using UnityEngine;
using UnityEngine.UIElements;

namespace PettyFrightlancer.UI
{
    /// <summary>
    /// Component that initializes the main game UI document.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public class GameUIDocument : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private GameScreen _gameScreen;

        private void Awake()
        {
            if (_document == null)
                _document = GetComponent<UIDocument>();

            if (_gameScreen == null)
                _gameScreen = GetComponent<GameScreen>();
        }

        private void Start()
        {
            // Additional setup if needed
        }
    }
}