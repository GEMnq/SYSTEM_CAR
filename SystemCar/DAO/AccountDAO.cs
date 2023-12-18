using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemCar.DTO;

namespace SystemCar.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool LoginForm(string username, string password)
        {
            string query = "LOGIN @username , @password ";
            DataTable result = DataProvider.Instance.createQuery(query, new object[] {username, password});
            return result.Rows.Count > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.createQuery("Select * from account where username = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public bool UpdateAccount(string username, string fullname, string pass, string newPass)
        {
            int result = DataProvider.Instance.createNonQuery("exec PROC_UpdateAccount @username , @fullname , @password , @newPassword", new object[] { username, fullname, pass, newPass });

            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.createQuery("SELECT * FROM dbo.Account");
        }

        public bool InsertAccount(string username, string fullname, string role)
        {
            string query = string.Format("INSERT dbo.Account ( username, fullname, role ) VALUES  ( N'{0}', N'{1}', N'{2}')", username, fullname, role);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string username, string fullname, string role)
        {
            string query = string.Format("UPDATE dbo.Account SET fullname = N'{1}', role = N'{2}' WHERE username = N'{0}'", username, fullname, role);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string username)
        {
            string query = string.Format("Delete dbo.Account where username = N'{0}'", username);
            int result = DataProvider.Instance.createNonQuery(query);

            return result > 0;
        }

        public List<Account> SearchAccountByName(string fullname)
        {
            List<Account> list = new List<Account>();

            string query = string.Format("SELECT * FROM dbo.Account WHERE dbo.fuConvertToUnsign1(fullname) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", fullname);

            DataTable data = DataProvider.Instance.createQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Account Account = new Account(item);
                list.Add(Account);
            }

            return list;
        }

       
    }
}
