using CarRentalSystem;
using System.Globalization;

Dictionary<string, Car>cars = new Dictionary<string, Car>();
Dictionary<string, User>users = new Dictionary<string, User>();

FileStream carsFile = File.Open("cars.dat", FileMode.OpenOrCreate);
FileStream usersFile = File.Open("users.dat", FileMode.OpenOrCreate);
//BinaryReader usersReader = new BinaryReader(usersFile);
BinaryReader carsReader = new BinaryReader(carsFile);
while (carsReader.BaseStream.Position < carsReader.BaseStream.Length)
{
    Car addCar = new Car(carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadSingle(),carsReader.ReadBoolean());
    cars.Add(addCar.GetNumberPlate(), addCar);
}

//change it so instead of using the boolean vairable, uses classes instead and returns the users class that has logged in, removing the else if
bool isStaff = false;
List<string> checkBox = new List<string>();
User currentUser = new User("user one", "emailll", "passwords", "a random date", false, checkBox);//this is so the program can run
while (true)
{
    Console.Write("Email: ");
    string email = Console.ReadLine().Trim();

    Console.Write("Password: ");
    string password = Console.ReadLine().Trim();

    if (email.ToLower() == "admin" && password.ToLower() == "password")//placeholder for now
    {
        isStaff = true;
        break;
    }
    else if(email.ToLower() == "user" && password.ToLower() == "password")
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

string date = (new DateOnly(2024,11,13)).ToString();

Console.Clear();
Console.Write($"Welcome back {currentUser.GetName()}");

Task.Delay(750).Wait();

if (isStaff)
{
    bool exit = false;
    while (!exit)
    {
        Console.Clear();
        Console.WriteLine("MENU \n1)Add a new vehicle \n2)Remove a vehicle \n3)Add a new user \n4)Remove a user \n5) \n6)Available vehicles \n7)Close program");
        string userOption = Console.ReadLine().Trim();
        Console.Clear();
        switch (userOption) 
        {
            case "1":
                BinaryWriter carWriter = new BinaryWriter(carsFile);
                Console.WriteLine("Enter vehicle details");
                Console.Write("Make: ");
                string make = Console.ReadLine();
                carWriter.Write(make);
                Console.Write("Model: ");
                string model = Console.ReadLine();
                carWriter.Write(model);
                Console.Write("Year of manufacture: ");
                string yom = Console.ReadLine();
                carWriter.Write(yom);
                Console.Write("Number plate: ");
                string numberPlate = Console.ReadLine();
                carWriter.Write(numberPlate);
                Console.Write("Body type: ");
                string bodyType = Console.ReadLine();
                carWriter.Write(bodyType);
                Console.Write("Cost to rent: ");
                float costToRent = Convert.ToSingle(Console.ReadLine());
                carWriter.Write(costToRent);
                carWriter.Write(true);
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
                BinaryWriter usersWriter = new BinaryWriter(usersFile);
                Console.WriteLine("Enter persons information");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                usersWriter.Write(name);
                Console.Write("Email: ");
                string email = Console.ReadLine();
                usersWriter.Write(email);
                Console.Write("Password: ");
                string password = Console.ReadLine();
                usersWriter.Write(password);
                string dob;
                while (true)
                {
                    Console.Write("Date of birth(DD/MM/YYYY): ");
                    dob = Console.ReadLine();
                    if (dob.Length< 11 && dob[2] == '/' && dob[5] == '/')
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("INCORRECT INPUT! \nPlease make sure you are entering the date in the correct format (DD/MM/YYYY)");
                    }
                }
                usersWriter.Write(dob);
                bool staffCheck;
                while (true)
                {
                    Console.Write("Staff(true/false): ");
                    string staff = Console.ReadLine().ToLower();
                    if (staff == "true")
                    {
                        staffCheck = true;
                        break;
                    }
                    else if (staff == "false")
                    {
                        staffCheck = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option, please enter either true or false");
                    }
                }
                usersWriter.Write(staffCheck);
                List<string> previousRents = new List<string>();
                usersWriter.Write("");
                User newUser = new User(name, email, password, dob, staffCheck, previousRents);
                users.Add(email, newUser);
                break;
            case "4":
                Console.Write("Enter email of user you wish to remove: ");
                string emailToRemove = Console.ReadLine();
                if (users.ContainsKey(emailToRemove))
                {
                    while (true)
                    {
                        Console.Write($"Are you sure you want to delete {emailToRemove}(Y/N): ");
                        string confirmation = Console.ReadLine().ToUpper();
                        if (confirmation == "Y")
                        {
                            users.Remove(emailToRemove);
                            Console.Clear();
                            Console.WriteLine($"{emailToRemove} has been removed");
                            Task.Delay(1500).Wait();
                            break;
                        }
                        else if (confirmation == "N")
                        {
                            Console.Clear();
                            Console.WriteLine($"{emailToRemove} has not been removed");
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
                break;
            case "5":
                
                break;
            case "6":
                GetAvailableVehicles();
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
                GetAvailableVehicles();
                break;
            case "3":
                List<string> rentHistory = currentUser.GetRentHistory();
                if(rentHistory.Count != 0)
                {
                    foreach (string vehicle in rentHistory)
                    {
                        if (cars.ContainsKey(vehicle))
                        {
                            Console.WriteLine($"{cars[vehicle].GetMake()} {cars[vehicle].GetModel()}--{vehicle}");
                        }
                    }
                }
                else
                {
                    Console.Write("No vehicle has been rented in the past");
                    Task.Delay(1500).Wait();
                }
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
carsReader.Close();
carsFile.Close();

Console.WriteLine("PROGRAM ENDED \nSEE YOU LATER :)");

void GetAvailableVehicles()
{
    foreach (KeyValuePair<string, Car> kvp in cars)
    {
        if (cars[kvp.Key].GetAvailability())
        {
            Console.WriteLine($"{cars[kvp.Key].GetMake()} {cars[kvp.Key].GetModel()}--{kvp.Key}");
        }
    }
    Task.Delay(5000).Wait();
}