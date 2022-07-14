namespace ClassesAndInterfaces.Abstract;

public class Square : Shape
{
    private readonly double _width;
    private readonly double _height;

    public Square(double x, double y, double width, double height) : base(x, y)
    {
        _width = width;
        _height = height;
    }

    public override double GetArea()
    {
        return _width * _height;
    }
}