using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using PettyFrightlancer.Common.Enums;
using PettyFrightlancer.Core.Services;
using PettyFrightlancer.Core.Events;

namespace PettyFrightlancer.UI
{
    /// <summary>
    /// Main game screen controller that manages the primary game UI.
    /// </summary>
    public class GameScreen : UIScreen
    {
        // UI Elements - Header
        private Button _menuToggle;
        private Label _headerTitle;
        private VisualElement _resourcesContainer;
        private Dictionary<ResourceType, ResourceDisplay> _resourceDisplays = new Dictionary<ResourceType, ResourceDisplay>();

        // UI Elements - Screen Navigation
        private VisualElement _lairScreen;
        private VisualElement _operationsScreen;
        private VisualElement _unitsScreen;
        private VisualElement _researchScreen;
        private Dictionary<Button, string> _screenMappings = new Dictionary<Button, string>();
        private List<Button> _navButtons = new List<Button>();

        // UI Elements - Side Menu
        private VisualElement _sideMenu;
        private Button _closeMenu;
        private Button _settingsMenuItem;

        // UI Elements - Lair
        private Label _enthusiasmValue;
        private VisualElement _enthusiasmFill;
        private VisualElement _lairGrid;

        // UI Elements - Toast
        private VisualElement _toast;
        private Label _toastMessage;

        // Services
        private IResourceManager _resourceManager;
        private IEventBus _eventBus;

        protected override void Awake()
        {
            base.Awake();

            // Get services from ServiceLocator
            _resourceManager = ServiceLocator.Instance.Get<IResourceManager>();
            _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        }

        protected override void BindElements()
        {
            // Header Elements
            _menuToggle = _root.Q<Button>("menu-toggle");
            _headerTitle = _root.Q<Label>("header-title");
            _resourcesContainer = _root.Q("resources-container");

            // Bind resource displays
            _resourceDisplays[ResourceType.SoulEssence] = new ResourceDisplay(
                _root.Q<Label>("soul-essence-value"),
                _root.Q<Label>("soul-essence-rate")
            );

            _resourceDisplays[ResourceType.GraveDust] = new ResourceDisplay(
                _root.Q<Label>("grave-dust-value"),
                _root.Q<Label>("grave-dust-rate")
            );

            _resourceDisplays[ResourceType.BoneChips] = new ResourceDisplay(
                _root.Q<Label>("bone-chips-value"),
                _root.Q<Label>("bone-chips-rate")
            );

            _resourceDisplays[ResourceType.Ectoplasm] = new ResourceDisplay(
                _root.Q<Label>("ectoplasm-value"),
                _root.Q<Label>("ectoplasm-rate")
            );

            // Screen Navigation
            _lairScreen = _root.Q("lair-screen");
            _operationsScreen = _root.Q("operations-screen");
            _unitsScreen = _root.Q("units-screen");
            _researchScreen = _root.Q("research-screen");

            // Nav Buttons with screen mappings
            var lairButton = _root.Q<Button>("lair-button");
            var operationsButton = _root.Q<Button>("operations-button");
            var unitsButton = _root.Q<Button>("units-button");
            var researchButton = _root.Q<Button>("research-button");

            _navButtons.Add(lairButton);
            _navButtons.Add(operationsButton);
            _navButtons.Add(unitsButton);
            _navButtons.Add(researchButton);

            // Map buttons to their target screens
            _screenMappings[lairButton] = "lair-screen";
            _screenMappings[operationsButton] = "operations-screen";
            _screenMappings[unitsButton] = "units-screen";
            _screenMappings[researchButton] = "research-screen";

            // Side Menu
            _sideMenu = _root.Q("side-menu");
            _closeMenu = _root.Q<Button>("close-menu");
            _settingsMenuItem = _root.Q<Button>("settings-menu-item");

            // Lair Elements
            _enthusiasmValue = _root.Q<Label>("enthusiasm-value");
            _enthusiasmFill = _root.Q("enthusiasm-fill");
            _lairGrid = _root.Q("lair-grid");

            // Toast
            _toast = _root.Q("toast");
            _toastMessage = _root.Q<Label>("toast-message");
        }

        protected override void RegisterCallbacks()
        {
            // Header
            _menuToggle.clicked += OnMenuToggleClicked;

            // Navigation
            foreach (var button in _navButtons)
            {
                button.clicked += () => OnNavButtonClicked(button);
            }

            // Side Menu
            _closeMenu.clicked += OnCloseMenuClicked;
            _settingsMenuItem.clicked += OnSettingsMenuItemClicked;

            // Event Subscriptions
            _eventBus.Subscribe<ResourceChangedEvent>(OnResourceChanged);

            // Room cells
            var roomCells = _lairGrid.Query(className: "room-cell").ToList();
            foreach (var cell in roomCells)
            {
                cell.RegisterCallback<ClickEvent>(OnRoomCellClicked);
            }
        }

