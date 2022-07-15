namespace ClassesAndInterfaces.Interface;

public class Cow : IAnimal
{
    public string _name { get; set; }
    public string _noise { get; set; }

    public Cow (string name)
    {
        _name = name;
        _noise = "Mu";
    }

    public string GetNoise()
    {
        return $"'{_noise}!'";
    }
}