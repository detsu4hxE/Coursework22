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
    public partial class PassportSeries : Form
    {
        public PassportSeries()
        {
            InitializeComponent();
        }
        private void PassportSeries_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainPage form = new MainPage();
            form.Show(this);
            Hide();
        }
        // Подключение к серверу
        SqlConnection myConnection = new SqlConnection("Server = .\\SQLEXPRESS; database = coursework; Integrated Security=True; TrustServerCertificate = True");
        //SqlConnection myConnection = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; database = coursework; Integrated Security=True;");
        int id = 0;
        Regex numCheck = new Regex(@"\d{4}");
        private void updateFun()
        {
            string selectquery = "select * from passport_series";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            tablePassportSeries.DataSource = table;
            this.tablePassportSeries.Columns["id_passport_series"].Visible = false;
        }
        private void PassportSeries_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            updateFun();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            string num = numberBox.Text;
            Match m1 = numCheck.Match(num);
            if (!m1.Success)
            {
                MessageBox.Show("Введено неверное значение");
            }
            else
            {
                string selectquery = $"select number from passport_series where number = '{num}';";
                SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                DataTable table = new DataTable();
                adpt.Fill(table);
                int rc = table.Rows.Count;
                if (rc == 1)
                {
                    MessageBox.Show("В таблице уже есть строка с данным значеинем");
                }
                else
                {
                    try
                    {
                        selectquery = $"insert into passport_series (number) values ('{num}');";
                        adpt = new SqlDataAdapter(selectquery, myConnection);
                        table = new DataTable();
                        adpt.Fill(table);
                        tablePassportSeries.DataSource = table;
                        updateFun();
                        MessageBox.Show("Значение было добавлено в таблицу");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void tablePassportSeries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = tablePassportSeries.CurrentCell.RowIndex;
            id = int.Parse(tablePassportSeries.Rows[r].Cells[0].Value.ToString());
            numberBox.Text = tablePassportSeries.Rows[r].Cells[1].Value.ToString();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Не выбрано поле для изменения");
            }
            else
            {
                string num = numberBox.Text;
                Match m1 = numCheck.Match(num);
                if (!m1.Success)
                {
                    MessageBox.Show("Введено неверное значение");
                }
                else
                {
                    string selectquery = $"select * from passport_series where number = '{num}';";
                    SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                    DataTable table = new DataTable();
                    adpt.Fill(table);
                    int rc = table.Rows.Count;
                    if (rc == 1)
                    {
                        MessageBox.Show("В таблице уже есть строка с данным значеинем");
                    }
                    else
                    {
                        selectquery = $"select number from passport_series where id_passport_series = {id};";
                        adpt = new SqlDataAdapter(selectquery, myConnection);
                        table = new DataTable();
                        adpt.Fill(table);
                        rc = table.Rows.Count;
                        if (rc == 0)
                        {
                            MessageBox.Show("Ошибка");
                        }
                        else
                        {
                            try
                            {
                                selectquery = $"update passport_series set number = '{num}' where id_passport_series = {id};";
                                adpt = new SqlDataAdapter(selectquery, myConnection);
                                table = new DataTable();
                                adpt.Fill(table);
                                tablePassportSeries.DataSource = table;
                                updateFun();
                                MessageBox.Show("Значение было изменено");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int r = tablePassportSeries.CurrentCell.RowIndex;
            int idfd = int.Parse(tablePassportSeries.Rows[r].Cells[0].Value.ToString());
            string num = tablePassportSeries.Rows[r].Cells[1].Value.ToString();
            string selectquery = $"select * from owner, passport_series where owner.id_passport_series = passport_series.id_passport_series and owner.id_passport_series = {idfd}";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int c = table.Rows.Count;
            if (MessageBox.Show($"Вы точно хотите удалить данную серию паспорта: {num}?\n{c} значений из таблицы 'Owner' с удаляемой серией паспорта также будут удалены",
                "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    selectquery = $"delete from passport_series where id_passport_series = {idfd}";
                    adpt = new SqlDataAdapter(selectquery, myConnection);
                    table = new DataTable();
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
        }
    }
}
