using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCar.DTO;

namespace SystemCar.DAO
{
    public class ShowBillDAO
    {
        private static ShowBillDAO instance;

        public static ShowBillDAO Instance
        {
            get { if (instance == null) instance = new ShowBillDAO(); return ShowBillDAO.instance; }
            private set { ShowBillDAO.instance = value; }
        }

        private ShowBillDAO() { }

        public List<ShowBill> GetListShowBillByTable(int id)
        {
            List<ShowBill> listShowBill = new List<ShowBill>();
       
            string query = "SELECT cate.name, cate.price, cus.fullname, b.status, b.fuel, b.feature FROM dbo.Bill AS b, dbo.Category AS cate, dbo.Customer AS cus" +
                " WHERE b.idCategory = cate.id AND b.idCustomer = cus.id AND b.status = 0 AND b.idCategory = " + id;
            DataTable data = DataProvider.Instance.createQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ShowBill ShowBill = new ShowBill(item);
                listShowBill.Add(ShowBill);
            }

            return listShowBill;
        }
    }
}
