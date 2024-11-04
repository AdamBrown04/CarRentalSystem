using CarRentalSystem;
using System.Globalization;
/*
string uPath = "Users.txt";
string cPath = "Cars.txt";

List<Car> cars = new List<Car>();
List<User> users = new List<User>();

need to add it so the file will output into the list which I can the use to sort certain things e.i if a vehicle is rented 
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

if (isStaff)
{
    bool exit = false;
    while (!exit)
    {
        Console.Clear();
        Console.WriteLine("MENU \n1)Add a new vehicle \n2)Remove a vehicle \n3)Add a new user \n4)Remove a user \n5)Currently rented vehicles \n6)Available vehicles \n7)Close program");
        string userOption = Console.ReadLine().Trim();
        Console.Clear();
        switch (userOption) 
        {
            case "1":
                Console.WriteLine("Enter vehicle details");
                Console.Write("Make: ");
                Console.ReadLine();
                Console.Write("Model: ");
                Console.ReadLine();
                Console.Write("Year of manufacture: ");
                Console.ReadLine();
                Console.WriteLine("Number plate: ");
                Console.ReadLine();
                Console.WriteLine("Body type: ");
                Console.ReadLine();
                Console.WriteLine("Cost to rent: ");
                Console.ReadLine();
                //isAvaliable always is true for new vehicles
                break;
            case "2":
                break; 
            case "3":
                break;
            case "4":
                break;
            case "5":
                break;
            case "6":
                break;
            case "7":
                exit = true;
                break;
            default: 
                Console.WriteLine("INVALID INPUT! \nPLEASE INPUT AN OPTION SHOWN IN THE LIST");
                Task.Delay (2000).Wait();
                break;
        }

    }


}
else
{
    bool exit = false;

    while (!exit)
    {
        Console.Clear();
        Console.WriteLine("MENU \n1)Rent a vehicle \n2)View available vehicles \n3)View previously rented vehicles \n4)Return currently rented car \n5)Exit program");
        string userOption = Console.ReadLine().Trim();
        Console.Clear();
        switch (userOption)
        {
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
                exit = true;
                break;
            default:
                Console.WriteLine("INVALID INPUT! \nPLEASE INPUT AN OPTION SHOWN IN THE LIST");
                break;
        }
    }
}

Console.WriteLine("PROGRAM ENDED \nSEE YOU LATER :)");
