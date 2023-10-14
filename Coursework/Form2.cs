using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Coursework
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainPage form = new MainPage();
            form.Show(this);
            Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Подключение к серверу
        SqlConnection myConnection = new SqlConnection("Server = .\\SQLEXPRESS; database = coursework; Integrated Security=True; TrustServerCertificate = True");
        //SqlConnection myConnection = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; database = coursework; Integrated Security=True;");
        private void updateFun()
        {
            // mark
            string selectquery = "select name from mark;";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            markBox.DataSource = table;
            markBox.ValueMember = "name";
            // color
            selectquery = "select name from color;";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            colorBox.DataSource = table;
            colorBox.ValueMember = "name";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            updateFun();
            markBox.DropDownStyle = ComboBoxStyle.DropDownList;
            colorBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void form2Fun()
        {
            string mark = markBox.Text;
            string color = colorBox.Text;
            string selectquery = $"select car.number, concat(surname, ' ', firstname, ' ', patronymic) as owner, " +
                $"passport_series.number as passport_series, passport_id" +
                $" from car, owner, passport_series, mark, color " +
                $"where passport_series.id_passport_series = owner.id_passport_series" +
                $" and owner.id_owner = car.id_owner " +
                $"and mark.id_mark = car.id_mark" +
                $" and color.id_color = car.id_color" +
                $" and mark.name = '{mark}'" +
                $" and color.name = '{color}';";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            tableForm2.DataSource = table;
        }
        private void markBox_SelectedValueChanged(object sender, EventArgs e)
        {
            form2Fun();
        }

        private void colorBox_SelectedValueChanged(object sender, EventArgs e)
        {
            form2Fun();
        }
    }
}
