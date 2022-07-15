namespace ClassesAndInterfaces.Abstract;

public abstract class Animal
{
    public string Name { get; set; }
    public abstract string Noise { get; }    //abstract: child classes MUST provide implementation

    public Animal(string name)
    {
        Name = name;

    }

    public void ListenAnimal(Animal animal)
    {
        Console.WriteLine($"Hearing sound from {animal.Name}");
    }

    public string GetNoise()
    {
        return $"'{Noise}!'";
    }
}