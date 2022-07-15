namespace ClassesAndInterfaces.Interface;

public class Dog : IAnimal
{
    public string _name { get; set; }
    public string _noise { get; set; }

    public Dog (string name)
    {
        _name = name;
        _noise = "Guau";
    }

    public string GetNoise()
    {
        return $"'{_noise}!'";
    }
}