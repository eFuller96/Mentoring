using System.Diagnostics;

namespace Solid;

public class Library
{
    private static List<Book> _bookInventory;
    private static bool _computerInUse;

    public Book GetBook(string name)
    {
        return _bookInventory.First(b => b.Name == name);
    }

    public void ReturnBook(Book book)
    {
        _bookInventory.Add(book);
    }

    public void RentComputer(TimeSpan duration)
    {
        Console.WriteLine($"Using library PC for {duration.TotalMinutes} minutes");
        _computerInUse = true;

        var timer = new Stopwatch();
        timer.Start();
        while (timer.ElapsedMilliseconds != duration.Milliseconds)
        {
            _computerInUse = false;
            timer.Stop();
        }
    }
}

public class Book
{
    public string Name { get; set; }
    public string Author { get; set; }
}





















//
//
//
// public class LibraryGood
// {
//     private static List<Book> _bookInventory;
//
//     public Book GetBook(string name)
//     {
//         return _bookInventory.First(b => b.Name == name);
//     }
//
//     public void ReturnBook(Book book)
//     {
//         _bookInventory.Add(book);
//     }
// }
//
// public class ComputerRental
// {
//     private static bool _computerInUse;
//
//     public void RentComputer(TimeSpan duration)
//     {
//         Console.WriteLine($"Using library PC for {duration.TotalMinutes} minutes");
//         _computerInUse = true;
//
//         var timer = new Stopwatch();
//         timer.Start();
//         while (timer.ElapsedMilliseconds != duration.Milliseconds)
//         {
//             _computerInUse = false;
//             timer.Stop();
//         }
//     }
// }
//
