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
    public partial class Form2 : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;
        public Form2()
        {
            InitializeComponent();
          
        }
        private void Form2_Load(object sender, EventArgs e) // selecting one or more rows POINT 1
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand Commandobj = new OracleCommand();
            Commandobj.Connection = conn;
         

            Commandobj.CommandText = "select * from PASSENGER_";
            Commandobj.CommandType = CommandType.Text;
           


            OracleDataReader dr = Commandobj.ExecuteReader();
            while (dr.Read())
            {
                PassengersIDcmb.Items.Add(dr[0].ToString()+ ' '+dr[1]+ ' '+dr[3]);
            }
            dr.Close();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PassengersIDcmb_SelectedIndexChanged(object sender, EventArgs e) // select using bind variables POINT 2
        {
            OracleCommand Commandobj = new OracleCommand();
            Commandobj.Connection = conn;
            Commandobj.CommandText = "select FIRST_NAME,LAST_NAME,EMAIL,SEX,MOBILE_NUM from PASSENGER_ where  PASSENGER_ID =:id ";
            Commandobj.CommandType = CommandType.Text;
            string[] str = PassengersIDcmb.SelectedItem.ToString().Split(' '); // split the data in combo box to get only the id
   
            Commandobj.Parameters.Add("id", str[0]);
            OracleDataReader dr = Commandobj.ExecuteReader();
            if (dr.Read()) {
                FirstNameText.Text = dr[0].ToString();
                LastNameText.Text = dr[1].ToString();
                EmailText.Text = dr[2].ToString();
                SexText.Text = dr[3].ToString();
                Mob_NumText.Text = dr[4].ToString();
            }
            dr.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        
    }
}
