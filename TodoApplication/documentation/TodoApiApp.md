# Todo API Application 

 ## Running a web application 
`launchSettings.json` - this is a file you will see in any [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) application. It is used to describe how to start an application, and holds configuration details. It is for development purposes only, and not used in production. Within the file, you will see a section named `iisSettings` - these are specific to debugging the application under IIS or IIS Express. You will also see a `profiles` section. This contains debug profiles - you will see one named the title of the application, and one named IIS Express. You can select which profile you want to use to determine how to start your application.

### Steps
1. Clone a repository from GitHub: https://github.com/eFuller96/Mentoring.git
1. Switch to a new branch called "noelia"
    - If it already exists, switch to it and `rebase` onto latest `master`. This will ensure all the latest changes on master are in your branch.
1. Open ToDoApplication.sln and run from within Visual Studio 
1. Using postman, get the weather forecast via a HTTP Request 
    1. Check launchSettings.json to see what port the application is configured to run on 
    1. Check TodoListController.cs to see what the endpoint name is 
1. Stop the application 
1. Edit the program so it runs on port number 1234 
1. Re-run and get the weather forecast via HTTP Request again 
1. Stop the application 
1. Re-run using IIS Configuration, and get the weather forecast via HTTP Request again 

## MVC Framework 

[MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0) (Model, View, Controller) is a design pattern, not restricted to .NET. It helps achieve separation of concerns, or in other words, logically separate the work based on the type of work it performs.  

- A Model is responsible for representing the state of the application.  
- A View is responsible for presenting content to users through a UI.
- A Controller is responsible for handling the user interactions. It is the entry point to the application, receiving requests and returning responses.  

![mvc-diagram](https://miro.medium.com/max/1400/1*dmXICCnEuM8toPGdwsJ-Xg.png)

We will explore attributes belonging to the [MVC namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc?view=aspnetcore-6.0) in this tutorial. These attributes configure the behavior of web API controllers and action methods.

### Steps
1. Within our web app, we will be using the ASP.NET Core MVC framework. Check the first line within TodoListController.cs and temporarily delete it. Observe the errors, then put it back 
1. Create a Model for a TodoItem 
    1. Add new directory called Models within the project 
    1. Add a new class called TodoItem 
    1. Add the following properties with these names and types: 
        - Id (long) 
        - Name (string) 
        - IsComplete (bool) 
1. Alter the Get() Method in the Controller to return a collection of TodoItem 
1. Add a new POST endpoint to add an item to the in-memory collection 
    1. Test out your new endpoint in Postman 
    1. Go GET your items – check your new item has been added 
    1. Hint: [FromBody] is a binding source attribute – It binds the request body to the parameter 
1. It currently returns a 200 status code when we add an item. Let's change it to a 204 No Content status code. 
    - Hint: take a look at [IActionResult](https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0) 
        - NoContent() is a method available to us through the [ControllerBase](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-6.0) – what our controller is derived from. 
1. Let's enforce some model validation. Define the Name property of the TodoItem as required. 
    1. Try make a POST request now without giving a name in the request body. Observe the response. 
        - Notice how we didn’t need to manually return a BadRequest object from within our AddItem method. This is because the [ApiController](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0#automatic-http-400-responses) attribute enforces model validation and handles returning a 400 response for us. 
1. All of our items have id: 0. Lets change that – assign them a GUID (Globally Unique Identifier)
    - Check out the .NET [GUID](https://docs.microsoft.com/en-us/dotnet/api/system.guid.newguid?view=net-6.0) type
1. Add a new GET method to retrieve just 1 item by id. 
1. Finish completing the [CRUD](https://www.codecademy.com/article/what-is-crud) operations by adding a PUT and a DELETE
    - Allow consumers of the API to update completing the task - e.g. setting IsComplete to true
    - Allow consumers of the API to delete an item from their list by passing through an ID number
        - Extra: create a new property in ToDoItem class to hold the position number of the item in the list, and increment as you add new ones. So the first one is assigned `1`, then `2`, etc.