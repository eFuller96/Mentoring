# Asynchronous Programming
Asynchronous programming allows us to write non-blocking code so that it runs concurrently. 
It is incredibly important to write asynchronous code for both client application and server applications. From a user POV, they do not want to be staring at an unresponsive screen whilst something is happening in the background. We need to keep UI responsive. As for servers, we don't want it to be inefficient processing 1 request at a time. It needs to be scalable, and handle hundreds (or even thousands) of requests.

## Under the hood (ASP.NET Core)
There is a pool of available *threads* available to the .NET runtime and it will use whatever is free. Available threads depends on the machine resources.
> A thread is an execution path that can proceed independently of others

As new API requests come in, an available thread will be allocated to the request to handle the work. These available threads come from a thread pool. Once there are no more threads left to allocate, the next request to come in will be *blocked* and now has to wait for one of the first requests to finish to free up a thread.

![thread-pool-diagram](https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/october/images/dn802603.cleary_figure1_hires(en-us,msdn.10).png)
![thread-pool-diagram2](https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/october/images/dn802603.cleary_figure2_hires(en-us,msdn.10).png)

> This is why we have to be careful with static variables! Static means it is shared across the different threads. They can all access it which is dangerous, one can write the value whilst the other is about to read... it can have unexpected side effects. That's why we should try avoid static in our code. But also lifetime scoped objects, we need to consider what should be shared across requests.

Using asynchronous programming resolves this problem. Threads aren't always processing work, they may be blocked by an I/O operation (e.g. making a call to a database, and waiting for a response). Instead of waiting, we could free up that thread and allow it to process the next request, and come back to the original request when we get a response back from the database.

This allows us to handle lots more requests and to not block threads.

## Task Parallel Library
TPL is a .NET library which helps simplify working with concurrent and asynchronous code. At a high-level, we do not need to worry about thread resources and how/when they are allocated. It is handled for us with this, but it's still good to know the "under the hood" stuff to understand what asynchronous really means in .NET programming.

With the Task Parallel Library (TPL), we can share the resource more efficiently, by reusing threads. We can be alerted when some work is completed by subscribing to it.

Provides us keywords such as `async` and `await`.

### Task
A `Task` represents an asynchronous operation. It resembles a thread. `Task<TResult>` returns a value which we can access with the `.Result` property.

We can `await` Tasks to free up a thread, and allow it to pick up other work.

## Tasks
1. Pull down [this git repository](https://github.com/eFuller96/AsyncSubway.git)
1. There are two separate web API applications. Run the SynchronousSubway and use the endpoint `/Subway [POST]` with a sandwich request. Keep an eye on your IDE output console, and note how long the request took in total (Postman will tell you)
1. Now run AsyncSubway project and use the endpoint `[POST] /Subway` with the same sandwich request. Observe the console. Note how long the request took.
1. Add a new functionality in Subway! Customers now want dessert. Lets bake some cookies, which takes 20 seconds. Customers should be able to opt into this in their subway request.
    1. Start with a synchronous approach first in `SynchronousSubway` project, then try the asynchronous solution in `AsyncSubway`.

Todo Application
1. Make all your endpoints asynchronous

## Resources
- [Task parallel Library](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-parallel-library-tpl)
- [Task-based asynchronous programming](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming)
- [Asynchronous Programming with Async and Await in ASP.NET Core](https://code-maze.com/asynchronous-programming-with-async-and-await-in-asp-net-core/)
- [Back to Basics: Efficient Async and Await](https://www.youtube.com/watch?v=R3u4Gb7mazE)