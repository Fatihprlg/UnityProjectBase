# Abstract
 This project is a template containing common classes and packages used to create hyper-casual games in Unity. It includes all chapter, management, and scene details except for game-specific mechanics. This provides developers with a wide range of motion, making the development process easier and faster. In this project, we aim to increase developer performance by designing ease-of-use into the Unity interface. This includes editor and interface arrangements within the project, ready-made code for scene management, ready-made formats for level design and a recording system, a data recording system to manage user data, an input manager, an initializer, screen and interface templates and managers for the game, a game manager, IoC, a tutorial template, and a user manual.
# Method
I prepared this project as a template that combines different modules, which were designed according to the Model View Controller (MVC) architecture and include features common to most games. In this architecture, the data and interface parts are divided into sections and the divide-and-conquer technique is used. Game and UI development typically involve waiting for a user's input or other triggering condition, sending a notification about these events to the appropriate place, deciding what to do in response, and updating the data accordingly. These actions demonstrate the compatibility of these applications with MVC (Costa, 2016). MVC treats data channels and presentation channels slightly differently, creating a structure similar to the Unity. Here, the 'Model' contains data classes, the 'View' contains presentation classes, and the 'Controller' contains management and 'Game Logic' classes.

The modules included in the template are as follows: Game Controller, Upgrade Controller, Pool System, Particle Pool System, Audio Manager, Currency Manager, Vibration Manager, Initializer, Data Handler, Level Controller, Tutorial Handler, PopUp Handler, Inversion of Control (IoC) Container, Camera Controller, Example Main Scene, Post Processor, Screen Controller, Documentation, and Scene Controller. Also used Demigiant's DOTween package as a third-party package.

# Modules

## Game Controller
The 'Game Controller' element controls the flow of the game and is essentially inspired by the 'state' design pattern. It runs the 'update' functions of all controllers in the scene, taking into account the specified order and the state of the game. It also manages in-game state changes and events that occur during these changes. The controllers that the 'Game Controller' component wants to run are dragged and dropped onto the 'Game Controller' component via the editor. If you want the controller to automatically map and call all controllers in the scene, you can check the 'autoMapControllers' box in the editor window. In this way, the 'Game Controller' will display all classes on the stage that are created by inheriting from the 'Controller Base' class.
The functions that are defined in the 'Controller Base' class will be mapped and managed by the 'Game Controller'. Since the 'Game Controller' works like a 'State Machine', it sends the 'Current State' variable as a parameter to the functions to be used and allows the functions to perform the desired operations in desired situations.
To add events during state changes, you can add them to the 'onStateChange' events in the 'Game Controller' component. The method to be added here must take a parameter from the 'Game States' enum. This information can be used to run the code if the desired state is reached during state change.
To change game states, you should use the ChangeState method from the object of the 'Game Controller' element `_gameController.ChangeState(GameStates state)`.
Statuses included in 'Game States':

- **Main:** Showing the main screen before the player starts playing the game. In this case, the main menu, development buttons, training, etc. could be included.
- **Game:** The state in which the game is played. All processes running in the game, such as 'movement' and 'path follow', occur in this case.
- **End:** It is the end screen of the game. This situation is again in the 'Game Controller', and it can be called with the EndState() function. This method takes the 'isPlayerWin' parameter whether the player has successfully completed the level and displays the appropriate 'Win Screen' or 'Lose Screen' on the screen.

## Upgrade Controller
The 'Upgrade Controller' element controls in-game enhancements. It includes the upgrade events that will occur when the upgrade buttons on the game main screen are clicked. To use it, you need to call the appropriate upgrade function by dragging the 'Upgrade Controller' item to the buttons in the 'Upgrade Screen'.
It maintains a list of type 'Upgrade Model' in the 'Upgrade Controller'. This model is a class that holds the data of the upgrade type. It can be used to add new upgrade types through the editor by depending on the index in the upgrade functions to be made.
It is possible to pull all the information about the upgrade from the 'upgrade model' object. The 'Upgrade Controller' class is designed to create only one interface, as upgrade functions can vary from game to game.

