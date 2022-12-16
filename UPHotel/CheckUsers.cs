using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPHotel
{
    public class CheckUsers
    {
        public string Login { get; set; }
        public bool IsAdmin { get; set; }
        public string Status => IsAdmin ? "Admin" : "User";
        public CheckUsers(string login, bool isAdmin)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }
    }
}
