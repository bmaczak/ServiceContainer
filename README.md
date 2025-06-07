# ServiceContainer
A lightweight Service Locator implementation for Unity with scoping support.

[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/bmaczak/ServiceContainer/blob/main/LICENSE)

## Installation

1. Open the Package Manager
2. Click "+" button at the top left
3. Select "Add package from git URL" and paste following URL:

`https://github.com/bmaczak/ServiceContainer.git`

## Introduction

When developing larger games or software with Unity it is not always straightforward how to handle dependencies between different parts of the codebase. Some of the most common solutions are:

- Singletons
- Service Locators
- Dependency Injection

**Singletons** are very easy to set up and use, however as the instances are stored in the concrete class it makes the code very rigid making testing difficult. And I'm not only talking about unit testing (in my experience not so common for games), but also test scenes or scenarios where some systems are replaced with a special or mock implementation, which I personally use a lot.

**Service Locators** give more flexibility as you can ask for interfaces or abstract classes without caring what implementation you are getting back, however dependencies for a system are not clear at a glance and the lifecycle and ownership of the created services might cause problems (I'm aiming to solve the latter).

**Dependency Injection** solves the issues of Service Locators but might be tricky to use with Unity (constructor injection is not feasible for Unity objects), and can add considerable overhead for both development time and performance.

I personally found the Service Locators to be good middle ground for most of the projects I worked on.

### Solving (some of) the problems of ServiceLocators
The naive implementation usually consists of a static ServiceLocator class that stores the registered services in a Dictionary, and then the MonoBehaviours that implement a service register and unregister themselves in Awake / OnDestroy. However there are some issues:
- How do you register services that are not MonoBehaviours? When do you unregister them?
- How can you create services that exist throughout the whole game lifecycle?
- If a service depends on another service how do you ensure that the dependency is registered in time?

For these problems I use the class ServiceContainer.
You can think of the ServiceContainer as a collection of services with the same lifecycle.
Services in a container are installed via installers (classes that implement the IServiceInstaller interface).
The package contains two classes for creating containers:
- The **SceneServiceContainer** class holds a container with a lifecycle matches the containing scenes lifecycle
- The **GlobalServiceContainer** class holds a container that exists throughout the whole game

Both of these classes are MonoBehaviours that get use the IServiceInstaller implementations attached to the same GameObject.

### Creating installers
1. Create a new MonoBehaviour script
2. Make it implement IServiceInstaller
3. Implement the InstallServices(ServiceContainer) method.

Inside the method you can install any object. You can have a serialized field in the inspector for a MonoBehaviour or ScriptableObject, you can create new instances, or even choose different implementations based on some conditions.

### Creating a scene service container
This is useful when you want some services to exist for a single scene.
1. Create an empty GameObject in the scene
2. Add the component SceneServiceContainer to it
3. Add any installers you need

### Creating a global service container
This is useful for services that need to be available for all of the application lifecycle.
1. Create a prefab in the Assets/Resources folder called "GlobalServices".
2. Add the component GlobalServicesContainer to it
3. Add any installers you need

The GlobalServiceContainer uses the RuntimeInitializeOnLoadMethod attribute, this ensures that the global services will be installed before any scene service.