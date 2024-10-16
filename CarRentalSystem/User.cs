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
        private string userName;
        private string password;
        private string email;
        private string DoB;
        private bool isStaff;
        private List<string> rentHistory = new List<string>();
    }
}
