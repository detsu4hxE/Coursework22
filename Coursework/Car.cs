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
using System.Text.RegularExpressions;

namespace Coursework
{
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }

        private void Car_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            MainPage form = new MainPage();
            form.Show(this);
            Hide();
        }

        // Подключение к серверу
        SqlConnection myConnection = new SqlConnection("Server = .\\SQLEXPRESS; database = coursework; Integrated Security=True; TrustServerCertificate = True");
        //SqlConnection myConnection = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; database = coursework; Integrated Security=True;");
        Regex yearCheck = new Regex(@"(19\d{2})|(20[0-1]\d)|(202[0-2])");
        Regex numCheck = new Regex(@"^[авекмнорстух]\d{3}[авекмнорстух]{2}$");
        int id = 0;
        string numfc;

        private void updateFun()
        {
            string selectquery = "select id_car, car.number as number, issue_year, color.name as color, mark.name as mark, " +
                "concat(surname, ' ', firstname, ' ', patronymic) as owner" +
                " from car, owner, mark, color " +
                "where car.id_mark = mark.id_mark" +
                " and color.id_color = car.id_color" +
                " and car.id_owner = owner.id_owner;";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            tableCar.DataSource = table;
            // color
            selectquery = "select name from color;";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            colorBox.DataSource = table;
            colorBox.ValueMember = "name";
            // mark
            selectquery = "select name from mark;";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            markBox.DataSource = table;
            markBox.ValueMember = "name";
            // mark
            selectquery = "select concat(surname, ' ', firstname, ' ', patronymic) as owner from owner;";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            ownerBox.DataSource = table;
            ownerBox.ValueMember = "owner";
            this.tableCar.Columns["id_car"].Visible = false;
        }
        private void Car_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            updateFun();
            markBox.DropDownStyle = ComboBoxStyle.DropDownList;
            colorBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ownerBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            string num = numberBox.Text;
            string year = issueYearBox.Text;
            string color = colorBox.Text;
            string mark = markBox.Text;
            string owner = ownerBox.Text;
            // Поле id_color
            string selectquery = $"select id_color from color where name = '{color}';";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int colorid = int.Parse(table.Rows[0][0].ToString());
            // Поле id_mark
            selectquery = $"select id_mark from mark where name = '{mark}';";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            int markid = int.Parse(table.Rows[0][0].ToString());
            // Поле id_owner
            selectquery = $"select id_owner from owner where concat(surname, ' ', firstname, ' ', patronymic) = '{owner}';";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            int ownerid = int.Parse(table.Rows[0][0].ToString());
            Match m1 = numCheck.Match(num);
            Match m2 = yearCheck.Match(year);
            selectquery = $"select number from car where number = '{num}';";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            if (table.Rows.Count == 1)
            {
                MessageBox.Show("В таблице уже есть автомобиль с данным номером");
            }
            else
            {
                if (!m1.Success || !m2.Success)
                {
                    MessageBox.Show("Введено неверное значение");
                }
                else
                {
                    try
                    {
                        selectquery = $"insert into car (number, issue_year, id_color, id_mark, id_owner)" +
                            $" values ('{num}', {int.Parse(year)}, {colorid}, {markid}, {ownerid});";
                        adpt = new SqlDataAdapter(selectquery, myConnection);
                        table = new DataTable();
                        adpt.Fill(table);
                        updateFun();
                        MessageBox.Show("Данные были добавлены в таблицу");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void tableCar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = tableCar.CurrentCell.RowIndex;
            id = int.Parse(tableCar.Rows[r].Cells[0].Value.ToString());
            numfc = tableCar.Rows[r].Cells[1].Value.ToString();
            numberBox.Text = numfc;
            issueYearBox.Text = tableCar.Rows[r].Cells[2].Value.ToString();
            colorBox.Text = tableCar.Rows[r].Cells[3].Value.ToString();
            markBox.Text = tableCar.Rows[r].Cells[4].Value.ToString();
            ownerBox.Text = tableCar.Rows[r].Cells[5].Value.ToString();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            int rc = 0;
            string num = numberBox.Text;
            string year = issueYearBox.Text;
            string color = colorBox.Text;
            string mark = markBox.Text;
            string owner = ownerBox.Text;
            // Поле id_color
            string selectquery = $"select id_color from color where name = '{color}';";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int colorid = int.Parse(table.Rows[0][0].ToString());
            // Поле id_mark
            selectquery = $"select id_mark from mark where name = '{mark}';";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            int markid = int.Parse(table.Rows[0][0].ToString());
            // Поле id_owner
            selectquery = $"select id_owner from owner where concat(surname, ' ', firstname, ' ', patronymic) = '{owner}';";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            int ownerid = int.Parse(table.Rows[0][0].ToString());
            Match m1 = numCheck.Match(num);
            Match m2 = yearCheck.Match(year);
            if (!m1.Success || !m2.Success)
            {
                MessageBox.Show("Введено неверное значение");
            }
            else
            {
                if (num != numfc)
                {
                    selectquery = $"select * from car where number = '{num}';";
                    adpt = new SqlDataAdapter(selectquery, myConnection);
                    table = new DataTable();
                    adpt.Fill(table);
                    rc = table.Rows.Count;
                }
                if (rc == 1)
                {
                    MessageBox.Show("В таблице уже есть строка с данным значением");
                }
                else
                {
                    try
                    {
                        selectquery = $"update car set number = '{num}', issue_year = {int.Parse(year)}, id_color = {colorid}, id_mark = {markid}, id_owner = {ownerid} where id_car = {id};";
                        adpt = new SqlDataAdapter(selectquery, myConnection);
                        table = new DataTable();
                        adpt.Fill(table);
                        updateFun();
                        MessageBox.Show("Данные были изменены");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int r = tableCar.CurrentCell.RowIndex;
            int idfd = int.Parse(tableCar.Rows[r].Cells[0].Value.ToString());
            string num = tableCar.Rows[r].Cells[1].Value.ToString();
            if (MessageBox.Show($"Вы точно хотите удалить данный автомобиль: {num}?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string selectquery = $"delete from car where id_car = {idfd}";
                    SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                    DataTable table = new DataTable();
                    adpt.Fill(table);
                    updateFun();
                    MessageBox.Show("Строка была удалена");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            numberBox.Text = "";
            issueYearBox.Text = "";
        }
    }
}
