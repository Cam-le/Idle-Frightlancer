using UnityEngine; 
using UnityEngine.UIElements;
using System;
using UnityEngine.UIElements.Experimental; // Keep using experimental for animation

namespace PettyFrightlancer.UI
{
    /// <summary>
    /// Extension methods for UI Toolkit elements.
    /// </summary>
    public static class UIExtensions
    {
        /// <summary>
        /// Shows a UI element with fade in animation.
        /// </summary>
        public static void ShowWithFade(this VisualElement element, float duration = 0.3f)
        {
            element.style.display = DisplayStyle.Flex;
            element.style.opacity = new StyleFloat(0f); // Start explicitly at 0

            // Create and start the animation
            element.experimental.animation
                .Start(0f, 1f, (int)(duration * 1000), (e, value) => // Ensure duration is int milliseconds
                {
                    // Explicitly create StyleFloat from the animated float value
                    e.style.opacity = new StyleFloat(value);
                })
                .Ease(Easing.OutCubic);
        }

        /// <summary>
        /// Hides a UI element with fade out animation.
        /// </summary>
        public static void HideWithFade(this VisualElement element, float duration = 0.3f)
        {
            // Start explicitly at 1 if it might not be already
            // element.style.opacity = new StyleFloat(1f); // Optional: uncomment if needed

            // Create and start the animation
            element.experimental.animation
                .Start(element.style.opacity.value, 0f, (int)(duration * 1000), (e, value) => // Animate from current opacity, ensure duration is int ms
                {
                    // Explicitly create StyleFloat from the animated float value
                    e.style.opacity = new StyleFloat(value);

                    // When animation completes (value is close to 0), hide the element
                    // Comparison is now valid as 'value' is treated as float
                    if (Mathf.Abs(value) < 0.01f) // Use Mathf.Abs for consistency in Unity
                    {
                        e.style.display = DisplayStyle.None;
                    }
                })
                .Ease(Easing.InCubic);
        }

        /// <summary>
        /// Sets up a button with click handler and hover effects.
        /// </summary>
        public static void SetupButton(this Button button, System.Action clickHandler)
        {
            // Unregister first to prevent potential multiple subscriptions if called repeatedly
            button.clicked -= clickHandler;
            button.clicked += clickHandler;

            // Consider using pseudo-states in USS for hover effects (:hover)
            // instead of manually adding/removing classes via callbacks,
            // but this callback method is also perfectly valid.
            button.RegisterCallback<MouseEnterEvent>(evt =>
            {
                button.AddToClassList("button-hover");
            });

            button.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                button.RemoveFromClassList("button-hover");
            });
        }
    }
}