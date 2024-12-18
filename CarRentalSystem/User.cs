using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    internal class User
    {
        //attributes
        private string fName;
        private string lName;
        private string email;
        private string password;
        private bool isStaff;
        private List<string> rentHistory = new List<string>();
        //constructor
        public User(string name, string email, string password, bool isStaff)
        {
            string[] names = name.Split(" ");
            fName = names[0];
            lName = names[1];
            this.email = email;
            this.password = password;
            this.isStaff = isStaff;
        }
        //Get operations
        public string GetName()
        {
            return fName + " " + lName;
        }
        public string GetPassword()
        {
            return password;
        }
        public string GetEmail()
        {
            return email;
        }
        public bool GetIsStaff()
        {
            return isStaff;
        }
        public List<string> GetRentHistory()
        {
            return rentHistory;
        }
        //adds a new vehicle to the rent history
        public void AddToRentHistory(string vehicleRegPlate)
        {
            rentHistory.Add(vehicleRegPlate);
        }
        //this is to be able to store the list in the binary file
        public string GetRentString()
        {
            string rentString = "";
            foreach (string rent in rentHistory)
            {
                rentString += ";"+rent;
            }
            return rentString;
        }
        //this is to be able to read data from the binary file into a list
        public List<string> GetRentListFromString(string rentString)
        {
            List<string> rentalHistory = rentString.Split(";").ToList();
            rentHistory.Capacity = rentalHistory.Count;
            //intentially left the capacity to be the same as count, not one less
            //as it is likely when a user logs into the system they want to rent a new car and therefore removes the operations to increase list capacity 
            return rentalHistory;
        }
        public void SetRentHistory(List<string> rentalHistory)
        {
            rentHistory = rentalHistory;
        }
        public void AddToFile(FileStream file) //adds the instance of user to binary file
        {
            BinaryWriter bw = new BinaryWriter(file);
            bw.Write(fName + " " + lName);
            bw.Write(email);
            bw.Write(password);
            bw.Write(isStaff);
            bw.Write(GetRentString());
            bw.Flush();
        }
    }
}
