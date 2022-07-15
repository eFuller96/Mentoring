## Class
A class is a blueprint from which you can instantiate objects from. In other words, a class is where you define your implementations and attributes, and you can create objects from this class. Objects are created with constructors, defined in the class.

## Interface
An interface is something a class can implement. You cannot instantiate objects from interfaces. They are used to define methods and properties, but they do not provide the implementations - that is what a class does. With interfaces, we can provide multiple inheritance - a class can implement multiple interfaces. Everything in an interface is public and abstract by default.

## Abstract Class
An abstract class is something a class can inherit from. You cannot instantiate objects from abstract classes. Unlike an interface, they can define both definitions and implementations. They also contain a constructor (used to create objects from), and can contain different access modifiers - e.g. public, private, protected, internal. Classes which inherit an abstract class are often referred to as the "child" or "derived" class. They can only inherit from *one* abstract class, otherwise known as "parent" or "base" class.

The idea of abstraction is to hide internal details and only show the functionality. You can have abstract classes, and abstract methods.
- With classes, the keyword "abstract" indicates that you cannot instantiate objects from this class.
- With methods, the keyword "abstract" indicates that it is the definition only, and an implementation is yet to be provided by the child class. The child class provides the implementation by defining the method with "override".


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