## Level Controller
The 'Level Controller' class is responsible for saving, reading, and creating sections in a 'json' file of type 'Level Model'. To activate objects on the stage in a section, their data should be added to the 'Level Model' class. The 'Level Controller' uses the 'Level Adapter' class as a helper class when loading and saving partitions. This class contains functions that are executed when saving or restoring objects in the scene.
To make the editor window more user-friendly, buttons have been added for recording, editing, loading, and cleaning the scene. Initially, there are two types of items available: 'Pool Items' and 'World Items'. These types are data classes derived from the 'Item Data Model' class.
The 'Pool Item Data Model' activates the desired objects from the 'Multiple Pool' models in the scene at the saved positions. To register these objects, the 'Pool Item Model' class should be added to the objects that need to be registered in the scene. The 'Level Adapter' then takes the objects you have activated on the stage and adds their data to the json file.
The 'World Item Data Model' can be used for all objects in the scene that are not in the object pool. To use it, the 'World Item Model' component must be added to the objects.
To load a level, you should press the 'Clear Scene' button to clean the scene first. Then, you can load one of the sections that were created before by entering the section index that you want to load. After making the desired changes in this section, you can press the 'Override Level' button to overwrite the same section. This way, desired changes can be made and saved in an existing section. If you want to see the features of the active section, all information about this section can be seen under the 'Active Level' tab. The sections can be managed by calling the functions mentioned in the code.

## Screen Controller
The 'Screen Controller' class manages a 'boolean' value named 'autoChangeScreen' to trigger the list of screens, the active screen, and automatic switching of screens. If this 'boolean' value is selected from the editor, a method involving screen changes is added to the 'onStateChange' events of the 'game controller' element. This way, the appropriate screen is automatically loaded at each state change.
The 'MainScreenIndex' value in the editor shows the first screen that will open when the game starts. The screens are rotated sequentially from this index. The screens before the main index are the screens that will be shown in certain situations and are constantly active, such as the Tutorial or PopUp screens.
The screens available by default are:
- **Main Screen:** It is the main menu of the game. In the 'upgrade screen', player money, department information, and other information are available. Player money and episode information are automatically retrieved from the 'Player Data Model' object.
- **Game Screen:** It is the screen that appears while playing the game. Player money and chapter information are also displayed here and adjusted automatically.
- **End Screen:** It is the section end screen. If the player successfully completed the level, it will show the 'Win Screen', otherwise it will trigger the 'Lose Screen' element.
- **Tutorial Screen:** It is the screen element that contains the tutorials added to the game. It stays blank if no training is added.

## Camera Controller
 The Camera Controller holds the Main Camera and all other virtual camera elements in the scene under the Setup object on the scene. To switch between cameras, you can call the ChangeCamera(int cameraIndex) method of the Camera Controller class.
Scene Controller: The Scene Controller manages scene transitions and refresh operations. It shows the loading screen while loading scenes asynchronously. It is a singleton object that can be used between scenes, allowing you to access the desired section and perform scene transitions.

## Pool System
 The Pool System implements the Pool Pattern, which is one of the most commonly used design patterns in game development. In this system, a pool is defined before the scene is started and the objects are inactive on the scene. Then, when an object is needed in the game, it is called from the pool and used. This helps reduce the burden on the processor of constantly creating and destroying objects that will be used more than once. It is a pattern used for objects that are very similar to each other in the scene, such as obstacles on the road, gun bullets, or objects to be collected.
