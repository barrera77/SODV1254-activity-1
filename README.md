# Space Adventure Game Documentation

## Overview
The **Space Adventure Game** simulates a space exploration journey where players manage spaceships, travel between planets, gather resources, and make strategic decisions to ensure the survival of their crew. The game features dynamic interactions between spaceships, planets, and a travel system governed by resource requirements.

---

## Classes and Their Roles

### 1. SpaceShip
**Purpose:** Represents a player's spaceship.

**Attributes:**
- **Name** - The spaceship's name.
- **Fuel** - Current fuel level.
- **MaxFuelCapacity** - Maximum fuel capacity.
- **CargoCapacity** - Maximum weight the ship can carry.
- **CargoList** - A list of cargo items currently on board.
- **Location** - The current planet where the spaceship is located.

**Methods:**
- **Fly(Planet destination)** - Moves the ship to a new planet if fuel requirements are met.
- **Refuel(int amount)** - Refuels the spaceship, ensuring it doesn't exceed `MaxFuelCapacity`.
- **LoadCargo(Cargo item)** - Adds a cargo item to the ship's cargo, verifying weight limits.
- **UnloadCargo(Cargo item)** - Removes a cargo item from the ship.

**Interaction:** The spaceship interacts with planets (to refuel or buy cargo) and the travel system (to validate resources for trips).

---

### 2. Planet
**Purpose:** Represents a planet with unique resources and facilities.

**Attributes:**
- **Name** - The name of the planet.
- **AvailableCargo** - A dictionary of cargo items and their quantities.
- **RefuelingStation** - Indicates if the planet has a refueling station.

**Methods:**
- **AddCargo(Cargo item, int quantity)** - Adds an item to the planet's inventory.
- **RemoveCargo(Cargo item, int quantity)** - Removes or decreases the quantity of a specific item.
- **RefuelSpaceship(SpaceShip ship, int amount)** - Refuels a spaceship if the planet has a refueling station.
- **DisplayAvailableCargo()** - Displays all available cargo on the planet.

**Interaction:** Planets serve as resource hubs for spaceships to load cargo, refuel, or prepare for interplanetary travel.

---

### 3. Cargo
**Purpose:** Represents a resource item such as fuel, food, or medical supplies.

**Attributes:**
- **Name** - The name of the cargo item.
- **Weight** - The weight of the cargo item.

**Interaction:** Cargo is managed by both `Planet` and `SpaceShip` classes, facilitating resource tracking during the game.

---

## Key Methods and Interactions

### 1. Travel System
**Method:** `MeetsTheRequirements(string start, string destination)`
- Checks if a spaceship has the necessary fuel, food, and medical supplies for a trip.
- Interacts with the `SpaceShip` and `Planet` classes to evaluate current resources against travel requirements.

---

### 2. Fuel Station
**Method:** `GoToFuelStation(int start, int destinationOption)`
- Allows the player to refuel their spaceship if the current planet has a refueling station.
- Simulates refueling with an animated progress bar.

---

### 3. Supplies Store
**Method:** `GoToSuppliesStore(int start, int destinationOption)`
- Enables players to purchase supplies (food, fuel, medical items) from the planet's inventory.
- Deducts the purchased items from the planet and adds them to the spaceship.

---

## Animation Approach
Animations are implemented using **threads and loops** to provide smooth, real-time effects that enhance player immersion and visually represent game events. These animations include:

- **Rocket Launching:** A step-by-step animation where the rocket ascends from the bottom to the top of the screen.
- **Rocket Crashing:** Simulates a catastrophic failure with the rocket breaking apart and debris scattering across the screen.
- **Fueling:** A progress bar animation visually represents the refueling process.
- **Teleprompters and Text Effects:** Scrolling messages and warnings, such as travel advice and supply alerts, provide a dynamic text-based experience.
- **Reaper Animation:** A character animation moves across the screen to add storytelling elements to the game.

These animations guide the player through critical moments of the gameplay, such as launches, crashes, or critical resource shortages.

---

## Game Flow
1. **Main Menu:** Players select a spaceship and view its stats.
2. **Destination Selection:** Players choose a planet to travel to, viewing travel requirements.
3. **Preparation:** Players refuel and buy supplies as needed to meet trip requirements.
4. **Launch or Crash:** Depending on the preparation, the spaceship either successfully launches or crashes.

---

## Summary
The game creates a dynamic ecosystem where spaceships rely on planets for resources, and planets act as hubs for player interaction. Methods across classes ensure seamless integration, and animations enhance the gameplay experience.

**All ASCII art used in the game is sourced from** [https://www.asciiart.eu/](https://www.asciiart.eu/).
