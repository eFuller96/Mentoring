namespace Solid;

public class Account
{
    public void RentMovie()
    {
        Console.WriteLine("Renting movie");
    }
}

public class ChildAccount : Account
{
    public void RentMovieWithChecksFirst()
    {
        Console.WriteLine("Checking if child is allowed to rent this movie");

        // if child is of age
            base.RentMovie();
        // else
            Console.WriteLine("You are not allowed");
    }
}

public class AmazonPrime
{
    public void GetMovie()
    {
        Account account = new Account();
        account.RentMovie();

        account = new ChildAccount();
        account.RentMovie();
    }
}

// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *
// *

// public class Account
// {
//     public virtual void RentMovie()
//     {
//         Console.WriteLine("Renting movie");
//     }
// }
//
// public class ChildAccount : Account
// {
//     public override void RentMovie()
//     {
//         Console.WriteLine("Checking if child is allowed to rent this movie");
//
//         // if child is of age
//         base.RentMovie();
//         // else
//         Console.WriteLine("You are not allowed");
//     }
// }
//
// public class AmazonPrime
// {
//     public void GetMovie()
//     {
//         Account account = new Account();
//         account.RentMovie();
//
//         account = new ChildAccount();
//         account.RentMovie(); 
//     }
// }
