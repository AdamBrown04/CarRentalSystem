﻿//link to GitHub page with readme: https://github.com/AdamBrown04/CarRentalSystem
//TL:DR of readme is to use --a pswrd when using the program for the first time in command line so you can add an account to the system

using CarRentalSystem;
using System.Globalization;
using System.Runtime.InteropServices;
//using dictionaries as it allows me to store a key reducing the amount of time taken to search for specific instances of each class
//key is the number plate for cars and the email for users
Dictionary<string, Car>cars = new Dictionary<string, Car>();
Dictionary<string, User>users = new Dictionary<string, User>();
//using binary files instead of JSONs as the system could be storing thousands of instances for both classes, creating massive file sizes if using JSONs
FileStream carsFile = File.Open("cars.dat", FileMode.OpenOrCreate);
FileStream usersFile = File.Open("users.dat", FileMode.OpenOrCreate);

BinaryReader carsReader = new BinaryReader(carsFile);
while (carsReader.BaseStream.Position < carsReader.BaseStream.Length)
{
    Car addCar = new Car(carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadSingle(),carsReader.ReadBoolean());
    string email = carsReader.ReadString();
    if (email != "unavailable")
    {
        addCar.SetEmailOfCurrentRenter(email);
    }
    cars.Add(addCar.GetNumberPlate(), addCar);
}

BinaryReader usersReader = new BinaryReader(usersFile);
while(usersReader.BaseStream.Position < usersReader.BaseStream.Length)
{
    User addUser = new User(usersReader.ReadString(), usersReader.ReadString(), usersReader.ReadString(), usersReader.ReadBoolean());
    addUser.GetRentListFromString(usersReader.ReadString());
    users.Add(addUser.GetEmail(), addUser);
}
//this is what is used when the program is ran in command line, meant to be used for admin purposes and not just for general staff
bool adminLine = false;
bool validLogin = false;

foreach (string arg in args)
{
    if(arg == "--a")
    {
        adminLine = true;
    }
    if(adminLine && validLogin)
    {
        validLogin = false;
    }
    if(adminLine && arg == "pswrd")
    {
        validLogin = true;
    }
}

if (validLogin)
{
    bool loop = true;
    while (loop)
    {
        Console.Write(">");
        string adminInput = Console.ReadLine().ToLower();
        switch (adminInput)
        {
            case "--n":
                AddNewUser();
                break;
            case "--r":
                Parallel.ForEach(users, (kvp, state) =>
                {
                    Console.WriteLine(kvp.Key);
                });
                RemoveUser(true);
                break;
            case "--h":
                Console.Write("COMMANDS: \n--n: Add a new user \n--r: Remove a user \n--e: Exit program \n");
                break;
            case "--e":
                loop = false;
                break;
            default:
                Console.Write("INVALID INPUT \nUSE --h FOR HELP");
                break;
        }
    }
    Environment.Exit(0);
}


User currentUser = null; //set as null to prevent an error on line 59, don't understand why it's needed but it works
bool found = false;

while (!found)
{
    Console.Write("Email: ");
    string email = Console.ReadLine().Trim();

    Console.Write("Password: ");
    string password = Console.ReadLine().Trim();

    bool searchComplete = false;
    //using parallel.foreach as it allows better scalability of the program over foreach
    Parallel.ForEach(users, (kvp, state) =>
    {
        if (kvp.Key == email && users[kvp.Key].GetPassword() == password)
        {
            currentUser = users[kvp.Key];
            searchComplete = true;
            found = true;
            state.Break();
        }
    });

    if (!searchComplete)
    {
            Console.WriteLine("USER DOES NOT EXIST/PASSWORD IS INCORRECT");
            Task.Delay(1500).Wait();
            Console.Clear();
    }
    else
    {

        Console.Clear();
        Console.Write($"Welcome back {currentUser.GetName()}");

        Task.Delay(750).Wait();
    }
}


