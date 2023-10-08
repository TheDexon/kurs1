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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True";
            string query = "SELECT [Код_товара], Наименование, [Цена_закупочная] FROM Товары WHERE [Код_товара] = @код_товара";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@код_товара", textBox1.Text);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        dataGridView4.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Товар не найден");
                    }
                }
            }

        }
    }
}
