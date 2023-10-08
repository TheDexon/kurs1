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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Номер_чека, Код_сотрудника, Дата, Дисконтная_карта, Скидка, Сумма_к_оплате FROM Счета", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView11111.DataSource = table;
            dataGridView11111.AutoGenerateColumns = true;
            dataGridView11111.ReadOnly = true;
            dataGridView11111.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Запрос для удаления всех строк из таблицы "Счета"
            string query = "DELETE FROM Счета";

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Таблица успешно очищена.");
                }
                else
                {
                    MessageBox.Show("Ошибка при очистке таблицы.");
                }

            }
        }


       
    }
}
