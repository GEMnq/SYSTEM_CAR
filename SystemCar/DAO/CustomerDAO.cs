using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SystemCar.DTO;

namespace SystemCar.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance
        {
            get { if (instance == null) instance = new CustomerDAO(); return CustomerDAO.instance; }
            private set { CustomerDAO.instance = value; }
        }

        private CustomerDAO() { }

        public List<Customer> GetListCustomer()
        {
            List<Customer> list = new List<Customer>();

            string query = "select * from Customer";

            DataTable data = DataProvider.Instance.createQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Customer Customer = new Customer(item);
                list.Add(Customer);
            }

            return list;
        }

        public bool InsertCustomer(string fullname, string address, int mobile)
        {
            string query = string.Format("INSERT dbo.Customer ( fullname, address, mobile ) VALUES ( N'{0}', N'{1}', {2})", fullname, address, mobile);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool UpdateCustomer(int id, string fullname, string address, int mobile)
        {
            string query = string.Format("UPDATE dbo.Customer SET fullname = N'{0}', address = N'{1}', mobile = {2} WHERE id = {3}", fullname, address, mobile, id);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool DeleteCustomer(int id)
        {
            BillDAO.Instance.DeleteBillByCustomerID(id);
            
            string query = "DELETE FROM DBO.Customer WHERE id = " + id;
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public List<Customer> SearchCustomerByName(string fullname, string address, int mobile)
        {
            List<Customer> list = new List<Customer>();

                string query = string.Format("SELECT * FROM dbo.Customer WHERE " +
         "dbo.fuConvertToUnsign1(fullname) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%' " +
         "OR dbo.fuConvertToUnsign1(address) LIKE N'%' + dbo.fuConvertToUnsign1(N'{1}') + '%' " +
         "OR mobile LIKE N'%' + '{2}' + '%'",
         fullname, address, mobile);



            DataTable data = DataProvider.Instance.createQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Customer Customer = new Customer(item);
                list.Add(Customer);
            }

            return list;
        }
    }
}
