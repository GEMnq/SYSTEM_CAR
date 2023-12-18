using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCar.DTO
{
    public class ShowBill
    {
       
        public ShowBill(string cateName, float price, string customerName, int status, string fuel, string feature)
        {
            this.cateName = cateName;    
            this.Price = price;
            this.CustomerName = customerName;
            this.Status = status;
            this.Fuel = fuel;
            this.Feature = feature;
        }

        public ShowBill(DataRow row)
        {
            this.CateName = row["name"].ToString();
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.CustomerName = row["fullname"].ToString();
            this.Status = (int)row["status"];
            this.Fuel = row["fuel"].ToString();
            this.Feature = row["feature"].ToString();
        }
        public string Feature { get; set; }
        public string fuel;

        public string Fuel
        {
            get { return fuel; }
            set { fuel = value; }
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private string cateName;

        public string CateName
        {
            get { return cateName; }
            set { cateName = value; }
        }

        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

    }
}
