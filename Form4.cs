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
    public partial class Form4 : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;
        public Form4()
        {
            InitializeComponent();
        }

        private void TicketIDcmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e) //selecting multirows with stored procedures POINT 5
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GetTicketId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("TID", OracleDbType.RefCursor, ParameterDirection.Output);
            cmd.Parameters.Add("PID", PassIDtext.Text);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TicketIDcmb.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void button2_Click(object sender, EventArgs e)//show mobile number btn (selecting one row using stored procedures POINT 4)
        {
            int mobileNumber;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GetMobileNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("mobnumber", OracleDbType.Int32, ParameterDirection.Output);
            cmd.Parameters.Add("PassengerID", PassIDtext.Text);
            cmd.ExecuteNonQuery();
            mobileNumber = Convert.ToInt32(cmd.Parameters["mobnumber"].Value.ToString());
            MobnumText.Text = mobileNumber.ToString();


        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            OracleCommand Commandobj = new OracleCommand();
            Commandobj.Connection = conn;
            Commandobj.CommandText = "Delete from TICKET_ where TICKET_ID=:ticketID";
            Commandobj.Parameters.Add("ticketID", TicketIDcmb.SelectedItem.ToString());
            int r = Commandobj.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Ticket is Cancelled");
               TicketIDcmb.Items.RemoveAt(TicketIDcmb.SelectedIndex);
                PassIDtext.Text = "";
                MobnumText.Text = "";

            }
        }
    }
}
