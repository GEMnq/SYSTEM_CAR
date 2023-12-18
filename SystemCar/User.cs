using System;
using System.Windows.Forms;
using SystemCar.DAO;
using SystemCar.DTO;

namespace SystemCar
{
    public partial class fUser : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public fUser(Account account)
        {
            InitializeComponent();
            this.LoginAccount = account;
        }

        void ChangeAccount(Account account)
        {
            txtFullname.Text = account.Fullname;
            txtUsername.Text = account.UserName;
        }

        void UpdateAccountInfo()
        {
            string fullname = txtFullname.Text;
            string username = txtUsername.Text;
            string password = txtPassWord.Text;
            string newpass = txtNewPass.Text;
            string reenterPass = txtReEnterPass.Text;


            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Please re-enter the correct password with the new password!");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(username, fullname, password, newpass))
                {
                    MessageBox.Show("Update successfully");
                   
                }
                else
                {
                    MessageBox.Show("Please enter correct password");
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        #region out
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void fUser_Load(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
