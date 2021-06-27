using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
       
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selecteditem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            if (selecteditem == "Display A passnger data")
            {
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
            else if (selecteditem == "Add,update or Cancel A Flight") {
                this.Hide();
                Form3 f3 = new Form3();
                f3.ShowDialog();
            }
            else if(selecteditem == "Cancel Reservation")
            {
                this.Hide();
                Form4 f4 = new Form4();
                f4.ShowDialog();
            }
        }

        
    }
}
