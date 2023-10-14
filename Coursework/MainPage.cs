using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void carButton_Click(object sender, EventArgs e)
        {
            Car form = new Car();
            form.Show(this);
            Hide();
        }
        private void colorButton_Click(object sender, EventArgs e)
        {
            Color form = new Color();
            form.Show(this);
            Hide();
        }

        private void markButton_Click(object sender, EventArgs e)
        {
            Mark form = new Mark();
            form.Show(this);
            Hide();
        }

        private void ownerButton_Click(object sender, EventArgs e)
        {
            Owner form = new Owner();
            form.Show(this);
            Hide();
        }

        private void passportSeriesButton_Click(object sender, EventArgs e)
        {
            PassportSeries form = new PassportSeries();
            form.Show(this);
            Hide();
        }

        private void form1Button_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show(this);
            Hide();
        }

        private void form2Button_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show(this);
            Hide();
        }
    }
}
