namespace Delegates2._0;

public static class Calculator
{
    public static int Calculate(int x, int y, Enum type)
    {
        if (type is CalculationType.Add)
        {
            return Add(x, y);
        }
        else if (type is CalculationType.Minus)
        {
            return Minus(x, y);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private static int Add(int x, int y)
        => x + y;

    private static int Minus(int x, int y)
        => x - y;
}