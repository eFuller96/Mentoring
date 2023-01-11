# Solid Principles

## Single Responsibility
A class or module should have *one* responsibility, and have *one* reason to change

### Tips
- When classes lose cohesion, start to split them. Classes lose cohesion when there are too many instance variables shared among a few methods. It's a sign that those methods and variables can become their own class
- Splitting large functions into smaller ones can help us see opportunity to split into classes

## Open/Closed
Classes should be open for extension but closed for modification. Meaning, as a developer you should be adding things rather than having to modify existing code.

### Tips
- Look for if/switch statements performing different logic based on a type. If it is an ever-growing statement, it's not adhering to Open/Closed principle

## Liskovs Substitution
Parent classes should be easily substituted with their child classes without blowing up the application.

### Tips
- Lot of strongly-typed programming languages implicitly enforce this principle for you
    - e.g. abstract classes and interfaces will force you to provide the implementation instead of relying on the base class implementation
- When some logic is executing that you didn't intend, but the compiler is happy, you're probably breaking this principle!


## Interface Segregation
Clients should not be forced to depend upon interfaces that they do not use

### Tips
- You shouldn't ever have a method throwing `NotImplementedException` - this will break the principle   

## Dependency Inversion
Classes should depend on abstraction but not on concretion

### Tips
- Are you instantiating objects within a class? Pass in things you need when possible, and avoid instantiating them within the class itself

## Source
https://web.microsoftstream.com/video/4c3ba5cc-2a39-4448-9ec9-e5199e9ae000?channelId=b93d2eba-e953-4078-bba6-3fc5ab06f1d9
https://medium.com/mindorks/solid-principles-explained-with-examples-79d1ce114ace