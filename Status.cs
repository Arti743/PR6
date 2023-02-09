using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PR6
{
    public partial class Status : Form
    {
        DataSet data;
        SqlDataAdapter adap;
        SqlCommandBuilder command;
        //Данные для подключения к серверу.
        string connectionString = @"Data Source=192.168.0.3, 3306; Initial Catalog=Ychet_shop; Integrated Security=True";
        //Выводим нужную таблицу в DataGridView.
        string sql = "SELECT * FROM Status_";

        public static bool Sett;
        public Status()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            //Пытаемся подключиться к серверу
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Если подключение успешно, появится окно с таблицей.
                try
                {
                    connection.Open();
                    adap = new SqlDataAdapter(sql, connection);

                    data = new DataSet();
                    adap.Fill(data);
                    dataGridView1.DataSource = data.Tables[0];
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.AllowUserToAddRows = false;
                }
                //Если подключение не удалось, выводим сообщение об этом.
                catch
                {
                    MessageBox.Show("Отсутсвтует подключение к серверу. Повторите попытку позже.", "Ошибка подключения");
                }
            }
        }

        //Кнопка сохранения введенный данных в таблицу.
        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adap = new SqlDataAdapter(sql, connection);
                command = new SqlCommandBuilder(adap);
                //Созданная заранее процедура.
                adap.InsertCommand = new SqlCommand("ADD_Position", connection);
                adap.InsertCommand.CommandType = CommandType.StoredProcedure;
                //Поля для ввода информации в таблицу.
                adap.InsertCommand.Parameters.Add(new SqlParameter("@Status_Name", SqlDbType.VarChar, 40, "Status_Name"));


                adap.Update(data);
            }
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

        //Обновление таблицы.
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            openchild(panel1, new Status());
        }

        //При нажатии на кнопку "Добавить", добавляется новая пустая строка в таблице.
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DataRow row = data.Tables[0].NewRow();
            data.Tables[0].Rows.Add(row);
        }

        //При выделении нужной строки нажимаем удалить.
        private void buttonDel_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }
    }
}
