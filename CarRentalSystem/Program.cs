using CarRentalSystem;
/*
string uPath = "Users.txt";
string cPath = "Cars.txt";

List<Car> cars = new List<Car>();
List<User> users = new List<User>();
*/

while (true)
{
    Console.Write("Username: ");
    string uName = Console.ReadLine();

    Console.Write("Password: ");
    string password = Console.ReadLine();

    if (uName.ToLower() == "admin" && password.ToLower() == "password")//placeholder for now
    {
        break;
    }
    else
    {
        Console.WriteLine("USER DOES NOT EXIST/PASSWORD IS INCORRECT");
        Task.Delay(1500).Wait();
        Console.Clear();
    }
}