You can access existing object pools by opening the Pools object in the scene hierarchy. The Multiple Pool Model contains more than one pool as a child object. It manages the pool objects that are inserted into it and keeps the index information of each added pool under the Multiple Pool Model object. When an object is called from within the pool, it is called as `_multiplePoolModel.GetDeactiveItem<ObjectType>(poolIndex)`. There are also functions for calling random objects.
To create a pool, you can create a GameObject under the pool object in the scene and add the Multiple Pool Model component to this object. Later, you can add desired pools as children to this object. The purpose of the Multiple Pool Model class is to separate pools by object type. For example, you can call different obstacle/Prop types from the Multiple Pool in the code by giving the id information of the pool. By throwing the prop elements in the scene into PropPools and the obstacle pools into Obstacle Pools, you can easily manage the different types of pools.

## Particle Pool System
To create a particle pool system, you need a "Particles Controller" object that manages and holds the particle pools in the scene.
1. Create an empty object and make it a child of the "Particles Controller" object.
2. Add the "Particle Pool Model" component to this object.
3. Add the "Particle Base Model" component to each object that will be added to the pool so that the pool can see the objects.
4. Add the "particle" objects as children to the pool object.
5. Click the "GetDeactiveItems" button in the "Particle Pool Model" component to add the particle objects to the pool list.
6. Drag and drop the pool object to the "Particle Pools" list in the "Particles Controller" object.
7. To call the pool object in the code, call the "SetParticle" method from the "static" "instance" element of the "particle controller" element. This method has two applications:
 - In the first application, the pool index of the particle object to be called, the world position, and the type of vibration to be given together with the particle should be given as parameters.
 - In the second application, in addition to these properties, it is possible to give a rotation to the particle in terms of "Quaternion".

## Audio Manager
The 'Audio Manager' is a 'MonoBehaviour' class that contains three 'Audio Sources' in the scene. It also has a 'Scriptable' of type 'Audios' that stores an object keeping a list of 'Audio Model' types. You can add 'Sound Effects' (SFX) to this list and enter necessary information for each one. To play sound effects, you can simply call the 'PlaySFX' method, and to play music, you can use the 'PlayMusic' method. Additionally, you can mute the sound or adjust the volume from the 'Audio Manager'.

## Currency Manager
The 'Currency Manager' is a class that manages monetization, throttling, and displays these events. It uses the 'Player Data Model' class to save and permanently store this information. Currency changes can be managed using the 'UpdateCurrency(int amount)', 'UpdateCurrencySmooth(int amount)', and 'DecreaseCurrency(int amount)' methods. Events that should occur during these currency exchanges can be added to the 'OnCurrencyUpdate' event listener. Additionally, the 'Currency Manager' uses the 'Observer' design pattern. The 'notify' method can be used in 'currency update' events by adding 'Observer' objects to this list in 'Initialize'. For example, 'CurrencyViewModel' objects subscribe to this item, monitor the changes, and reflect them on the screen.

## Vibration Manager
The Vibration Manager is a static class that can be accessed from anywhere in your game. Here's how you can use it:
Call the static methods in the Vibration Manager and use the "Vibration Types" enum to give the desired vibration.
To ensure that the vibration does not start if the given second has not passed since the previous vibration, give a threshold value to the method.
Use the Vibration Manager to turn vibration on or off.

## Input Manager
The 'Input Manager' uses a 'MonoBehaviour' object which is added as a field and an object instance is created (it can also be used as '[SerializeField]'). All operations related to the 'pointer' can be performed on this object. By using the 'OnPointer', 'OnPointerDown' and 'OnPointerUp' event listeners, events that will occur during inputs can be specified.
This class keeps track of the current location of the 'pointer', the subject it was pressed on, and the 'delta' information. In order for it to work, the 'PointerUpdate()' function must be called in the 'Update' function in a 'MonoBehaviour' class under the desired condition.

## Initializer
The 'Initializer' class allows us to manage the 'default execution order' definition in Unity using a different perspective. When Unity is started, its self-defined 'Event Functions' are executed in an order that may differ from device to device if not defined manually. This can cause reference drops if the order of the working class changes first in the dependencies established between classes.
The 'Initializer' class solves this problem by using the 'Initialize' method of the 'IInitializable' interface. The user sorts the classes derived from the 'IInitializable' class in the scene and for which the 'Initialize' operation is requested, into the component as desired, and the 'Initializer' class initializes these classes at once when the scene is loaded. The 'InitializeOnAwake' option should be left on to 'initialize' the items added to the list, respectively, within the 'Awake' function. It is recommended that this option not be turned off except in special circumstances.

