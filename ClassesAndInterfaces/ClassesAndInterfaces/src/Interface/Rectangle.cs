namespace ClassesAndInterfaces.Interface;

public class Rectangle : IShape
{
    private readonly double _width;
    private readonly double _height;
    public double _xCoord { get; set; }
    public double _yCoord { get; set; }

    public Rectangle(double x, double y, double width, double height)
    {
        _width = width;
        _height = height;
        _xCoord = x;
        _yCoord = y;
    }

    public double GetArea()
    {
        return _width * _height;
    }
}