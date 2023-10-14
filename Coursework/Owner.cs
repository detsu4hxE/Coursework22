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
    public partial class Owner : Form
    {
        public Owner()
        {
            InitializeComponent();
        }

        private void Owner_FormClosed(object sender, FormClosedEventArgs e)
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
        Regex nameCheck = new Regex(@"^[А-ЯЁ][а-яё]+$");
        Regex numCheck = new Regex(@"\d{6}");
        int id = 0;

        private void updateFun()
        {
            string selectquery = "select id_owner, surname, firstname, patronymic, number as passport_series, passport_id from owner, passport_series where owner.id_passport_series = passport_series.id_passport_series";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            tableOwner.DataSource = table;
            // passport series
            selectquery = "select number from passport_series;";
            adpt = new SqlDataAdapter(selectquery, myConnection);
            table = new DataTable();
            adpt.Fill(table);
            passportSeriesBox.DataSource = table;
            passportSeriesBox.ValueMember = "number";
            this.tableOwner.Columns["id_owner"].Visible = false;
        }

        private void Owner_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            updateFun();
            passportSeriesBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            string sur = surnameBox.Text;
            string fir = firstnameBox.Text;
            string pat = patronymicBox.Text;
            string passer = passportSeriesBox.Text;
            string pasid = passportIdBox.Text;
            // Поле id_name
            string selectquery = $"select id_passport_series from passport_series where number = '{passer}';";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int passerid = int.Parse(table.Rows[0][0].ToString());
            Match m1 = nameCheck.Match(sur);
            Match m2 = nameCheck.Match(fir);
            Match m3 = m1;
            if (pat.Trim() == "")
            {
                pat = "NULL";
            }
            else
            {
                m3 = nameCheck.Match(pat);
                pat = $"'{pat}'";
            }
            Match m4 = numCheck.Match(pasid);
            if (!m1.Success || !m2.Success || !m3.Success || !m4.Success)
            {
                MessageBox.Show("Введено неверное значение");
            }
            else
            {
                try
                {
                    selectquery = $"insert into owner (surname, firstname, patronymic, id_passport_series, passport_id) values ('{sur}', '{fir}', {pat}, {passerid}, '{pasid}');";
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

        private void tableOwner_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = tableOwner.CurrentCell.RowIndex;
            id = int.Parse(tableOwner.Rows[r].Cells[0].Value.ToString());
            surnameBox.Text = tableOwner.Rows[r].Cells[1].Value.ToString();
            firstnameBox.Text = tableOwner.Rows[r].Cells[2].Value.ToString();
            patronymicBox.Text = tableOwner.Rows[r].Cells[3].Value.ToString();
            passportSeriesBox.Text = tableOwner.Rows[r].Cells[4].Value.ToString();
            passportIdBox.Text = tableOwner.Rows[r].Cells[5].Value.ToString();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string sur = surnameBox.Text;
            string fir = firstnameBox.Text;
            string pat = patronymicBox.Text;
            string passer = passportSeriesBox.Text;
            string pasid = passportIdBox.Text;
            // Поле id_name
            string selectquery = $"select id_passport_series from passport_series where number = {passer};";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int passerid = int.Parse(table.Rows[0][0].ToString());
            Match m1 = nameCheck.Match(sur);
            Match m2 = nameCheck.Match(fir);
            Match m3 = m1;
            if (pat.Trim() == "")
            {
                pat = "NULL";
            }
            else
            {
                m3 = nameCheck.Match(pat);
                pat = $"'{pat}'";
            }
            Match m4 = numCheck.Match(pasid);
            if (id == 0)
            {
                MessageBox.Show("Не выбрано поле для изменения");
            }
            else
            {
                if (!m1.Success || !m2.Success || !m3.Success || !m4.Success)
                {
                    MessageBox.Show("Введено неверное значение");
                }
                else
                {
                    try
                    {
                        selectquery = $"update owner set surname = '{sur}', " +
                            $"firstname = '{fir}', patronymic = {pat}, i" +
                            $"d_passport_series = {passerid}, passport_id = '{pasid}' " +
                            $"where id_owner = {id} ;";
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
            int r = tableOwner.CurrentCell.RowIndex;
            int idfd = int.Parse(tableOwner.Rows[r].Cells[0].Value.ToString());
            string sur = tableOwner.Rows[r].Cells[1].Value.ToString();
            string fir = tableOwner.Rows[r].Cells[2].Value.ToString();
            string pat = tableOwner.Rows[r].Cells[3].Value.ToString();
            string selectquery = $"select * from car where id_owner = {idfd}";
            SqlDataAdapter adpt = new SqlDataAdapter(selectquery, myConnection);
            DataTable table = new DataTable();
            adpt.Fill(table);
            int c = table.Rows.Count;
            if (MessageBox.Show($"Вы точно хотите удалить данного владельца: {sur} {fir} {pat}?\n{c} значений из таблицы 'Car' с удаляемым владельцем также будут удалены",
                "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    selectquery = $"delete from owner where id_owner = {idfd}";
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
            surnameBox.Text = "";
            firstnameBox.Text = "";
            patronymicBox.Text = "";
            passportIdBox.Text = "";
        }
    }
}
