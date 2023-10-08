using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs2
{
    public partial class Form7 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-8OFF3I4\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True");
        public Form7()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, что в TextBox введено число
            if (!int.TryParse(textBox1.Text, out int supplierId))
            {
                MessageBox.Show("Введите корректный код поставщика!");
                return;
            }

            // Проверяем, что в TextBox не пустое значение
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Введите код поставщика!");
                return;
            }

            string query = $"SELECT Код_товара, Категория, Наименование, Единица_измерения FROM Товары WHERE Код_поставщика = {supplierId}";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Товары");
            dataGridView7.DataSource = ds.Tables["Товары"];
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True";
            string query = "SELECT * FROM Поставщики";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Поставщики");
                dataGridView9.DataSource = ds.Tables["Поставщики"];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Сохраняем изменения в таблице "Товары"
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Поставщики", connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update((DataTable)dataGridView9.DataSource);
            // выводим сообщение об успешном сохранении
            MessageBox.Show("Данные успешно сохранены!");
        }
    }
}
