namespace ClassesAndInterfaces.Interface;

public class Cat : IAnimal
{
    public string _name { get; set; }
    public string _noise { get; set; }

    public Cat (string name)
    {
        _name = name;
        _noise = "Miau";
    }

    public string GetNoise()
    {
        return $"'{_noise}!'";
    }
}