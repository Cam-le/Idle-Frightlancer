namespace PettyFrightlancer.Core.Services
{
    using UnityEngine.UIElements;

    /// <summary>
    /// Manages UI screens and navigation.
    /// </summary>
    public interface IUIManager : IService
    {
        /// <summary>
        /// Shows a UI screen.
        /// </summary>
        /// <param name="screenType">Type of screen to show.</param>
        /// <param name="data">Optional data to pass to the screen.</param>
        void ShowScreen(UIScreenType screenType, object data = null);

        /// <summary>
        /// Hides the current screen.
        /// </summary>
        void HideCurrentScreen();

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="title">Title of the dialog.</param>
        /// <param name="message">Message to display.</param>
        /// <param name="onConfirm">Callback when the user confirms.</param>
        /// <param name="onCancel">Callback when the user cancels.</param>
        void ShowDialog(string title, string message, System.Action onConfirm = null, System.Action onCancel = null);

        /// <summary>
        /// Gets the root visual element for UI Toolkit.
        /// </summary>
        /// <returns>Root visual element.</returns>
        VisualElement GetRoot();
    }

    /// <summary>
    /// Types of UI screens in the game.
    /// </summary>
    public enum UIScreenType
    {
        Main,
        Lair,
        Operations,
        Units,
        Research,
        Settings
    }
}