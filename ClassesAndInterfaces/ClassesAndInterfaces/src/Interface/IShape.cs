namespace ClassesAndInterfaces.Interface;

public interface IShape
{
    public double _xCoord { get; }
    public double _yCoord { get; }

    public void DrawShape(IShape shape)
    {
        Console.WriteLine($"Drawing shape {shape.GetType()} at ({_xCoord},{_yCoord})");
    }

    public double GetArea();
}