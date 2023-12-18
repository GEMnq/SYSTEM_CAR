using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemCar.DAO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using SystemCar.DTO;

namespace SystemCar
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPass.Text;
            if(username.Trim() == "")
            {
                MessageBox.Show("Please enter your username");
            }
            else if (password.Trim() == "")
            {
                MessageBox.Show("Please enter your password");
            }
            else if(LoginForm(username, password))
            {
                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(username);
                fSystem f = new fSystem(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
            
        }

        bool LoginForm(string username, string password)
        {
            return AccountDAO.Instance.LoginForm(username, password);
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the program?", "Notification", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPass.Text = "";
        }

        #region out
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
