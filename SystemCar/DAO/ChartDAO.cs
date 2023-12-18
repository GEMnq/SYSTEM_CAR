using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCar.DTO;
namespace SystemCar.DAO
{
    public class ChartDAO
    {
        private static ChartDAO instance;

        public static ChartDAO Instance
        {
            get { if (instance == null) instance = new ChartDAO(); return ChartDAO.instance; }
            private set { ChartDAO.instance = value; }
        }

        private ChartDAO() { }

        public List<Chart> GetName()
        {
            List<Chart> list = new List<Chart>();
            string query = "SELECT C.name AS name, COUNT(B.id) AS payment " +
                            "FROM dbo.CATEGORY C " +
                            "LEFT JOIN Bill B ON C.id = B.idCategory " +
                            "WHERE B.status = 1 " +
                            "GROUP BY C.name";
            DataTable data = DataProvider.Instance.createQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Chart lst = new Chart(item);
                list.Add(lst);
            }

            return list;
        }
    }
}
