using CarRentalSystem;
using System.Globalization;

Dictionary<string, Car>cars = new Dictionary<string, Car>();
Dictionary<string, User>users = new Dictionary<string, User>();

FileStream carsFile = File.Open("cars.dat", FileMode.OpenOrCreate);
FileStream usersFile = File.Open("users.dat", FileMode.OpenOrCreate);

BinaryReader carsReader = new BinaryReader(carsFile);
while (carsReader.BaseStream.Position < carsReader.BaseStream.Length)
{
    Car addCar = new Car(carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadString(), carsReader.ReadSingle(),carsReader.ReadBoolean());
    cars.Add(addCar.GetNumberPlate(), addCar);
}

BinaryReader usersReader = new BinaryReader(usersFile);
while(usersReader.BaseStream.Position < usersReader.BaseStream.Length)
{
    User addUser = new User(usersReader.ReadString(), usersReader.ReadString(), usersReader.ReadString(), usersReader.ReadString(), usersReader.ReadBoolean());
    //addUser.GetRentListFromString(usersReader.ReadString()); not sure how I want to implement this yet
    users.Add(addUser.GetEmail(), addUser);
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
        Console.WriteLine("MENU \n1)Add a new vehicle \n2)Remove a vehicle \n3)Add a new user \n4)Remove a user \n5)Remove a person who's currently renting \n6)Available vehicles \n7)Close program");
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
                float costToRent = Convert.ToSingle(Console.ReadLine());
                Car newCar = new Car(make, model, yom, numberPlate, bodyType, costToRent, true);
                cars.Add(numberPlate, newCar);
                newCar.AddToFile(carsFile);
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
                Console.WriteLine("Enter persons information");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                string dob;
                while (true)
                {
                    Console.Write("Date of birth(DD/MM/YYYY): ");
                    dob = Console.ReadLine();
                    if (dob.Length == 10 && dob[2] == '/' && dob[5] == '/')
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("INCORRECT INPUT! \nPlease make sure you are entering the date in the correct format (DD/MM/YYYY)");
                    }
                }
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
                User newUser = new User(name, email, password, dob, staffCheck);
                users.Add(email, newUser);
                newUser.AddToFile(usersFile);
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
                            //remove from file
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
                //use parallel.foreach to display all vehicles that are currently rented with email of renter,
                //get user to input email, confirm, remove current renter from instance
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
        Console.WriteLine("MENU \n1)Rent a vehicle \n2)View available vehicles \n3)View previously rented vehicles \n4) \n5)Exit program");
        string userOption = Console.ReadLine().Trim();
        Console.Clear();
        switch (userOption)
        {
            case "1":
                GetAvailableVehicles(100);
                Console.WriteLine("Enter number plate of vehicle you want to rent: ");
                string regPlateToRent = Console.ReadLine().ToUpper();

                if (cars.ContainsKey(regPlateToRent) && cars[regPlateToRent].GetAvailability())
                {
                    int daysRenting = 0;
                    while (true)
                    {
                        Console.Write("Enter the number of days you want to rent the vehicle for: ");
                        daysRenting = Convert.ToInt32(Console.ReadLine());
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
                    int totalCost = daysRenting * Convert.ToInt32(cars[regPlateToRent].GetCostToRent());
                    Console.Write($"The total cost of renting {regPlateToRent} for {daysRenting} days is £{totalCost} " +
                        $"\nDo you want to continue(Y/N)");
                    string option = Console.ReadLine().ToUpper();
                    if(option == "Y")
                    {
                        cars[regPlateToRent].SetEmailOfCurrentRenter(currentUser.GetEmail());
                        cars[regPlateToRent].SetAvailability(false);
                        users[currentUser.GetEmail()].AddToRentHistory(regPlateToRent);
                        Console.WriteLine("Vehicle has been rented");
                        Task.Delay(1500).Wait();
                        Console.Clear();
                        //need to add file updating
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