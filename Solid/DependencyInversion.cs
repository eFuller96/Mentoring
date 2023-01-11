using System.Reflection;

namespace Solid;

public class TodoApplication
{
    private readonly Database _database = new Database();

    public void AddItem(object item)
    {
        _database.Save(item);
    }
}

public class Database
{
    public void Save(object item)
    {
        Console.WriteLine("saving to db");
    }
}

// ****

// booo
// public class TodoApplication
// {
//     private readonly Database _database = new Database();
//     private readonly CsvWriter _csvWriter = new CsvWriter();
//
//     public void AddItemViaDb(object item)
//     {
//         _database.Save(item);
//     }
//
//     public void AddItemViaCsv(object item)
//     {
//         _csvWriter.Save(item);
//     }
// }
//
// public class Database
// {
//     public void Save(object item)
//     {
//         Console.WriteLine("saving to db");
//     }
// }
// public class CsvWriter
// {
//     public void Save(object item)
//     {
//         Console.WriteLine("writing to csv");
//     }
// }

// public class TodoApplication
// {
//     private readonly IStorage _storage;
//
//     public TodoApplication(IStorage storage)
//     {
//         _storage = storage;
//     }
//
//     public void AddItem(object item)
//     {
//         _storage.Save(item);
//     }
// }
//
// public interface IStorage
// {
//     void Save(object item);
// }
//
// public class Database : IStorage
// {
//     public void Save(object item)
//     {
//         Console.WriteLine("saving to db");
//     }
// }
//
// public class CsvWriter : IStorage
// {
//     public void Save(object item)
//     {
//         Console.WriteLine("writing to csv");
//     }
// }