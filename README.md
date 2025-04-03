# Idle Frightlancer 👻

**A 2D Mobile Tycoon/Idle Game of Spooky Business Management**

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) <!-- Replace with your actual license badge -->
[![Unity Version](https://img.shields.io/badge/Unity-6000.0.x%20LTS-blueviolet)](https://unity.com/releases/editor/archive) <!-- Update with your specific Unity LTS version -->
[![Development Status](https://img.shields.io/badge/Status-In%20Development-brightgreen)](solo-dev-plan.md)

---

Welcome to the development repository for **Idle Frightlancer**! This project is a 2D pixel art tycoon/management game with idle elements, designed for mobile platforms. Players take on the role of a fledgling spectral entrepreneur, building a terrifying lair, recruiting an undead workforce, managing resources, and sending units out on haunting operations to grow their spooky business empire.

This game is being developed solo, leveraging Unity and its UI Toolkit for a modern, performant mobile experience.

## ✨ Key Features

*   ✅ **Lair Construction & Management:** Design and expand your multi-room underground lair on a grid system.
*   ✅ **Undead Unit Recruitment:** Create various types of undead minions (skeletons, ghosts, etc.).
*   ✅ **Resource Management:** Juggle resources like Soul Essence, Grave Dust, and more.
*   ✅ **Haunting Operations:** Assign units to operations with varying risks and rewards.
*   ✅ **Idle Progression:** Resources generate and operations can progress while the game is offline.
*   ✅ **Research & Upgrades:** Unlock new rooms, units, and abilities through a research tree.
*   ✅ **Unit Enthusiasm System:** Manage your workforce's morale for optimal efficiency.
*   ✅ **Random Events & Hero Challenges:** Adapt to unexpected occurrences and defend against pesky heroes.
*   ✅ **Data-Driven Design:** Easily configurable content using ScriptableObjects.
*   ✅ **Localization Support:** Built with multi-language capabilities in mind.

## 🛠️ Technical Overview

*   **Engine:** Unity (Targeting latest stable LTS)
*   **UI Framework:** Unity UI Toolkit (UXML / USS)
*   **Platform:** Mobile First (iOS & Android)
*   **Architecture:** Service-Oriented Architecture (SOA) combined with an Event-Driven Communication pattern.
*   **Core Language:** C#
*   **Version Control:** Git

## 🏗️ Architecture Highlights

The architecture is designed for maintainability, testability, and performance on mobile devices, especially considering the constraints of solo development.

*   **Modularity:** Systems are designed as independent services (e.g., `ResourceSystem`, `UnitSystem`) communicating via a central `EventBus` to minimize coupling.
*   **Single Responsibility:** Components within each system strive to adhere to the Single Responsibility Principle (SRP).
*   **Data-Driven:** Extensive use of `ScriptableObjects` for configuring game data (Resources, Rooms, Units, Operations, Events, etc.), allowing for easier balancing and content expansion.
*   **Performance Focus:** Optimized for mobile constraints, including efficient timer management, object pooling, batch processing for calculations, asynchronous operations for saving/loading and offline progress, and UI Toolkit best practices.
*   **Testability:** Interfaces and dependency injection patterns facilitate unit and integration testing using the Unity Test Framework.

## 🧩 Core Systems

The game is built upon several key systems:

*   `GameController`: Manages application lifecycle, game state, scene loading.
*   `TimeManager`: Handles game time, timers (pooled), and offline progress calculation.
*   `SaveManager`: Manages game state persistence (asynchronous).
*   `EventBus`: Facilitates decoupled communication between systems.
*   `ResourceSystem`: Manages all in-game resources and their generation.
*   `LairSystem`: Handles lair grid, room placement, and upgrades.
*   `UnitSystem`: Manages unit creation (pooling), assignment, and enthusiasm.
*   `HauntingSystem`: Manages haunting operations, requirements, and rewards.
*   `ProgressionSystem`: Handles research, unlocks, and player progression.
*   `EventSystem`: Manages random and triggered game events.
*   `UISystem`: Manages all UI via UI Toolkit, including data binding and theming.
*   `LocalizationSystem`: Handles multi-language support integrated with UI Toolkit.
*   *(Other systems include Analytics, Monetization as needed)*

## 🚀 Development Status & Plan

This project is currently **in active development**.

*   **Phase 1: Foundation** (Core Architecture, Base Systems)
*   **Phase 2: Functional Prototype** (Basic Gameplay Loop)
*   **Phase 3: Core Feature** (All Primary Systems Implemented)
*   **Phase 4: Expansion** (Content, Secondary Features)
*   **Phase 5: Polish** (Optimization, UX, Balancing)
*   **Phase 6: Finalization** (Testing, Platform Prep, Release)

## 🤝 Contributing

As this is currently a solo development project, contributions are not being accepted at this time.

## 📜 License

This project is licensed under the [MIT License]

Copyright (c) [2025] [Cam Le]. All Rights Reserved.
---
*Happy Haunting!*
```
