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
    public partial class Form3 : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select AIRPORT_CODE from AIRPORT";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                AirportCodecmb.Items.Add(dr2[0].ToString());
            }
            dr2.Close();
            cmd.Parameters.Clear();

            
            cmd.CommandText = "select FLIGHT_NUM from flight";
            cmd.CommandType = CommandType.Text;
             dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                FlightNumcmb.Items.Add(dr2[0].ToString());
            }
            dr2.Close();
            


        }

        private void insertBtn_Click(object sender, EventArgs e) // INSERT WITHOUT PROCEDURES POINT 3
        {
            OracleCommand Commandobj = new OracleCommand();
            Commandobj.Connection = conn;
            Commandobj.CommandText = "insert into FLIGHT values(:flightnum,:airline,:reservedseats,(select AIRPORT_CODE from AIRPORT where AIRPORT_CODE =:AirportCode))";
            Commandobj.Parameters.Add("flightnum", FlightNumcmb.Text);
            Commandobj.Parameters.Add("airline", airlineText.Text);
            Commandobj.Parameters.Add("reservedseats", reservedSeatsText.Text);
            Commandobj.Parameters.Add("AirportCode", AirportCodecmb.Text);
            int r = Commandobj.ExecuteNonQuery();
            if (r!= -1)
            {
                FlightNumcmb.Items.Add(FlightNumcmb.Text.ToString());
                MessageBox.Show("Flight is Added");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e) // UPDATE POINT 3
        {
            OracleCommand Commandobj = new OracleCommand();
            Commandobj.Connection = conn;
            Commandobj.CommandText = "update FLIGHT set AIRLINE=:airline, RESERVED_SEATS=:reservedseats where FLIGHT_NUM=:flightnum";
            Commandobj.Parameters.Add("airline", airlineText.Text);
            Commandobj.Parameters.Add("reservedseats", reservedSeatsText.Text);
            Commandobj.Parameters.Add("flightnum", FlightNumcmb.SelectedItem.ToString());
            int r = Commandobj.ExecuteNonQuery();
            if (r != -1) {
                MessageBox.Show("Flight is modified");
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e) //DELETE POINT 3
        {
            OracleCommand Commandobj = new OracleCommand();
            Commandobj.Connection = conn;
            Commandobj.CommandText = "Delete from FLIGHT where FLIGHT_NUM=:flightnum";
            Commandobj.Parameters.Add("flightnum", FlightNumcmb.Text);
            int r = Commandobj.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Flight is deleted");
                FlightNumcmb.Items.RemoveAt(FlightNumcmb.SelectedIndex);
                AirportCodecmb.Text = "";
                reservedSeatsText.Text = "";
                airlineText.Text = "";
            }
        }
    }
}
