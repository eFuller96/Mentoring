namespace Solid;

public class AreaCalculator
{
    public double GetArea(object shape)
    {
        switch (shape)
        {
            case Rectangle rectangle:
                return rectangle.Width * rectangle.Height;
            case Circle circle:
                return circle.Radius * circle.Radius * Math.PI;
        }

        return 0;
    }
}

public class Rectangle
{
    public readonly double Width;
    public readonly double Height;
}

public class Circle
{
    public readonly double Radius;
}

// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *

// public class AreaCalculator
// {
//     public double GetArea(Shape shape)
//     {
//         return shape.GetArea();
//     }
// }
//
// public abstract class Shape
// {
//     public abstract double GetArea();
// }
//
// public class Rectangle : Shape
// {
//     private readonly double _width;
//     private readonly double _height;
//     
//     public override double GetArea()
//     {
//         return _width * _height;
//     }
//
//     public Rectangle(double width, double height)
//     {
//         _width = width;
//         _height = height;
//     }
// }
//
// public class Circle : Shape
// {
//     private readonly double _radius;
//
//     public Circle(double radius)
//     {
//         _radius = radius;
//     }
//     
//     public override double GetArea()
//     {
//         return Math.PI * Math.Pow(_radius, 2);
//     }
// }