if (currentUser.GetIsStaff())
{
    bool exit = false;
    while (!exit)
    {
        Console.Clear();
        Console.WriteLine("==========MENU========== \n1) Add a new vehicle \n2) Remove a vehicle \n3) Add a new user \n4) Remove a user " +
            "\n5) Remove current renter \n6) Available vehicles \n7) Close program");
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
                Console.Write("Number plate: ");
                string numberPlate = Console.ReadLine().ToUpper();
                Console.Write("Body type: ");
                string bodyType = Console.ReadLine();
                Console.Write("Cost to rent: ");
                string costToRentString = Console.ReadLine();
                float costToRent = 0f;
                try
                {
                     costToRent = Convert.ToSingle(costToRentString);
                }
                catch
                {
                    Console.WriteLine($"{costToRentString} is an invalid input, please enter a valid number");
                    Task.Delay(1500).Wait();
                    return;
                }
                if(costToRent > 0f)
                {
                    Car newCar = new Car(make, model, yom, numberPlate, bodyType, costToRent, true);
                    cars.Add(numberPlate, newCar);
                    newCar.AddToFile(carsFile);
                }
                else
                {
                    Console.WriteLine($"{costToRent} is not a valid value, please enter a value above 0");
                    Task.Delay(1500).Wait();
                }
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
                                UpdateCarFile();
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
                AddNewUser();
                break;
            case "4":
                Parallel.ForEach(users, (kvp, state) =>
                {
                    if (!users[kvp.Key].GetIsStaff())
                    {
                        Console.WriteLine(kvp.Key);
                    }   
                });
                RemoveUser(false);
                break;
            case "5":
                Parallel.ForEach(cars, (kvp, state) =>
                {
                    if (!cars[kvp.Key].GetAvailability())
                    {
                        Console.WriteLine($"Number plate:{kvp.Key} Email:{cars[kvp.Key].GetEmailOfCurrentRenter()}");
                    }
                });
                Console.Write("Enter number plate of the vehicle for person you want to remove: ");
                string numberPlateForEmail = Console.ReadLine();
                if (cars.ContainsKey(numberPlateForEmail))
                {
                    cars[numberPlateForEmail].SetEmailOfCurrentRenter("");
                    cars[numberPlateForEmail].SetAvailability(true);
                    UpdateCarFile();
                    Console.WriteLine("Person has been removed from the vehicle");
                    Task.Delay(1500).Wait();
                }
                else
                {
                    Console.WriteLine("This vehicle does not exist");
                    Task.Delay(1500).Wait();
                }
                break;
            case "6":
                GetAvailableVehicles(5000);
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
        Console.WriteLine("===============MENU=============== \n1) Rent a vehicle \n2) View available vehicles " +
            "\n3) View previously rented vehicles \n4) Exit program");
        string userOption = Console.ReadLine().Trim();
        Console.Clear();
        switch (userOption)
        {
            case "1":
                GetAvailableVehicles(100);
                Console.Write("Enter number plate of vehicle you want to rent: ");
                string regPlateToRent = Console.ReadLine().ToUpper();

                if (cars.ContainsKey(regPlateToRent) && cars[regPlateToRent].GetAvailability())
                {
                    int daysRenting = 0;
                    while (true)
                    {
                        Console.Write("Enter the number of days you want to rent the vehicle for: ");
                        string daysRentingString = Console.ReadLine();
                        try
                        {
                            daysRenting = Convert.ToInt32(daysRentingString);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid input, please enter a valid number");
                            Task.Delay(1500).Wait();
                            continue;
                        }
                        if (daysRenting < 1)
                        {
                            Console.WriteLine("Invalid input, cannot rent for less then a day");
                            Task.Delay(1500).Wait();
                            Console.Clear();
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    int totalCost = daysRenting * Convert.ToInt32(cars[regPlateToRent].GetCostToRent()); //find cost to rent
                    Console.Write($"The total cost of renting {regPlateToRent} for {daysRenting} days is £{totalCost} " +
                        $"\nDo you want to continue(Y/N) ");
                    string option = Console.ReadLine().ToUpper();//set to upper to prevent case sensitivity
                    if (option == "Y")
                    {
                        cars[regPlateToRent].SetEmailOfCurrentRenter(currentUser.GetEmail());
                        cars[regPlateToRent].SetAvailability(false);
                        users[currentUser.GetEmail()].AddToRentHistory(regPlateToRent);
                        UpdateUserFile();
                        UpdateCarFile();
                        Console.WriteLine("Vehicle has been rented");
                        Task.Delay(1500).Wait();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Process has been terminated");
                        Task.Delay(1500).Wait();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("This vehicle does not exist or it is currently being rented");
                    Task.Delay(1500).Wait();
                }
                break;
            case "2":
                GetAvailableVehicles(5000);
                break;
            case "3":
                List<string> rentHistory = currentUser.GetRentHistory();
                if(rentHistory.Count != 0)
                {
                    Parallel.ForEach(rentHistory, vehicle =>
                    {
                        if (cars.ContainsKey(vehicle))
                        {
                            Console.WriteLine($"{cars[vehicle].GetMake()} {cars[vehicle].GetModel()}--{vehicle}");
                        }
                    });
                    Task.Delay(2500).Wait();
                }
                else
                {
                    Console.Write("No vehicle has been rented in the past");
                    Task.Delay(1500).Wait();
                }
                break;
            case "4":
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

//created this as a method due to repeat use in the program
//the parameter of time delay (in ms) is because the wait doesn't need to be as long for every use of the method
void GetAvailableVehicles(int timeDelay)
{
    Parallel.ForEach(cars, (kvp, state) =>
    {
        if (cars[kvp.Key].GetAvailability())
        {
            Console.WriteLine($"{cars[kvp.Key].GetMake()} {cars[kvp.Key].GetModel()}--{kvp.Key}--£{cars[kvp.Key].GetCostToRent()}/day");
        }
    });
    Task.Delay(timeDelay).Wait();
}
//thought about using IEnumerable here but decided against it as I am using a dictionary and not a list
//if I was using a list, I believe I would have still used this method as it would allow for better scalability

//this is used to add new users to the system, made it a method as it is used in two different places in the program 
void AddNewUser()
{
    Console.WriteLine("Enter persons information");
    Console.Write("Name: ");
    string name = Console.ReadLine();
    Console.Write("Email: ");
    string email = Console.ReadLine();
    Console.Write("Password: ");
    string password = Console.ReadLine();
    bool staffCheck;
    while (true)
    {
        Console.Write("Staff(true/false): ");
        string staff = Console.ReadLine().ToLower();
        if (staff == "true" || staff == "t")
        {
            staffCheck = true;
            break;
        }
        else if (staff == "false" || staff == "f")
        {
            staffCheck = false;
            break;
        }
        else
        {
            Console.WriteLine("Invalid option, please enter either true or false");
        }
    }
    List<string> RentalHistory = new List<string>();
    RentalHistory.Add(";");
    User newUser = new User(name, email, password, staffCheck);
    newUser.SetRentHistory(RentalHistory);
    users.Add(email, newUser);
    newUser.AddToFile(usersFile);
}
//method as it is used in multiple places
//isAdmin is used to determine if the user is coming from the full program or the admin command line as this changes what the user can do
void RemoveUser(bool isAdmin)
{
    Console.Write("Enter email of user you wish to remove: ");
    string emailToRemove = Console.ReadLine();
    if (users.ContainsKey(emailToRemove))
    {
        if (!isAdmin && users[emailToRemove].GetIsStaff())
        {
            Console.WriteLine("You do not have permission to remove this user");
            Task.Delay(1500).Wait();
        }
        else
        {
            while (true)
            {
                Console.Write($"Are you sure you want to delete {emailToRemove}(Y/N): ");
                //set to upper to prevent case sensitivity
                string confirmation = Console.ReadLine().ToUpper();
                if (confirmation == "Y")
                {
                    users.Remove(emailToRemove);
                    UpdateUserFile();
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
    }
}
//method used due to multiple instances of updating the file
void UpdateUserFile()
{
//Close the file to make sure no errors occur and then re-open the same file under the same vairable but using FileMode.create to allow me to complete rewrite all data
    usersFile.Close();
    usersFile = File.Open("users.dat", FileMode.Create);
    //used a regular foreach here due to needing to access the file in a linear method
    foreach (KeyValuePair<string, User> user in users)
    {
        users[user.Key].AddToFile(usersFile);
    }
}
//method used due to multiple instances of updating the file
void UpdateCarFile()
{
    carsFile.Close();
    carsFile = File.Open("cars.dat", FileMode.Create);
    foreach (KeyValuePair<string, Car> car in cars)
    {
        cars[car.Key].AddToFile(carsFile);
    }
}