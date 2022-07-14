using ClassesAndInterfaces.Interface;

namespace ClassesAndInterfaces;

public static class InterfaceImplementation
{
    public static void GetShapes()
    {
        var rectangle = new Rectangle(x: 5.3, y: 2.3, width: 9, height: 5);
        var square = new Square(x: 10, y: 1.5, width: 5, height: 5);
        var circle = new Circle(x: 7.6, y: 3.8, radius: 4);

        var shapes = new IShape[] { rectangle, square, circle };

        foreach (var shape in shapes)
        {
            shape.DrawShape(shape);
    
            Console.WriteLine($"Area of {shape.GetType()} is {shape.GetArea()}");
        }
    }
    
    public static void GetAnimals()
    {
        
        // todo - create classes of different types of animals, implementing a common interface.
        // Create a method to return the animal noise. e.g. Console.Writeline("Woof");
        
        Console.WriteLine("todo - print animal noises!");
    }
}