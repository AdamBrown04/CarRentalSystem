using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    internal class User
    {
        private string fName;
        private string lName;
        private string username;
        private string password;
        private string email;
        private string DoB;
        private bool isStaff;
        private List<string> rentHistory = new List<string>();

        public void ViewfName()
        {
            Console.Write(fName);
        }
        public void ViewlName()
        {
            Console.Write(lName);
        }
        public void ViewUsername()
        {
            Console.Write(username);
        }
        public void ViewPassword()
        {
            Console.Write(password);
        }
        public void ViewEmail()
        {
            Console.Write(email);
        }
        public void ViewDoB()
        {
            Console.Write(DoB);
        }
        public void ViewIsStaff()
        {
            if (isStaff)
            {
                Console.Write($"{username} is staff");
            }
            else
            {
                Console.Write($"{username} is not staff");
            }
        }
        public void GetfName(string firstName)
        {
            fName = firstName;
        }
        public void GetlName(string lastName)
        {
            lName = lastName;
        }
        public void Getusername(string Username)
        {
            username = Username;
        }
        public void GetPassword(string Password)
        {
            password = Password;
        }
        public void GetEmail(string Email)
        {
            email = Email;
        }
        public void GetDoB(string DOB)
        {
            DoB = DOB;
        }
        public void GetIsStaff(bool staff)
        {
            isStaff = staff;
        }
    }
}
