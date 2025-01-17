# Turn Based Strategy Course
A Turn-Based Strategy video game built following along with [this tutorial](https://www.gamedev.tv/courses/unity-turn-based-strategy).
- Note: While the lecture videos were all recorded using Unity 2022, I have chosen to build this project in Unity 6.
  - I will likely encounter many discrepancies and bugs. These will be noted in the Progress Log, below.

## Requisite Unity Assets
### GameDev.tv
- Provided Game Assets.

## Progress Log
### Section 1 - Introduction & Setup
#### 2025/01/06
- Lecture 7 Complete
  - Project created.
  - Initial commit and push completed.
  - Set up a custom layout for Unity and removed the unnecessary URP template assets.
- Lecture 8 Complete
  - Configured the URP settings.
  - Imported the provided prototype assets.
- Lecture 9 Complete
  - Renamed <i>SampleScene</i> asset to <i>GameScene</i>.
  - Configured the initial Post-Processing settings.

<b>Section Complete</b>
<br>
<br>

### Section 2 - Unit Movement & Selection
#### 2025/01/07
- Lecture 2 Complete
  - Set up basic <i>Unit</i> GameObject.
  - Added a floor object to the main scene.
    - Disconnected instructor's custom nodes from the Synty grid material on the floor.
- Lecture 3 Complete
  - Set up basic unit controller script with rudimentary movement.
  - Using NAMESPACES for scoping, despite the tutorial not using them.
- Lecture 4 Complete
  - Set up rudimentary raycast code for acquiring the cursor's world position every Update.
- Lecture 5 Complete
  - Set up code to return the cursor's world position when the user presses or holds the "move" key.
    - This currently explicitly checks for the mouse left-click.
    - I will change this to use the Unity Input System later, if the tutorial doesn't cover that.
- Lecture 8 Complete
  - Acquired animation assets from the <i>Slim Shooter Pack</i> on [Mixamo](www.mixamo.com) and added them to the project.
- Lecture 9 Complete
  - Created and assigned an animator controller to the <i>Unit</i>> object.
    - Added the <i>Idle</i> and <i>Run</i> animations to the animator controller.
  - Added <i>Rifle</i> game object to the <i>Unit</i> game object.
    - Edited its position and rotation during the <i>Run</i> animation to match with the unit's movement.
- Lecture 10 Complete
  - Hooked up the animator controller in code.
- Lecture 11 Complete
  - Set up <i>Unit</i> rotation when moving.
- Lecture 12 Complete
  - Set up <i>UnitActionSystem</i> script to handle unit selection and movement control.
- Lecture 13 Complete
  - Added Unit Selection texture to project and implemented it on the <i>Unit</i> prefab.
- Lecture 14 Complete
  - Implemented code to ensure that the selection texture is only active on the instance of the Unit prefab which is currently selected.

<b>Section Complete</b>
<br>
<br>

### Section 3 - Grid System & Camera
#### 2025/01/08
- Lecture 3 Complete
  - Reconnected instructor's nodes to the Synty grid material on the floor object.
- Lecture 4 Complete
  - Set up basic grid debugging object, to mark grid position values.
- Lecture 5 Complete
  - Added dynamically assigned grid position text to the grid debug object prefab.
- Lecture 6 Complete
  - Added code to  display the name of every unit located on each grid position.
  - Did some refactoring and commenting for organization, clarity, and readability.
- Lecture 7 Complete
  - Added Cinemachine package to project.
  - Set up initial Cinemachine virtual camera.
- Lecture 8 Complete
  - Added movement and rotation functionality to the Cinemachine camera.
- Lecture 9 Complete
  - Added zoom functionality to the Cinemachine camera.
    - This required some extra effort, as the method described in the tutorial is no longer valid.

<b>Section Complete</b>
<br>
<br>

### Section 4 - Actions & UI
#### 2025/01/10
- Lecture 2 Complete
  - Migrated movement code into its own script.
#### 2025/01/11
- Lecture 3 Complete
  - Added validation to movement, making it impossible to move to any invalid position.
    - Also ensures that a unit moves only to the center of a given grid position.
- Lecture 4 Complete
  - Added visual asset to highlight valid grid positions for unit movement.
#### 2025/01/12
- Lecture 5 Complete
  - Added new <i>Actions</i> namespace
    - Moved UnitActionSystem.cs into this new namespace and edited other scripts to accomodate the change.
  - Slightly refactored the Update function of the MoveAction script for added versatility and readability.
  - Added <i>SpinAction</i> class for testing.
  - Added abstract <i>BaseAction</i> class for all actions to inherit from.
- Lecture 6 Complete
  - Added delegate to the <i>BaseAction</i> class to ensure only one action can be active at a time.
- Lecture 7 Complete
  - Set up basic UI Canvas.
- Lecture 8 Complete
  - Added a button prefab for actions and a container for them.
  - Added classes to handle behavior of action buttons and their container.
- Lecture 9 Complete
  - Refactored to trigger all actions on left click, but only when selected by first pressing the associated button.
- Lecture 10 Complete
  - Added validation to ensure an action can only be triggered by clicking on a valid grid position for that action.
#### 2025/01/14
- Lecture 11 Complete
  - Added UI element to show player which Action is currently selected.
- Lecture 12 Complete
  - Added UI element to tell player when an action is currently active.
#### 2025/01/16
- Lecture 13 Complete
  - Set up a basic action points system to regulate the number of actions a given unit may take in a single turn.
- Lecture 14 Complete
  - Set up the turn system.
    - NOTE: The tutorial calls the <i>UpdateTurnNumberText</i> function in the TurnSystemUI script by listening to the OnTurnChanged event from the TurnSystem script.
      - I expect that calling the function directly is more efficient, but I may alter this to align with the tutorial if issues later arise.

<b>Section Complete</b>
<br>
<br>

### Section 5 - Enemies & Combat
#### 2025/01/17
- Lecture 2 Complete
  - Created a <i>Unit<i> Prefab Variant <i>UnitEnemy</i>.
  - Changed the <i>Characters</i> namespace to <i>Units</i>.
  - Added logic to allow for enemies to have turns.
  - Added a visual element to the UI to alert the player when it is an enemy's turn.