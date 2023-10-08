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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Сохраняем изменения в таблице "Товары"
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Товары", connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update((DataTable)dataGridView1.DataSource);
            // выводим сообщение об успешном сохранении
            MessageBox.Show("Данные успешно сохранены!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Создаем новый экземпляр формы Form2
            Form3 form3 = new Form3();

            // Выполняем запрос и получаем результаты в виде DataTable
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Товары.[№], Товары.[Код_товара], Товары.Категория, Товары.Наименование, Товары.[Единица_измерения], Товары.[Цена_закупочная], Товары.[Наценка,%], (Товары.[Цена_закупочная]*Товары.[Наценка,%]+Товары.[Цена_закупочная]) AS [Цена_реализации], Товары.[Количество_на_складе], Товары.[Срок_реализации], Товары.[Код_поставщика] FROM Товары", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }

            // Устанавливаем DataTable в качестве источника данных для dataGridView1 на форме Form2
            form3.dataGridView3.DataSource = dataTable;

            // Отображаем форму Form2
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Создание подключения к базе данных
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True");

            // Создание команды SQL для выполнения запроса
            SqlCommand command = new SqlCommand("SELECT * FROM Товары WHERE [Срок_реализации] < @Today", connection);

            // Добавление параметра даты в запрос
            command.Parameters.AddWithValue("@Today", DateTime.Today);

            // Создание адаптера данных и заполнение таблицы
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // Отображение данных в DataGridView на форме Form3
            Form4 form4 = new Form4();
            form4.dataGridView3.DataSource = table;
            form4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Соединение с базой данных
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                // Запрос на обновление значения столбца "Наценка,%" в таблице "Товары"
                string query = "UPDATE Товары SET [Наценка,%] = @Markup";

                // Создание команды для выполнения запроса
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        // Открытие соединения
                        connection.Open();

                        // Установка значения параметра запроса
                        command.Parameters.AddWithValue("@Markup", textBox1.Text);

                        // Выполнение запроса
                        int rowsAffected = command.ExecuteNonQuery();

                        // Вывод сообщения об успешном выполнении запроса
                        MessageBox.Show($"{rowsAffected} строк было изменено.");
                    }
                    catch (Exception ex)
                    {
                        // Вывод сообщения об ошибке выполнения запроса
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True";
            string query = "SELECT * FROM Товары";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Товары");
                dataGridView1.DataSource = ds.Tables["Товары"];
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
