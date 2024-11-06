using CarRentalSystem;
using System.Globalization;
/*
string uPath = "Users.txt";
string cPath = "Cars.txt";
need to add it so the file will output into the list which I can the use to sort certain things e.i if a vehicle is rented 
*/
Dictionary<string, Car>cars = new Dictionary<string, Car>();
List<User> users = new List<User>();



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
                string make = Console.ReadLine();
                Console.Write("Model: ");
                string model = Console.ReadLine();
                Console.Write("Year of manufacture: ");
                string yom = Console.ReadLine();
                Console.WriteLine("Number plate: ");
                string numberPlate = Console.ReadLine();
                Console.WriteLine("Body type: ");
                string bodyType = Console.ReadLine();
                Console.WriteLine("Cost to rent: ");
                float costToRent = Convert.ToSingle(Console.ReadLine());
                Car newCar = new Car(make, model, yom, numberPlate, bodyType, costToRent, true);
                cars.Add(numberPlate, newCar);
                break;
            case "2":
                Console.Write("Enter number plate of vehicle you want to remove: ");
                string carToRemove = Console.ReadLine();
                if (carToRemove.Length == 8)
                {
                    if(cars.ContainsKey(carToRemove))
                    {
                        while (true)
                        {
                            Console.Write($"Are you sure you want to remove {carToRemove}(Y/N): ");
                            string confirmation = Console.ReadLine().ToUpper();
                            if (confirmation == "Y")
                            {
                                cars.Remove(carToRemove);
                                Console.Clear();
                                Console.WriteLine($"{carToRemove} has been removed");
                                Task.Delay(1500).Wait();
                                break;
                            }
                            else if (confirmation == "N")
                            {
                                Console.Clear();
                                Console.WriteLine($"{carToRemove} has not been removed");
                                Task.Delay(1500).Wait();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Input is invalid, please provide a valid input");
                                Task.Delay(1500).Wait();
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("This vehicle does not exist");
                        Task.Delay(1500).Wait();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("Please enter a valid number plate");
                    Task.Delay(1500).Wait();
                }
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
            case "0":
                //output all items in dictionary to test if adding a class just called newCar will cause issues
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
