using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace VeterinaryClinic
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            MeExit();
        }
        private void MeExit()
        {
            DialogResult iExit;

            iExit = MessageBox.Show("Confirm if you Want to Exit", "Save DataGridView", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(iExit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeExit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(
                Passport.Text,
                txtName.Text,
                Breed.Text,
                Age.Text,
                Owner.Text,
                Sex.Text);
        }
        private void Delete()
        {
            foreach(DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            foreach(var c in this.Controls)
            {
                if(c is TextBox)
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
            int numRows = dataGridView1.Rows.Count;
            for(int i = 0; i < numRows; i++)
            {
                try
                {
                    int max = dataGridView1.Rows.Count - 1;
                    dataGridView1.Rows.Remove(dataGridView1.Rows[max]);
                }catch(Exception e)
                {
                    MessageBox.Show("All Rows are to be deleted " + e, "DataGridView Delete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }
        Bitmap bitmap;
        private void Button3_Click(object sender, EventArgs e)
        {
            int height = dataGridView1.Height;
            dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
            bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
            dataGridView1.Height = height;
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            if (dataGridView1.Rows.Count > 0)
            {

                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int height = dataGridView1.Height;
            dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
            bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
            dataGridView1.Height = height;
        }

        private void AddNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(Passport.Text, txtName.Text, Breed.Text, Age.Text, Owner.Text, Sex.Text);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            Microsoft.Office.Interop.Excel.Range xlRange;

            int xlRow;
            string strfileName;

            openFileDialog1.Filter = "Excel Office | *.xls; *.xlsx";
            openFileDialog1.ShowDialog();
            strfileName = openFileDialog1.FileName;

            if(strfileName != string.Empty)
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(strfileName);
                xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                xlRange = xlWorkSheet.UsedRange;
                int i = 0;
                for(xlRow = 2; xlRow <=xlRange.Rows.Count; xlRow++)
                {
                    i++;
                    dataGridView1.Rows.Add( xlRange.Cells[xlRow,1].Text, xlRange.Cells[xlRow, 2].Text, xlRange.Cells[xlRow, 3].Text, xlRange.Cells[xlRow, 4].Text, xlRange.Cells[xlRow, 5].Text, xlRange.Cells[xlRow, 6].Text);      
                }
            xlWorkBook.Close();
            xlApp.Quit();
            }
        }
        int indexRow;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            Passport.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            Breed.Text = row.Cells[2].Value.ToString();
            Age.Text = row.Cells[3].Value.ToString();
            Owner.Text = row.Cells[4].Value.ToString();
            Sex.Text = row.Cells[5].Value.ToString();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            DataGridViewRow newDataRow = dataGridView1.Rows[indexRow];
            newDataRow.Cells[0].Value = Passport.Text;
            newDataRow.Cells[1].Value = txtName.Text;
            newDataRow.Cells[2].Value = Breed.Text;
            newDataRow.Cells[3].Value = Age.Text;
            newDataRow.Cells[4].Value = Owner.Text;
            newDataRow.Cells[5].Value = Sex.Text;
        }

    
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb =
                new System.Text.StringBuilder();
            string searchValue = textBox1.Text;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listBox1.Items.Clear();
            try
            {
                bool valueResult = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(searchValue))
                        {
                            
                            listBox1.Items.Add("Passport: " + row.Cells[i - 2].Value.ToString() + " Name: " + row.Cells[i - 1].Value.ToString() + " Age: " + row.Cells[i + 1].Value.ToString() + " Owner: "+ row.Cells[i + 2].Value.ToString() + " Sex: " + row.Cells[i + 3].Value.ToString());

                            int rowIndex = row.Index;
                           // dataGridView1.Rows[rowIndex].Selected = true;
                            valueResult = true;
                            break;
                        }
                        
                    }

                }
                if (!valueResult)
                {
                    MessageBox.Show("Unable to find " + textBox1.Text, "Not Found");
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb =
               new System.Text.StringBuilder();
            string searchValue = textBox1.Text;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listBox1.Items.Clear();
            try
            {
                bool valueResult = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(searchValue))
                        {

                            listBox1.Items.Add("Passport: " + row.Cells[i - 5].Value.ToString() + " Name: " + row.Cells[i - 4].Value.ToString() + " Breed: " + row.Cells[i - 3].Value.ToString() + " Age: " + row.Cells[i - 2].Value.ToString() + " Owner: " + row.Cells[i - 1].Value.ToString());

                            int rowIndex = row.Index;
                           // dataGridView1.Rows[rowIndex].Selected = true;
                            valueResult = true;
                            break;
                        }
                        
                    }

                }
                if (!valueResult)
                {
                    MessageBox.Show("Unable to find " + textBox1.Text, "Not Found");
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb =
               new System.Text.StringBuilder();
            string searchValue = textBox1.Text;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listBox1.Items.Clear();
            try
            {
                bool valueResult = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(searchValue))
                        {
                            
                            listBox1.Items.Add("Passport: " + row.Cells[i - 3].Value.ToString()  + " Name: " + row.Cells[i - 2].Value.ToString() + " Breed: " + row.Cells[i - 1].Value.ToString() + " Owner: " + row.Cells[i + 1].Value.ToString() + " Sex: " + row.Cells[i + 2].Value.ToString());

                            int rowIndex = row.Index;
                            //dataGridView1.Rows[rowIndex].Selected = true;
                            valueResult = true;
                            break;
                        }
                    }
                    

                }
                if (!valueResult)
                {
                    MessageBox.Show("Unable to find " + textBox1.Text, "Not Found");
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
