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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Сотрудники", connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                // Сохраняем изменения в таблице "Сотрудники"
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True");
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Сотрудники", connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update((DataTable)dataGridView1.DataSource);
                // выводим сообщение об успешном сохранении
                MessageBox.Show("Данные успешно сохранены!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True";
            string query = "SELECT * FROM Сотрудники";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Сотрудники");
                dataGridView1.DataSource = ds.Tables["Сотрудники"];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
