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
    public partial class Mark : Form
    {
        public Mark()
        {
            InitializeComponent();
        }

        private void Mark_FormClosed(object sender, FormClosedEventArgs e)
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
        string namefc = "";
        int id = 0;
        Regex nameCheck = new Regex(@"^[А-ЯЁ]+[а-яё]*(\-| )?[а-яё]*$");
        private void updateFun()
        {
            string selectquery = "select * from mark";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            tableMark.DataSource = table;
            this.tableMark.Columns["id_mark"].Visible = false;
        }

        private void Mark_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            updateFun();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            string name = nameBox.Text;
            int isf;
            if (isForeignBox.Checked)
            {
                isf = 1;
            }
            else
            {
                isf = 0;
            }
            Match m1 = nameCheck.Match(name);
            if (!m1.Success)
            {
                MessageBox.Show("Введено неверное значение");
            }
            else
            {
                string selectquery = $"select * from mark where name = '{name}';";
                SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                DataTable table = new DataTable();
                adpt.Fill(table);
                int rc = table.Rows.Count;
                if (rc == 1)
                {
                    MessageBox.Show("В таблице уже есть марка с данным названием");
                }
                else
                {
                    try
                    {
                        selectquery = $"insert into mark (name, is_foreign) values ('{name}', {isf});";
                        adpt = new SqlDataAdapter(selectquery, myConnection);
                        table = new DataTable();
                        adpt.Fill(table);
                        updateFun();
                        MessageBox.Show("Марка была добавлена в таблицу");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void tableMark_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = tableMark.CurrentCell.RowIndex;
            id = int.Parse(tableMark.Rows[r].Cells[0].Value.ToString());
            namefc = tableMark.Rows[r].Cells[1].Value.ToString();
            nameBox.Text = namefc;
            string isffc = tableMark.Rows[r].Cells[2].Value.ToString();
            if (isffc == "True")
            {
                isForeignBox.Checked = true;
            }
            else
            {
                isForeignBox.Checked = false;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            int rc = 0;
            if (id == 0)
            {
                MessageBox.Show("Не выбрано поле для изменения");
            }
            else
            {
                string name = nameBox.Text;
                int isf;
                if (isForeignBox.Checked)
                {
                    isf = 1;
                }
                else
                {
                    isf = 0;
                }
                Match m1 = nameCheck.Match(name);
                if (!m1.Success)
                {
                    MessageBox.Show("Введено неверное значение");
                }
                else
                {
                    if (name != namefc)
                    {
                        string selectquery = $"select * from mark where name = '{name}';";
                        SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                        DataTable table = new DataTable();
                        adpt.Fill(table);
                        rc = table.Rows.Count;
                    }
                    if (rc == 1)
                    {
                        MessageBox.Show("В таблице уже есть строка с данным значением");
                    }
                    else
                    {
                        string selectquery = $"select * from mark where id_mark = {id};";
                        SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
                        DataTable table = new DataTable();
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
                                selectquery = $"update mark set name = '{name}', is_foreign = {isf} where id_mark = '{id}';";
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
            int r = tableMark.CurrentCell.RowIndex;
            int idfd = int.Parse(tableMark.Rows[r].Cells[0].Value.ToString());
            string name = tableMark.Rows[r].Cells[1].Value.ToString();
            string selectquery = $"select * from car, mark where car.id_mark = mark.id_mark and car.id_mark = {idfd}";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int c = table.Rows.Count;
            if (MessageBox.Show($"Вы точно хотите удалить данную марку: {name}?\n{c} значений из таблицы 'Car' с удаляемой маркой также будут удалены",
                "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    selectquery = $"delete from mark where id_mark = {idfd}";
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
            isForeignBox.Checked = false;
        }
    }
}