## Data Handler
The 'DataHandler' class is responsible for permanently storing user data. It stores objects derived from the 'Data Model' class as binary files. To access any data from the code, the 'Data' property, which is 'static', is used. For example, 'PlayerDataModel.Data.requested Information' can be used to access 'Player' data. To save changes made to data, the 'DataHandler' automatically saves data using the 'Application Focus' event function within itself. However, the 'DataHandler' editor window provides options to manually save or clear data. The editor window can be accessed from the menubar as My Game Lib>DataHandlerWindow.
To create a new 'data model', the user must enter the name of the new data model in the box above the 'Create New Data Model' button and click the button. The new 'data model' must then be initialized like other models in the 'Data Handler' class and added to the 'Save' function.
The 'GetMoney' button in the editor window can be used to add money while testing. The amount of money to be added must be entered in the box above the button and the button must be pressed.

## Tutorial Handler
The 'Tutorial Handler' object is a template that contains several tutorials used to teach players certain parts of the game. The desired features can be adjusted from the 'Lesson Model' component in the course. The 'GetLesson' generic method of the 'TutorialHandler' is used to call the lessons.
The default lessons include the 'Click Button Lesson', which creates a tutorial showing a button to be pressed, and the 'Click Object Lesson', which creates a tutorial targeting an object that needs to be clicked. Users can also create new lessons by inheriting from the 'LessonModel' class and placing them under the Resources>LessonPrefabs folder as prefabs.

## Pop Up Handler
The 'Pop Up Handler' class manages the 'Pop Up View Model' and produces 'Pop Up' screens according to the given title, text, and button values. To use it, an object containing a 'Pop Up Handler' component is created on the scene, and a 'Pop Up View Model' is created under a 'Canvas,' and a 'Pop Up Screen' containing the necessary features is added under it (available by default in the 'Main Scene'). Then, the 'Singleton' 'Pop Up Handler' class can be used to call this screen from within the code. The 'ShowPopUp' function can be used to call a 'pop up' in the classroom, and the 'HidePopUp' function can be used to hide the 'Pop Up' screens displayed on the screen. If multiple 'Pop Up' screens are summoned in a row without hiding the previous one, the 'Handler' will throw these 'pop ups' into a queue structure and will open one after the other as the 'pop up' on the screen is hidden. The 'Pop Up Handler' class also provides an option to add a callback function that will be called when the user clicks on a button in the 'pop up' screen.
In the 'Pop Up Screen' object, there are two 'Pop Up' button objects. These buttons are used to execute the callback functions added to the 'Pop Up Handler' class when clicked. When adding a new 'pop up' screen, the button names should be changed according to the desired button texts, and the 'ButtonAction' event of the 'ButtonComponent' script on the button should be assigned to the corresponding callback function.

## Inversion of Control (IoC)
Inversion of Control (IoC) is a design pattern used to manage dependencies in software architecture, including game development. In Unity, it is adapted to the MonoBehaviour class structure and consists of a container that injects dependency, a builder struct that pulls and forwards dependencies in the scene, and a context struct that controls the builders.
There are three basic ways to assign dependencies in Unity. The first and most commonly used is to describe the addictive elements in the scene as '[SerializeField]' and drag-and-drop them through the editor. The second way is to use Unity-provided functions such as 'GetComponent', 'FindGameObjectWithTag', and 'FindObjectOfType'. The third way is to create a new object instance with the 'new' keyword for classes that are not 'MonoBehaviour'. However, this last method is not suitable for the Unity engine and cannot be used for 'MonoBehaviour' classes.
The IoC structure uses a dictionary to keep dependencies and places them where needed. This module is the coded version of the IoC structure by integrating it into the Unity system. It automatically takes all 'MonoBase' classes in the scene and stores them in a dictionary. Instead of creating any dependency, the user qualifies this dependency as `[Dependency]` and attaches the object instance of this dependency to that field as the IoC falls.
To use this feature, the user needs to add the 'Context' object and the 'Builder' object to the scene. After this step, the builder automatically defines and stores the 'MonoBase' classes on the stage every time it 'recompiles'. The fields that the user wants to 'inject' in the code must be qualified as `[Dependency]`. To initialize the code or before using the dependency, the user can call the 'extension' method this.Inject(). If there is more than one instance of an object in a scene and the user wants to get a specific one, he can get the reference of the object instance he wants in the form of `[Dependency("Game Object Name")]`.

