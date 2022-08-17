# Unit Tests
We will be using the frameworks NUnit and NSubstitute.

We will also be writing tests to conform to RFC (Request For Comments) standards. [RFC documents](https://www.rfc-editor.org/rfc/rfc9110) contain technical specifications for the internet - it isn't all strict protocol you must follow, some of it is best practices or proposed standards. 
> The Hypertext Transfer Protocol (HTTP) is a stateless application-level protocol for distributed, collaborative, hypertext information systems. This document describes the overall architecture of HTTP, establishes common terminology, and defines aspects of the protocol that are shared by all versions.

You may find you need to alter your solution to get tests passing. That's expected - unit tests pick up things we may have missed or not thought about.

### Steps
1. Switch to your branch `noelia` and update it to have latest master
1. Create a new Unit Test Project called `TodoApplicationTests`
1. Install NUnit and NSubstitute framework for the project
1. Add a test class `RepoTests.cs`
1. In that class, add a test for each of the CRUD operations
    - On GetItems, test that we return all items
    - On GetItem, test that we return for a given Id, we return a matching Item
        - Test unhappy paths too: for an Id which doesn't exist
    - On AddItem, test that for the item passed in, it is added to the static list
        - Test unhappy paths - what if you try add an item with the same Id as an existing one?
    - On UpdateItem, test that for the item passed in, it is updated with passed in resource
        - Test unhappy paths: Id doesn't exist, or it failed to update
    - On DeleteItem, test that for a given Id, it is removed from the static list
        - Test unhappy paths too, for a non-existent Id 
1. Add another test class `TodoListControllerTests.cs`
Here, you will need to mock your Repo class as we want to test in isolation.
    - On `[HttpGet]`:
        - Test that the happy path returns 200 with content
        - Test unhappy path: for an Id which doesn't exist, we return 404 not found
    - On `[HttpGet("{id}")]`:
        - Test that the happy path returns 200 with requested item
        - Test unhappy path: for an Id which doesn't exist, we return 404 not found
    - On `[HttpPost]`:
        - Test that we add the item passed in to our static collection, if successful, check that we return a 201 - this indicates a resource has been created. When using this status code, we should return a link to the resource in the response header under `Location` - this is so we align to the RFC specification. See here for details around [201 created](https://www.rfc-editor.org/rfc/rfc9110#name-201-created) response 
        - Test unhappy path: if an item couldn't be added, it returns 500    
    - On `[HttpPut("{id}")]`:
        - Test that for a successful path, we return 204
        - Test unhappy path: for an Id which doesn't exist, we return 404 not found
    - On `[HttpDelete("{id}")]`:
        - Test that the requested item is removed from the static collection, if successful, check it returns 204
        - Test unhappy paths: if an item couldn't be removed, it returns 500