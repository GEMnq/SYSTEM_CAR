using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using SystemCar.DAO;
using SystemCar.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SystemCar
{
    public partial class fAdmin : Form
    {
        BindingSource CategoryList = new BindingSource();
        BindingSource AccountList = new BindingSource();
        BindingSource OrderList = new BindingSource();

        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadRole()
        {
            DataTable listAccount = AccountDAO.Instance.GetListAccount();
            var uniqueRoles = listAccount.AsEnumerable()
                                  .Select(row => row.Field<string>("Role"))
                                  .Distinct()
                                  .ToList();

            // Tạo một DataTable mới từ danh sách các giá trị duy nhất
            DataTable uniqueRolesTable = new DataTable();
            uniqueRolesTable.Columns.Add("Role", typeof(string));

            foreach (var role in uniqueRoles)
            {
                uniqueRolesTable.Rows.Add(role);
            }

            // Gán DataTable mới làm nguồn dữ liệu cho ComboBox
            cbRoleAccount.DataSource = uniqueRolesTable;
            //cbRoleAccount.DataSource = listAccount;
            cbRoleAccount.DisplayMember = "Role";
        }

        #region Methods
        void LoadData()
        {
            dtgvCategory.DataSource = CategoryList;
            dtgvAccount.DataSource = AccountList;
            dtgvOrder.DataSource = OrderList;

            LoadRole();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            LoadListCategory();
            LoadListAccount();
            LoadListBill();
           

            AddCategoryBinding();
            AddAccountBinding();
            AddBillBinding();
        }

        void LoadListAccount()
        {
            AccountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void LoadListBill()
        {
            OrderList.DataSource = BillDAO.Instance.GetListBill();
        }


        void LoadListCategory()
        {
            CategoryList.DataSource = CategoryDAO.Instance.GetListCategoryBill();
        }

        void LoadListChart()
        {
            dtgvChart.DataSource = ChartDAO.Instance.GetName();
        }

        void AddAccountBinding()
        {
            txtIDAccount.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtFullnameAccount.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Fullname", true, DataSourceUpdateMode.Never));
            txtUsernameAccount.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Username", true, DataSourceUpdateMode.Never));
            cbRoleAccount.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Role", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding()
        {
            txtIDCate.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameCate.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtBrandCate.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Brand", true, DataSourceUpdateMode.Never));
            txtPriceCate.DataBindings.Add(new Binding("Value", dtgvCategory.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void AddBillBinding()
        {
            txtIDBill.DataBindings.Add(new Binding("Text", dtgvOrder.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameBill.DataBindings.Add(new Binding("Text", dtgvOrder.DataSource, "Type", true, DataSourceUpdateMode.Never));
            txtFeatureBill.DataBindings.Add(new Binding("Text", dtgvOrder.DataSource, "Feature", true, DataSourceUpdateMode.Never));
            txtFuelBill.DataBindings.Add(new Binding("Text", dtgvOrder.DataSource, "Fuel", true, DataSourceUpdateMode.Never));
            txtCheckinBill.DataBindings.Add(new Binding("Text", dtgvOrder.DataSource, "DateCheckIn", true, DataSourceUpdateMode.Never));
            txtCheckoutBill.DataBindings.Add(new Binding("Text", dtgvOrder.DataSource, "DateCheckOut", true, DataSourceUpdateMode.Never));
            txtStatusBill.DataBindings.Add(new Binding("Text", dtgvOrder.DataSource, "Status", true, DataSourceUpdateMode.Never));
            if(txtStatusBill.Text == "0")
            {
                txtStatusBill.Text = "Đã thanh toán";
            }
        }
        void LoadDateTimePickerBill()
        {
            // set Date mặc định
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
            CultureInfo culture = new CultureInfo("vi-VN");

            float total = 0;
            for (int rowIndex = 0; rowIndex < dtgvBill.Rows.Count; rowIndex++)
            {
               
                object cellValue = dtgvBill.Rows[rowIndex].Cells["Price"].Value;

                if (cellValue != null)
                {
                  
                    total += (float)Convert.ToDouble(cellValue);
                }
            }
            txtTotal.Text = total.ToString("c", culture);
        }        
        List<Category> SearchFoodByName(string name)
        {
            List<Category> listCategory = CategoryDAO.Instance.SearchCategoryByName(name);

            return listCategory;
        }

        List<Account> SearchAccountByName(string fullname)
        {
            List<Account> listAccount = AccountDAO.Instance.SearchAccountByName(fullname);

            return listAccount;
        }
        #endregion

        #region Event
        #region Account
        private void btnShowAcc_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }

        private void btnInsertAcc_Click(object sender, EventArgs e)
        {
            string fullname = txtFullnameAccount.Text;
            string username = txtUsernameAccount.Text;
            string role = cbRoleAccount.Text;

            if (AccountDAO.Instance.InsertAccount(username, fullname, role))
            {
                MessageBox.Show("Insert successfully");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Insert fail!");
            }
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            string username = txtUsernameAccount.Text;
            if (loginAccount.UserName.Equals(username))
            {
                MessageBox.Show("You can't delete your own account");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(username))
            {
                MessageBox.Show("Delete successfully");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Delete fail!");
            }
        }

        private void btnUpdateAcc_Click(object sender, EventArgs e)
        {
            string fullname = txtFullnameAccount.Text;
            string username = txtUsernameAccount.Text;
            string role = cbRoleAccount.Text;

            if (AccountDAO.Instance.UpdateAccount(username, fullname, role))
            {
                MessageBox.Show("Update successfully");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Update fail!");
            }
        }

       
        #endregion

        #region Category
        private void btnSeeCate_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            string name = txtNameCate.Text;
            string brand = txtBrandCate.Text;
            float price = (float)txtPriceCate.Value;

            if (CategoryDAO.Instance.InsertCategory(name, brand, price))
            {
                TableDAO.Instance.InsertTable(name);
                MessageBox.Show("Insert successfully");
                LoadListCategory();
                if (insertCategory != null)
                    insertCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Insert fail!");
            }
        }

        private void btnUpdateCate_Click(object sender, EventArgs e)
        {
            string name = txtNameCate.Text;
            string brand = txtBrandCate.Text;
            float price = (float)txtPriceCate.Value;
            int id = Convert.ToInt32(txtIDCate.Text);

            if (CategoryDAO.Instance.UpdateCategory(id, name, brand, price))
            {
                TableDAO.Instance.UpdateTable(id, name);
                MessageBox.Show("Update successfully");
                LoadListCategory();
                if (updateCategory != null)
                    updateCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Update fail!");
            }
        }

        private void btnDeleteCate_Click(object sender, EventArgs e)
        {
        
            int id = Convert.ToInt32(txtIDCate.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                TableDAO.Instance.DeleteTable(id);
                MessageBox.Show("Delete successfully");
                LoadListCategory();
                if (deleteCategory != null)
                    deleteCategory(this, new EventArgs());

            }
            else
            {
                MessageBox.Show("Delete fail!");
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            CategoryList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        #endregion
        private void btnTk_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }




        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        #endregion

        #region out
        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabCategory_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabProduct_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSale_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }




        #endregion

        private void txtPriceCate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Select an Excel file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string name = "Sheet1";
                    string pathconn = "Provider = Microsoft.jet.OLEDB.4.0; Data source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR= yes;\";";
                    OleDbConnection conn = new OleDbConnection(pathconn);
                    OleDbDataAdapter MyDataAdapter = new OleDbDataAdapter("Select * from [" + name + "$]", conn);
                    DataTable dt = new DataTable();
                    MyDataAdapter.Fill(dt);
               
                    dtgvCategory.DataSource = dt;
                    for (int i = 0; i < dtgvCategory.Rows.Count - 1; i++) 
                    {
                        string item1 = dtgvCategory.Rows[i].Cells["Name"].Value.ToString();
                        string item2 = dtgvCategory.Rows[i].Cells["Brand"].Value.ToString();
                        float item3 = float.Parse(dtgvCategory.Rows[i].Cells["Price"].Value.ToString());
                        //CategoryDAO.Instance.InsertCategory(item1, item2, item3);
                        //TableDAO.Instance.InsertTable(item1);
                        CategoryDAO.Instance.InsertExcelCategory(item1, item2, item3);
                        TableDAO.Instance.InsertExcelSchedule(item1);
                    }
                    MessageBox.Show("Import successful!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            List<DTO.Chart> chart = ChartDAO.Instance.GetName();
            chart1.Series.Clear();
            LoadListChart();
            Series s = new Series("Payments");
            
            chart1.ChartAreas[0].AxisX.Title = "Category";
            chart1.ChartAreas[0].AxisY.Title = "Payment Count";           
            chart1.Titles.Add("The chart shows the number of times the car has been rented");      
            s.ChartType = SeriesChartType.Column;


            foreach (var item in chart)
            {
                s.Points.AddXY(item.Name, item.Payment);
            }
            
            chart1.Series.Add(s);

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRoleAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
