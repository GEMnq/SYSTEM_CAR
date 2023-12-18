using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemCar.DAO;
using SystemCar.DTO;

namespace SystemCar
{
    public partial class fSystem : Form
    {
        bool sidebarExpand;
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Role); }
        }
        public fSystem(Account account)
        {
            InitializeComponent();
            this.LoginAccount = account;

            LoadTable();
            LoadCustomer();
        }

        #region Method

        void ChangeAccount(string role)
        {
            if (role == "admin")
            {
                lbManagement.Enabled = true;
            }
            else
            {
                lbManagement.Enabled = false;
            }
        }
        void LoadCustomer()
        {
            List<Customer> listCustomer = CustomerDAO.Instance.GetListCustomer();
            cbCustomer.DataSource = listCustomer;
            cbCustomer.DisplayMember = "Fullname";
        }

        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.SkyBlue;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<ShowBill> listBillInfo = ShowBillDAO.Instance.GetListShowBillByTable(id);
            if(listBillInfo != null && listBillInfo.Count > 0)
            {
                foreach (ShowBill item in listBillInfo)
                {
                    ListViewItem lsvItem = new ListViewItem(item.CateName.ToString());
                    lsvItem.SubItems.Add(item.Price.ToString());
                    lsvItem.SubItems.Add(item.CustomerName.ToString());
                   

                    //set Bill
                    cbCustomer.Text = item.CustomerName.ToString();
                    cbFuel.Text = item.Fuel.ToString();
                    txtPrice.Text = item.Price.ToString();

                    if (item.Status.ToString() == "0" || item.Status == 0)
                    {
                        lsvItem.SubItems.Add("No");
                    }
                    lsvItem.SubItems.Add(item.Fuel.ToString());
                    lsvItem.SubItems.Add(item.Feature.ToString());
                    lsvBill.Items.Add(lsvItem);       
                    CultureInfo culture = new CultureInfo("vi-VN");
                    txtPrice.Text = item.Price.ToString("c", culture);
                }
                
            }
            else
            {
                cbCustomer.Text = "";
                cbFuel.Text = "";
                txtPrice.Text = "";
            }    

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            
            // lấy idBill từ idTable
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            if (idBill != -1)
            {
                if (MessageBox.Show("Are you sure to pay your bill? " + table.Name, "Notification", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill);
                    ShowBill(table.ID);

                    LoadTable();
                }
            }
            else
            {
                MessageBox.Show("The car is empty. Payment cannot be made!");
            }
        }
        #endregion

        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            cbCategory.Text = ((sender as Button).Tag as Table).Name;
            UncheckAllCheckBoxes();
            ShowBill(tableID);
        }
        string check()
        {
            string result = "";

            for (int i = 1; i <= 13; i++)
            {
                string checkBoxName = "cb" + i;
                CheckBox checkBox = this.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;

                if (checkBox != null && checkBox.Checked)
                {
                    result += (checkBox.Text + ", ");
                }
            }

            return result;
        }
        void UncheckAllCheckBoxes()
        {
            for (int i = 1; i <= 13; i++)
            {
                string checkBoxName = "cb" + i;
                CheckBox checkBox = this.Controls.Find(checkBoxName, true).FirstOrDefault() as CheckBox;

                if (checkBox != null)
                {
                    checkBox.Checked = false;
                }
            }
        }

        private void btnInsertAcc_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Please choose car rental service!");
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int idCustomer = (cbCustomer.SelectedItem as Customer).ID;
            String type = cbCategory.Text;
            String fuel = cbFuel.SelectedItem as String;
            String feature = check();

            if (idBill == -1) // bill chưa tồn tại -> tạo mới
            {
                BillDAO.Instance.InsertBill(table.ID, idCustomer, type, feature, fuel);
                MessageBox.Show("Insert bill successfully!");
                ShowBill(table.ID);
                LoadTable();
            }
            else
            {
                BillDAO.Instance.UpdateBill(idBill, idCustomer, feature, fuel);
                MessageBox.Show("Update bill successfully!");
                ShowBill(table.ID);
                LoadTable();
            }
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Please choose car rental service!");
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            if (idBill == -1) // bill chưa tồn tại -> tạo mới
            {
                MessageBox.Show("Bill non-existent!");          
            }
            else
            {
                BillDAO.Instance.DeleteBill(idBill);
                MessageBox.Show("Delete bill successfully!");
                ShowBill(table.ID);
                LoadTable();
            }
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            
        }

        private void lbManagement_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            f.InsertCategory += f_InsertCategory;
            f.DeleteCategory += f_DeleteCategory;
            f.UpdateCategory += f_UpdateCategory;
            f.ShowDialog();
        }

        void f_UpdateCategory(object sender, EventArgs e)
        {
           
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
                LoadTable();
        }

        void f_DeleteCategory(object sender, EventArgs e)
        {
            
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
                LoadTable();
        }

        void f_InsertCategory(object sender, EventArgs e)
        {
          
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
                LoadTable();

        }

        private void lbLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbAccount_Click(object sender, EventArgs e)
        {
            fUser f = new fUser(loginAccount);
            f.ShowDialog();
        }

        private void lbCustomer_Click(object sender, EventArgs e)
        {
            fFormCustomer f = new fFormCustomer();
            f.ShowDialog();
        }
        #endregion



        #region out
        private void fSystem_Load(object sender, EventArgs e)
        {

        }

        private void txtFeature_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbFuel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void c360_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void reportsAndStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manegementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void sidebarTicked(object sender, EventArgs e)
        {
            if(sidebarExpand)
            {
                sidebar.Width -= 10;
                if(sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void lbMenu_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void lvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbCustomer_Enter(object sender, EventArgs e)
        {

        }








        #endregion

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
    }
}
