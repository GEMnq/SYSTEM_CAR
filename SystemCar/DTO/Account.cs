using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCar.DTO
{
    public class Account
    {
        public Account(string username, string fullname, string role, string password = null)
        {
            this.UserName = username;
            this.Fullname = fullname;
            this.Role = role;
            this.Password = password;
        }

        public Account(DataRow row)
        {
            this.UserName = row["username"].ToString();
            this.Fullname = row["fullname"].ToString();
            this.Role = row["role"].ToString(); ;
            this.Password = row["password"].ToString();
        }

        public string Fullname { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
