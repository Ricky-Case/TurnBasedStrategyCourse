# Turn Based Strategy Course
A Turn-Based Strategy video game built following along with [this tutorial](https://www.gamedev.tv/courses/unity-turn-based-strategy).
- Note: While I am building this project in Unity 6, the lecture videos were all recorded using Unity 2022.
  - I will likely encounter many discrepancies and bugs. These will be noted in the Progress Log, below.

## Requisite Unity Asset Packs
### GameDev.tv
- Provided Game Assets.

## Progress Log
### Section 1 - Introduction & Setup
#### 2025/01/06
- Lecture 7 Complete
  - Project created and the initial commit and push completed.
  - Set up a custom layout for Unity and cleaned up unnecessary URP template assets.
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
  - Set up basic unit GameObject.
  - Added floor object to scene.
    - Disconnected instructor's custom nodes from the Synty grid material on floor.
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
  - Acquired animation assets from the "Slim Shooter Pack" on [Mixamo](www.mixamo.com) and added them to the project.
- Lecture 9 Complete
  - Created and assigned an animator controller to the unit object.
    - Added the idle and run animations to the animator controller.
  - Added rifle object to unit object.
    - Edited its position and rotation during the run animation to match with the unit's movement.
- Lecture 10 Complete
  - Hooked up the animator controller in code.
- Lecture 11 Complete
  - Set up unit rotation when moving.
- Lecture 12 Complete
  - Set up UnitActionSystem script to handle unit selection and movement control.
- Lecture 13 Complete
  - Added Unit Selection texture to project and implemented it on the Unit prefab.
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
  - Added cinemachine package to project.
  - Set up initial cinemachine virtual camera.
- Lecture 8 Complete
  - Added movement and rotation functionality to the cinemachine camera.
- Lecture 9 Complete
  - Added zoom functionality to the cinemachine camera.
    - This required some extra effort, as the method described in the tutorial is no longer valid.

<b>Section Complete</b>
<br>
<br>

