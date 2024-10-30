using CarRentalSystem;
/*
string uPath = "Users.txt";
string cPath = "Cars.txt";

List<Car> cars = new List<Car>();
List<User> users = new List<User>();
*/

//change it so instead of using the boolean vairable, uses classes instead and returns the users class that has logged in, removing the else if
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

Console.Clear();
Console.WriteLine($"Welcome back "); //once classes are properly implemented will output the users name

Task.Delay(1500).Wait();
Console.Clear();

if (isStaff)
{

}
else
{

}
