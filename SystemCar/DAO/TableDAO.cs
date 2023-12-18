using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCar.DTO;

namespace SystemCar.DAO
{
    public class TableDAO
    {
        public static int TableWidth = 110;
        public static int TableHeight = 100;

        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        private TableDAO() { }

        // Table == Schedule
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.createQuery("GetTableList");

            //change dataTable to List<Table> in DTO
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public bool InsertTable(string name, string status = "Trống")
        {
            string query = string.Format("INSERT dbo.Schedule ( name, status) VALUES ( N'{0}', N'{1}')", name, status);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool UpdateTable(int id, string name)
        {
            string query = string.Format("UPDATE dbo.Schedule SET name = N'{0}' WHERE id = {1}", name, id);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool DeleteTable(int id)
        {
            string query = "DELETE FROM DBO.Schedule WHERE id = " + id;
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public void InsertExcelSchedule(string name, string status = "Trống")
        {
            DataProvider.Instance.createNonQuery("exec InsertScheduleByExcel @name , @status ", new object[] { name, status });
        }
    }
}
