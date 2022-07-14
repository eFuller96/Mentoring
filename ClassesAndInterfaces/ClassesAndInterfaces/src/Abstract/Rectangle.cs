namespace ClassesAndInterfaces.Abstract;

public class Rectangle : Shape
{
    private readonly double _width;
    private readonly double _height;

    public Rectangle(double x, double y, double width, double height) : base(x, y)
    {
        _width = width;
        _height = height;
    }

    public override double GetArea()
    {
        return _width * _height;
    }
}