        protected override void UnregisterCallbacks()
        {
            // Header
            _menuToggle.clicked -= OnMenuToggleClicked;

            // Navigation
            foreach (var button in _navButtons)
            {
                button.clicked -= () => OnNavButtonClicked(button);
            }

            // Side Menu
            _closeMenu.clicked -= OnCloseMenuClicked;
            _settingsMenuItem.clicked -= OnSettingsMenuItemClicked;

            // Event Subscriptions
            _eventBus.Unsubscribe<ResourceChangedEvent>(OnResourceChanged);

            // Room cells
            var roomCells = _lairGrid.Query(className: "room-cell").ToList();
            foreach (var cell in roomCells)
            {
                cell.UnregisterCallback<ClickEvent>(OnRoomCellClicked);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateResourceDisplays();
        }

        #region Event Handlers

        private void OnMenuToggleClicked()
        {
            _sideMenu.AddToClassList("active");
        }

        private void OnCloseMenuClicked()
        {
            _sideMenu.RemoveFromClassList("active");
        }

        private void OnNavButtonClicked(Button button)
        {
            // Update nav button states
            foreach (var navButton in _navButtons)
            {
                navButton.RemoveFromClassList("active");
            }
            button.AddToClassList("active");

            // Get screen name from our button mapping
            if (!_screenMappings.TryGetValue(button, out string screenName))
                return;

            // Show active screen, hide others
            _lairScreen.RemoveFromClassList("active");
            _operationsScreen?.RemoveFromClassList("active");
            _unitsScreen?.RemoveFromClassList("active");
            _researchScreen?.RemoveFromClassList("active");

            var targetScreen = _root.Q(screenName);
            if (targetScreen != null)
            {
                targetScreen.AddToClassList("active");
            }
        }

        private void OnSettingsMenuItemClicked()
        {
            // Show settings modal (would be implemented in a separate class)
            _sideMenu.RemoveFromClassList("active");
            // Implementation for showing settings modal would go here
        }

        private void OnRoomCellClicked(ClickEvent evt)
        {
            var roomCell = evt.currentTarget as VisualElement;
            if (roomCell == null)
                return;

            if (roomCell.ClassListContains("empty"))
            {
                // Show build room modal
                ShowToast("Build Room clicked");
            }
            else
            {
                // Show room details modal
                ShowToast("Room Details clicked");
            }
        }

        private void OnResourceChanged(ResourceChangedEvent evt)
        {
            UpdateResourceDisplay(evt.ResourceType);
        }

        #endregion

        #region UI Updates

        private void UpdateResourceDisplays()
        {
            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                UpdateResourceDisplay(resourceType);
            }
        }

        private void UpdateResourceDisplay(ResourceType resourceType)
        {
            if (!_resourceDisplays.TryGetValue(resourceType, out var display))
                return;

            float amount = _resourceManager.GetResource(resourceType);
            float rate = _resourceManager.GetGenerationRate(resourceType);

            display.UpdateDisplay(amount, rate);
        }

        public void ShowToast(string message)
        {
            _toastMessage.text = message;
            _toast.AddToClassList("visible");

            // Hide after delay
            StartCoroutine(HideToastAfterDelay(3f));
        }

        private System.Collections.IEnumerator HideToastAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _toast.RemoveFromClassList("visible");
        }

        #endregion

        /// <summary>
        /// Helper class to handle resource display updates.
        /// </summary>
        private class ResourceDisplay
        {
            private readonly Label _valueLabel;
            private readonly Label _rateLabel;

            public ResourceDisplay(Label valueLabel, Label rateLabel)
            {
                _valueLabel = valueLabel;
                _rateLabel = rateLabel;
            }

            public void UpdateDisplay(float amount, float rate)
            {
                // Format amount (e.g. 1,234 or 1.2K)
                string formattedAmount = FormatResourceAmount(amount);
                _valueLabel.text = formattedAmount;

                // Format rate (e.g. +12/min)
                string sign = rate >= 0 ? "+" : "";
                _rateLabel.text = $"{sign}{rate:F0}/min";
            }

            private string FormatResourceAmount(float amount)
            {
                if (amount >= 1_000_000)
                    return $"{amount / 1_000_000:F1}M";
                else if (amount >= 1_000)
                    return $"{amount / 1_000:F1}K";
                else
                    return $"{amount:F0}";
            }
        }
    }
}