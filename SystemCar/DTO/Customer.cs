using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCar.DTO
{
    public class Customer
    {

        public Customer(int id, string fullname, string address, int mobile)
        {
            this.ID = id;
            this.Fullname = fullname;
            this.Address = address;
            this.Mobile = mobile;
        }

        public Customer(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Fullname = row["fullname"].ToString();
            this.Address = row["address"].ToString();
            this.Mobile = (int)row["mobile"];
        }

        public int ID { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public int Mobile { get; set; }

    }
}
