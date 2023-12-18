using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCar.DTO
{
    public class Chart
    {
        public Chart(string name, int payment)
        {
            this.Name = name;
            this.Payment = Payment;
        }

        public Chart(DataRow row)
        {
          
            this.Name = row["name"].ToString();
            this.Payment = (int)row["payment"];
        }

        public string Name { get; set; }
        public int Payment { get; set; }

        
    }
}
