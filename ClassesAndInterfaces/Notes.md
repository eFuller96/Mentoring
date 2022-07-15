## Class
A class is a blueprint from which you can instantiate objects from. In other words, a class is where you define your implementations and attributes, and you can create objects from this class. Objects are created with constructors, defined in the class.

## Interface
An interface is something a class can implement. You cannot instantiate objects from interfaces. They are used to define methods and properties, but they do not provide the implementations - that is what a class does. With interfaces, we can provide multiple inheritance - a class can implement multiple interfaces. Everything in an interface is public and abstract by default.

## Abstract Class
An abstract class is something a class can inherit from. You cannot instantiate objects from abstract classes. Unlike an interface, they can define both definitions and implementations. They also contain a constructor (used to create objects from), and can contain different access modifiers - e.g. public, private, protected, internal. Classes which inherit an abstract class are often referred to as the "child" or "derived" class. They can only inherit from *one* abstract class, otherwise known as "parent" or "base" class.

The idea of abstraction is to hide internal details and only show the functionality. You can have abstract classes, and abstract methods.
- With classes, the keyword "abstract" indicates that you cannot instantiate objects from this class.
- With methods, the keyword "abstract" indicates that it is the definition only, and an implementation is yet to be provided by the child class. The child class provides the implementation by defining the method with "override".

## Fields 
Fields should be kept private to a class and accessed via get and set properties. Properties provide a level of abstraction allowing you to change the fields while not affecting the external way they are accessed by the things that use your class.

## Properties
Properties are used to expose fields, allowing you to "set" and "get" it's value. Properties are also known as accessors, and the "get" and "set" are called peoperty accessors.
`value` is a keyword in C#, it is like a parameter for the set method.
```
public int Id
{
    get
    {
        return _id;
    }
    set
    {
        _id = value;
    }
}
```

## Access Modifiers
An assembly is a .ddl or.exe that can contain a collection of APIs that can be called by applications or other assemblies.

### public
The type or member can be accessed by any other code in the same assembly or another assembly that references it. The accessibility level of public members of a type is controlled by the accessibility level of the type itself.

### private
The type or member can be accessed only by code in the same class or struct.

### protected
The type or member can be accessed only by code in the same class, or in a class that is derived from that class.

### internal
The type or member can be accessed by any code in the same assembly, but not from another assembly. In other words, internal types or members can be accessed from code that is part of the same compilation.

### protected internal
The type or member can be accessed by any code in the assembly in which it's declared, or from within a derived class in another assembly.

### private protected
The type or member can be accessed by types derived from the class that are declared within its containing assembly.


## Resources
https://www.geeksforgeeks.org/c-sharp-class-and-object/?ref=lbp

## Other notes

private string _name;

public string Name
{
    get { return _type; }
    set { _type = value; }
}

IS THE SAME AS

public string Name { get; set; }
https://www.geeksforgeeks.org/c-sharp-class-and-object/?ref=lbp
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers