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
        private string dob;
        private bool isStaff;
        private List<string> rentHistory = new List<string>();
        //constructor
        public User(string name, string email, string password, string DoB, bool isStaff, List<string> rentalHistory)
        {
            string[] names = name.Split(" ");
            fName = names[0];
            lName = names[1];
            this.email = email;
            this.password = password;
            dob = DoB;
            this.isStaff = isStaff;
            rentHistory = rentalHistory;
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
        public string GetDoB()
        {
            return dob;
        }
        public bool GetIsStaff()
        {
            return isStaff;
        }
        public List<string> GetRentHistory()
        {
            return rentHistory;
        }
    }
}
