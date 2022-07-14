namespace ClassesAndInterfaces.Interface;

public class Circle : IShape
{
    private readonly double _radius;
    public double _xCoord { get; set; }
    public double _yCoord { get; set; }

    public Circle(double x, double y, double radius)
    {
        _radius = radius;
        _xCoord = x;
        _yCoord = y;
    }


    public double GetArea()
    {
        return Math.PI * Math.Pow(_radius, 2);
    }
}