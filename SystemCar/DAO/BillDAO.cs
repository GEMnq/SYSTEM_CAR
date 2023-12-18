using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemCar.DTO;

namespace SystemCar.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

       
        /// Thành công: bill ID
        /// thất bại: -1
     
        public int GetUncheckBillIDByTableID(int id)
        {
            string query = "SELECT * FROM dbo.Bill WHERE idCategory = " + id + " AND status = 0";
            DataTable data = DataProvider.Instance.createQuery(query);

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }

        public void InsertBill(int id, int idCustomer, string type, string feature, string fuel)
        {
            DataProvider.Instance.createNonQuery("exec InsertBill @idTable , @idCustomer , @type , @feature , @fuel ", new object[] { id, idCustomer, type, feature, fuel });
        }


        public void UpdateBill(int idBill, int idCustomer, string feature, string fuel)
        {
            DataProvider.Instance.createNonQuery("EXEC PROC_UpdateBill @idBill , @idCustomer , @feature , @fuel", new object[] { idBill, idCustomer, feature, fuel});
        }

        public bool DeleteBill(int id)
        {   
            string query = "DELETE FROM DBO.Bill WHERE id = " + id;
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public void CheckOut(int id)
        {
            string query = "UPDATE dbo.Bill SET status = 1, DateCheckOut = GETDATE() WHERE id = " + id;
            DataProvider.Instance.createNonQuery(query);
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.createQuery("exec PROC_GetBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }

        

        public void DeleteBillByCategoryID(int id)
        {
            string query = "DELETE FROM DBO.Bill WHERE idCategory = " + id;
            DataProvider.Instance.createNonQuery(query);
        }

        public void DeleteBillByCustomerID(int id)
        {
            string query = "DELETE FROM DBO.Bill WHERE idCustomer = " + id;
            DataProvider.Instance.createNonQuery(query);
        }

        public DataTable GetListBill()
        {
            string query = "select id as [ID], type as [Type], feature as [Feature], fuel as [Fuel], DateCheckIn as [DateCheckIn], DateCheckOut as [DateCheckOut], status as [Status] from dbo.Bill";
            return DataProvider.Instance.createQuery(query);
        }

        
    }
}
