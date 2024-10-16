using CarRentalSystem;

string uPath = "Users.txt";
string cPath = "Cars.txt";

List<Car> cars = new List<Car>();
List<User> users = new List<User>();

Console.Write("Username: ");
string uName = Console.ReadLine();

Console.Write("Password: ");
string password = Console.ReadLine();