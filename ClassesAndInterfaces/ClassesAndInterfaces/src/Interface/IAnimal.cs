namespace ClassesAndInterfaces.Interface;

public interface IAnimal
{
    public string _name { get; }
    public string _noise { get; }


    public void ListenAnimal(IAnimal animal)
    {
        Console.WriteLine($"Hearing sound from {animal._name}");
    }

    public string GetNoise();
}