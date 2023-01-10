using System;

namespace ClassesAndInterfaces.Abstract;

public class Circle : Shape
{
    private readonly double _radius;

    public Circle(double x, double y, double radius) : base(x, y)
    {
        _radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Math.Pow(_radius, 2);
    }
}