using System;
using System.Windows.Forms;

namespace PR6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void openchild(Panel pen, Form emp)
        {
            emp.TopLevel = false;
            emp.FormBorderStyle = FormBorderStyle.None;
            emp.Dock = DockStyle.Fill;
            pen.Controls.Add(emp);
            emp.BringToFront();
            emp.Show();
        }

        //Нажатие на кнопку "Работники"
        private void button1_Click(object sender, EventArgs e)
        {
            openchild(panel1, new Emp());
        }

        //Нажатие на кнопку "Должности"
        private void button2_Click(object sender, EventArgs e)
        {
            openchild(panel1, new Position());
        }

        //Нажатие на кнопку "Статус работника"
        private void button3_Click(object sender, EventArgs e)
        {
            openchild(panel1, new Status());
        }
    }
}
