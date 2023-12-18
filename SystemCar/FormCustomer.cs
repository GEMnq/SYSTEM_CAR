using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemCar.DAO;
using SystemCar.DTO;
using System.Collections;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
//using LicenseContext = System.ComponentModel.LicenseContext;

namespace SystemCar
{
    public partial class fFormCustomer : Form
    {
        BindingSource CustomerList = new BindingSource();
        public fFormCustomer()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            dtgvCustomer.DataSource = CustomerList;
            LoadListCustomer();
            AddCustomerBinding();
        }

        void LoadListCustomer()
        {
            CustomerList.DataSource = CustomerDAO.Instance.GetListCustomer();
        }

        void AddCustomerBinding()
        {
            txtIDCus.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtFullnameCus.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "Fullname", true, DataSourceUpdateMode.Never));
            txtAddressCus.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "Address", true, DataSourceUpdateMode.Never));
            txtMobileCus.DataBindings.Add(new Binding("Text", dtgvCustomer.DataSource, "Mobile", true, DataSourceUpdateMode.Never));
        }

        /// <summary>
        /// export file excel
        /// </summary>
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (dtgvCustomer.Rows.Count > 0)
            {
                Excel.ApplicationClass MExcel = new Excel.ApplicationClass();
                MExcel.Application.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)MExcel.ActiveSheet;

                // Đặt tên bảng
                string tableName = "Danh sách khách hàng";
                int columnCount = dtgvCustomer.Columns.Count;
                int startColumn = 1;
                int endColumn = startColumn + columnCount - 1;

                worksheet.Name = "Khách hàng";

                // Merge các cột để đặt tên bảng
                ((Excel.Range)worksheet.Cells[1, startColumn]).Value = tableName;
                ((Excel.Range)worksheet.Cells[1, startColumn]).Font.Bold = true;
                ((Excel.Range)worksheet.Cells[1, startColumn]).Font.Size = 16;
                ((Excel.Range)worksheet.Cells[1, startColumn]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                Excel.Range mergeRange = worksheet.Range[worksheet.Cells[1, startColumn], worksheet.Cells[1, endColumn]];
                mergeRange.Merge();
                mergeRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous; // Đường biên

                worksheet.Cells[2, startColumn] = ""; // Dòng trắng

                // Thêm header của các cột
                for (int i = startColumn; i <= endColumn; i++)
                {
                    worksheet.Cells[3, i] = dtgvCustomer.Columns[i - startColumn].HeaderText;
                    ((Excel.Range)worksheet.Cells[3, i]).Font.Bold = true;
                    ((Excel.Range)worksheet.Cells[3, i]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)worksheet.Cells[3, i]).Interior.Color = System.Drawing.Color.LightBlue; // Màu nền
                }

                // Thêm dữ liệu
                for (int i = 0; i < dtgvCustomer.Rows.Count; i++)
                {
                    for (int j = 0; j < dtgvCustomer.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 4, startColumn + j] = dtgvCustomer.Rows[i].Cells[j].Value.ToString();
                    }
                }

                // AutoFit cột và dòng
                worksheet.Columns.AutoFit();
                worksheet.Rows.AutoFit();

                MExcel.Visible = true;
            }
            else
            {
                MessageBox.Show("No records found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnInsertAcc_Click(object sender, EventArgs e)
        {
            string fullname = txtFullnameCus.Text;
            string address = txtAddressCus.Text;
            int mobile = Convert.ToInt32(txtMobileCus.Text);

            if (CustomerDAO.Instance.InsertCustomer(fullname, address, mobile))
            {
                MessageBox.Show("Insert successfully");
                LoadListCustomer();
            }
            else
            {
                MessageBox.Show("Insert fail!");
            }
        }

        private void btnUpdateAcc_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDCus.Text);
            string fullname = txtFullnameCus.Text;
            string address = txtAddressCus.Text;
            int mobile = Convert.ToInt32(txtMobileCus.Text);

            if (CustomerDAO.Instance.UpdateCustomer(id, fullname, address, mobile))
            {
                MessageBox.Show("Update successfully");
                LoadListCustomer();
            }
            else
            {
                MessageBox.Show("Update fail!");
            }
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDCus.Text);
            BillDAO.Instance.DeleteBillByCustomerID(id);

            ////////////////////////
            //////////////////////////
            
            if (CustomerDAO.Instance.DeleteCustomer(id))
            {
                MessageBox.Show("Delete successfully");
                LoadListCustomer();
            }
            else
            {
                MessageBox.Show("Delete fail!");
            }
        }

        private void btnShowAcc_Click(object sender, EventArgs e)
        {
            LoadListCustomer();
        }

        List<Customer> SearchCustomerByName(string fullname, string address, int mobile)
        {
            List<Customer> listName = CustomerDAO.Instance.SearchCustomerByName(fullname, address, mobile);

            return listName;
        }

        private void btnSearchCate_Click(object sender, EventArgs e)
        {
            string search = txbSearchFoodName.Text;

            if (int.TryParse(search, out int mobile))
            {
                
                CustomerList.DataSource = SearchCustomerByName(search, search, mobile);
            }
            else
            {
               
                CustomerList.DataSource = SearchCustomerByName(search, search, -1);
            }
        }
    }
}
