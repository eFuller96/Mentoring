# Dependency Injection
**Dependency Injection** (DI) is a design pattern used to implement **Inversion of Control** (IoC). IoC helps you to decouple classes in your application, so you are not forced into depending on concrete implementations. Instead, your application can become loosely-coupled and instead depend on abstractions, such as interfaces. A loosely-coupled application is what we always want to be achieving, it increases flexibility and re-usability of our code. See the diagrams below; the top one is a tightly-coupled application (classes are highly dependent on one another) and the bottom is a loosely-coupled application (classes depend on abstractions)

![direct-dependency-diagram](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image4-1.png)
![inverted-dependency-diagram](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image4-2.png)

## .NET
In .NET 6, there no longer is a `Startup.cs` class where you would normally set up your dependency injections. It is now merged in with `Program.cs`. There are many external libraries in .NET to help you implement IoC, but we will be using .NET's in-built one to create and register our services. `IServiceCollection` holds a collection of registered services. In `Program.cs` you will see this type exposed on `builder.Services`.

## Lifetime
Services can be registered with a lifetime. The below three methods are the different variations:
1. **AddScoped**
    - Scoped lifetime services are created once per request. The same instance is then used for other calls within that same request. This lifetime is good for when you want to maintain state within a singular request.
1. **AddTransient**
    - Transient lifetime services are created every time they are requested. Other calls within the same request will create a new instance. This lifetime works best for lightweight, stateless services.
1. **AddSingleton**
    - Singleton lifetime services are created the first time they are requested (or when configuring the services in Program.cs) and then every subsequent request will use the same instance. This lifetime is memory efficient, but have to watch our for memory leaks building up over time.

### Steps
1. Rename your `Repo` class to `TodoRepository`
    - Rename the adjacent test class too
1. Remove static properties in your `TodoRepository` class
1. Pass though `TodoRepository` to your Controller constructor and initialise an instance, assigning to a private field named `_todoRepository`
    - Fix your code to use this new private field
1. Run your program, hit any endpoint and observe the error you get
     - This is because we haven't registered the repository in the service container yet - how does it know what instance to provide when it sees `TodoRepository` in our constructor signature?
1. Lets fix this error. Register type `TodoRepository` in your service container as a Singleton
    - Hint: this is done in `Program.cs`, see [Microsoft documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0#lifetime-and-registration-options)
    - Note that you do not need an interface yet, your app will work creating a single instance of `ToDoRepository`

Congrats on your first dependency injection! We no longer have a static instance holding our data. But the application is still tightly coupled. The controller is tied into this concerete implementation of `TodoRepository`, so we are still unable to test in isolation without having to care about implementation details of external dependencies.
1. Extract out an Interface for your `TodoRepository` class called `ITodoRepository`
1. Lets decouple our application - use the Interface type rather than our concrete type in the Controller.
1. Run your program, hit any endpoint and observe the error you get
     - See it's the same error as before? This is because we haven't registered the interface in our service container yet - how does it know what implementation to provide when it sees the interface abstraction in our code?
1. Lets fix it - register the `ITodoRepository` interface into your service container with an implementation type, as a `Singleton`
    - Remove the old class registration
1. We no longer need to violate our tests by performing real actions in dependencies when we don't need it. In the Controller tests, mock the dependencies needed
    - Install [NSubstitute](https://nsubstitute.github.io/help.html) nuget package and use this framework to mock our `TodoRepository` class and what the methods return

We are not yet at a point to mock data in our `TodoRepository` tests - but we can improve on it's current state. Lets stub data for it.
1. Change `TodoRepository` so that a constructor takes an `IDictionary<Guid, TodoItem>` to assign to the private field
1. Register that type in our service container as a Singleton so that at runtime, `TodoRepository` can resolve an instance of that type for it's constructor
1. In our test class, in the `SetUp` method we can now initialise our repository with a todo item so each test now starts with some fake data.
1. Fix your tests so that they test in isolation now

Lastly, we can do one more dependency abstraction. The repository is dependent on data storage - our in-memory dictionary. We want the application to be in a state where the repository shouldn't have to care what the storage is or how it manages and stores the data. For example, we may want to switch it out for a file storage, or a database storage easily without having to change unrelated code. This part is decoupling our code - it should be isolated from one another whenever possible.
1. Lets extract out a new class `InMemoryDataStorage`, with an interface `IDataStorage`. The purpose of this class is to handle storing the data, accessing it and modifying it. It is here your dictionary will live. Define all the methods needed in your interface first, and then move out the data logic from repository into your implementation class.
1. Inject this new type and interface into the repository class.
1. We can now mock our data storage properly, instead of stubbing. Use `NSubstitute` to mock dependencies in `TodoRepositoryTests.cs`

### Extra challenge
1. Play around with registering your data storage as Scoped or Transient - test various endpoints 
1. Implement a `FileStorage` from `IDataStorage` - store items in a CSV file and read from/write to it
1. Play around with the ordering of registering your `IDataStorage` implementations - what do you notice?