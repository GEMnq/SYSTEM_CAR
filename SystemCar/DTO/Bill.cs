using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCar.DTO
{
    public class Bill
    {
        public Bill(int id, int categoryID, int customerID, string type, string feature, string fuel, DateTime? dateCheckin, DateTime? dateCheckOut, int status)
        {
            this.ID = id;
            this.CategoryID = categoryID;
            this.CustomerID = customerID;
            this.Type = type;
            this.Feature = feature;
            this.Fuel = fuel;
            this.DateCheckIn = dateCheckin;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.CategoryID = (int)row["idCategory"];
            this.CustomerID = (int)row["idCustomer"];
            this.Type = row["type"].ToString();
            this.Feature = row["feature"].ToString();
            this.Fuel = row["fuel"].ToString();

            this.DateCheckIn = (DateTime?)row["dateCheckin"];

            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;

            this.Status = (int)row["status"];

        }

        private int categoryID;

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        private int customerID;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }


        private string feature;

        public string Feature
        {
            get { return feature; }
            set { feature = value; }
        }

        public string fuel;

        public string Fuel
        {
            get { return fuel; }
            set { fuel = value; }
        }


        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private DateTime? dateCheckOut;

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        private DateTime? dateCheckIn;

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
