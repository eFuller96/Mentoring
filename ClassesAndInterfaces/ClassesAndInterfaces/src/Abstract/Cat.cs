namespace ClassesAndInterfaces.Abstract;

public class Cat : Animal
{

    public Cat(string name) : base(name)
    {
    }
    public override string Noise { get { return "Miau"; } }
}