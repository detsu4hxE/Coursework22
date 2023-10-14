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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            MainPage form = new MainPage();
            form.Show(this);
            Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Подключение к серверу
        SqlConnection myConnection = new SqlConnection("Server = .\\SQLEXPRESS; database = coursework; Integrated Security=True; TrustServerCertificate = True");
        //SqlConnection myConnection = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; database = coursework; Integrated Security=True;");
        private void updateFun()
        {
            string selectquery = "select concat(surname, ' ', firstname, ' ', patronymic) as owner from car, mark, color, owner" +
                " where car.id_owner = owner.id_owner and car.id_mark = mark.id_mark and car.id_color = color.id_color" +
                " group by surname, firstname, patronymic having count(id_car) > 1;";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            ownerBox.DataSource = table;
            ownerBox.ValueMember = "owner";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            updateFun();
            ownerBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void ownerBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string owner = ownerBox.Text;
            string selectquery = $"select mark.name as mark, number, color.name as color, is_foreign" +
                $" from car, mark, color, owner " +
                $"where car.id_owner = owner.id_owner" +
                $" and car.id_mark = mark.id_mark" +
                $" and car.id_color = color.id_color " +
                $"and concat(surname, ' ', firstname, ' ', patronymic) = '{owner}' order by mark.name";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            tableForm1.DataSource = table;
            int am = 0;
            for (int i = 0; i < (tableForm1.Rows.Count - 1); i++)
            {
                string isf = tableForm1.Rows[i].Cells[3].Value.ToString();
                if (isf == "True")
                {
                    am++;
                }
            }
            amountLabel.Text = $"Количество иномарок: {am}";
        }
    }
}
