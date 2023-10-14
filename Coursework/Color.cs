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
    public partial class Color : Form
    {
        public Color()
        {
            InitializeComponent();
        }

        private void Color_FormClosed(object sender, FormClosedEventArgs e)
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
        int id = 0;
        Regex nameCheck = new Regex(@"^[А-ЯЁ][а-яё]+\-?[а-яё]+$");

        private void updateFun()
        {
            string selectquery = "select * from color";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            tableColor.DataSource = table;
            this.tableColor.Columns["id_color"].Visible = false;
        }
        private void Color_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            updateFun();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            string name = nameBox.Text;
            Match m1 = nameCheck.Match(name);
            if (!m1.Success)
            {
                MessageBox.Show("Введено неверное значение");
            }
            else
            {
                string selectquery = $"select * from color where name = '{name}';";
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
                        selectquery = $"insert into color (name)" +
                            $" values ('{name}');";
                        adpt = new SqlDataAdapter(selectquery, myConnection);
                        table = new DataTable();
                        adpt.Fill(table);
                        updateFun();
                        MessageBox.Show("Цвет был добавлен в таблицу");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void tableColor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = tableColor.CurrentCell.RowIndex;
            id = int.Parse(tableColor.Rows[r].Cells[0].Value.ToString());
            nameBox.Text = tableColor.Rows[r].Cells[1].Value.ToString();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Не выбрано поле для изменения");
            }
            else
            {
                string name = nameBox.Text;
                Match m1 = nameCheck.Match(name);
                if (!m1.Success)
                {
                    MessageBox.Show("Введено неверное значение");
                }
                else
                {
                    string selectquery = $"select * from color where name = '{name}';";
                    SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                    DataTable table = new DataTable();
                    adpt.Fill(table);
                    int rc = table.Rows.Count;
                    if (rc == 1)
                    {
                        MessageBox.Show("В таблице уже есть строка с данным значением");
                    }
                    else
                    {
                        selectquery = $"select * from color where id_color = {id};";
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
                                selectquery = $"update color set name = '{name}' where id_color = {id};";
                                adpt = new SqlDataAdapter(selectquery, myConnection);
                                table = new DataTable();
                                adpt.Fill(table);
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
            int r = tableColor.CurrentCell.RowIndex;
            int idfd = int.Parse(tableColor.Rows[r].Cells[0].Value.ToString());
            string name = tableColor.Rows[r].Cells[1].Value.ToString();
            string selectquery = $"select * from car, color where car.id_color = color.id_color and car.id_color = {idfd}";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int c = table.Rows.Count;
            if (MessageBox.Show($"Вы точно хотите удалить данный цвет: {name}?\n{c} значений из таблицы 'Car' с удаляемым цветом также будут удалены",
                "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    selectquery = $"delete from color" +
                        $" where id_color = {idfd}";
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
            nameBox.Text = "";
        }
    }
}
