using CarRentalSystem;
/*
string uPath = "Users.txt";
string cPath = "Cars.txt";

List<Car> cars = new List<Car>();
List<User> users = new List<User>();
*/
bool isStaff = false;
while (true)
{
    Console.Write("Username: ");
    string uName = Console.ReadLine().Trim();

    Console.Write("Password: ");
    string password = Console.ReadLine().Trim();

    if (uName.ToLower() == "admin" && password.ToLower() == "password")//placeholder for now
    {
        isStaff = true;
        break;
    }
    else if(uName.ToLower() == "user" && password.ToLower() == "password")
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

Console.WriteLine(isStaff);