## Post Processor
Post processing is a commonly used technique in Unity to apply visual effects and make adjustments to the image by giving some effects to the camera. The 'PostProcessor' module includes two pre-made image profiles designed for this purpose, which can be easily changed via the editor. It provides a standard and simple image profile, called 'Classic', and a more advanced profile, called 'Contrast', which includes more color settings and effects. To select one of these profiles, simply press the corresponding button in the editor. The 'PostProcessor' module automatically inserts the selected profile into the 'volume'. Additionally, custom profiles can be added to the 'volume', which will automatically appear as a new button in the editor.
When adjusting the profile, different images can be obtained by changing the 'lookup texture' (LUT) picture in the 'Color Lookup' section. The LUT is used as the 'Color Correction LUT', which is a texture used to perform color grading in a post-processing effect. Instead of manipulating individual color channels via curves, as with color correction curves, only a single texture is used to produce the corrected image. The appropriate image can be obtained by using the textures located under the 'Assets/Base/PostProcess/Resources/LUT' folder in the project.

## Documentation
The 'Documentation' module provides an editor window that displays module descriptions for the template. To access this window, click on the 'Documentation' option under the 'My Game Lib' tab in the top taskbar.

## Other Features
- The 'Extensions' module contains useful extensions that can be used during game development.
- The 'Helpers' module is a combination of different types of functions that can be useful.
- The 'CustomHierarchyItem' module allows for customizing the appearance of objects in the hierarchy by adjusting their color, icon, and text. 
- The 'EditorButton' attribute can be assigned to functions in code to turn them into editor buttons, which can be used to execute these functions in the editor whenever needed.

# Bibliography
- Blanco. (2021). Writing a minimal IoC Container in C#. encora. https://www.encora.com/insights/writing-a-minimal-ioc-container-in-c. Date of access: 22.02.2023
- Costa. (2016). Unity With MVC: How to Level Up Your Game Development. toptal. https://www.toptal.com/unity-unity3d/unity-with-mvc-how-to-level-up-your-game-development. Date of access: 22.02.2023
- Dorokhina. (2022). Promoting game flow in the design of a hyper-casual mobile game.
- Statista. (2020). App Store and Google Play mobile game downloads worldwide 2018-2019.
- Giardini. (2014). DOTween (HOTween V2). Demigiant. http://demigiant.com/plugins/dotween/index.php. Date of access: 22.02.2023
- Daybson. (2019). HyperCasual. https://github.com/daybson/HyperCasual. Date of access: 22.02.2023
- Ermiş. (2022). HyperTemplate. https://github.com/SinanErmis/HyperTemplate. Date of access: 22.02.2023
- Mandala. (2012). IoC Container for Unity3d. https://www.gamedeveloper.com/programming/ioc-container-for-unity3d. Date of access: 22.02.2023
- Unity Technologies. (2023). Script Execution Order Settings. Unity Technologies. https://docs.unity3d.com/Manual/class-MonoManager.html. Date of access: 22.02.2023
- Unity Technologies. (2023). Color Correction Lookup Texture. Unity Technologies. https://docs.unity3d.com/540/Documentation/Manual/script-ColorCorrectionLookup.html. Date of access: 22.02.2023
