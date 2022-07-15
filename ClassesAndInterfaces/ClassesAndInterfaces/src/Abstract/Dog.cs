namespace ClassesAndInterfaces.Abstract;

public class Dog : Animal
{

    public Dog(string name) : base(name)
    {
    }

    public override string Noise { get { return "Guau"; }}
}