namespace ClassesAndInterfaces.Abstract;

public abstract class Shape
{
    private double _xCoord;
    private double _yCoord;

    public Shape(double x, double y)
    {
        _xCoord = x;
        _yCoord = y;
    }

    public void DrawShape(Shape shape)
    {
        Console.WriteLine($"Drawing shape {shape.GetType()} at ({_xCoord},{_yCoord})");
    }

    public abstract double GetArea();
}