using UnityEngine;
using UnityEngine.UIElements;

namespace PettyFrightlancer.UI
{
    /// <summary>
    /// Base class for UI Toolkit documents.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public abstract class UIScreen : MonoBehaviour
    {
        protected UIDocument _uiDocument;
        protected VisualElement _root;

        protected virtual void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }

        protected virtual void OnEnable()
        {
            _root = _uiDocument.rootVisualElement;
            BindElements();
            RegisterCallbacks();
        }

        protected virtual void OnDisable()
        {
            UnregisterCallbacks();
        }

        /// <summary>
        /// Binds UI elements to fields.
        /// </summary>
        protected abstract void BindElements();

        /// <summary>
        /// Registers event callbacks.
        /// </summary>
        protected abstract void RegisterCallbacks();

        /// <summary>
        /// Unregisters event callbacks.
        /// </summary>
        protected abstract void UnregisterCallbacks();

        /// <summary>
        /// Shows the UI screen.
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the UI screen.
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}