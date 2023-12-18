using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCar.DTO;

namespace SystemCar.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();

            string query = "select * from Category";

            DataTable data = DataProvider.Instance.createQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }

        public List<Category> GetListCategoryBill()
        {
            List<Category> list = new List<Category>();

            string query = "select id as [ID], name as [Name], brand as [Brand], price as [Price] from Category";

            DataTable data = DataProvider.Instance.createQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }

        public bool InsertCategory(string name, string brand, float price)
        {
            string query = string.Format("INSERT dbo.Category ( name, brand, price ) VALUES ( N'{0}', N'{1}', {2})", name, brand, price);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool UpdateCategory(int id, string name, string brand, float price)
        {
            string query = string.Format("UPDATE dbo.Category SET name = N'{0}', brand = N'{1}', price = {2} WHERE id = {3}", name, brand, price, id);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool DeleteCategory(int id)
        {
            BillDAO.Instance.DeleteBillByCategoryID(id);
            string query = "DELETE FROM DBO.Category WHERE id = " + id;
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

       

        public List<Category> SearchCategoryByName(string name)
        {
            List<Category> list = new List<Category>();

            string query = string.Format("SELECT * FROM dbo.Category WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);

            DataTable data = DataProvider.Instance.createQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category Category = new Category(item);
                list.Add(Category);
            }

            return list;
        }

        public void InsertExcelCategory(string name, string brand, float price)
        {
            DataProvider.Instance.createNonQuery("exec InsertCategoryByExcel @name , @brand , @price ", new object[] { name, brand, price });
        }
    }
}
