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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Создаем новую форму
            Form2 form2 = new Form2();

            // Получаем данные из таблицы "Товары" и передаем их на новую форму
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP-8OFF3I4\\SQLEXPRESS02; Initial Catalog = Trading_company; Integrated Security = True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Товары", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            form2.dataGridView1.DataSource = table;

            // Отображаем новую форму
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(); // Создаем экземпляр новой формы
            form7.Show(); // Отображаем новую форму
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(); // Создаем экземпляр новой формы
            form8.Show(); // Отображаем новую форму
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11(); // Создаем экземпляр новой формы
            form11.Show(); // Отображаем новую форму
        }
    }
}
