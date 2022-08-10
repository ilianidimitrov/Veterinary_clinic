using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VeterinaryClinic
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();  
        }

        string[] username = {"qwerty", "root" };
        string[] password = {"1234", "root" };
        private void Button1_Click(object sender, EventArgs e)
        {
            if (username.Contains(textBox1.Text) && password.Contains(textBox2.Text) && Array.IndexOf(username, textBox1.Text) == Array.IndexOf(password, textBox2.Text))
            {
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
            else
            {
                MessageBox.Show("The Username and/or Password is incorrect");
            }
         
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
