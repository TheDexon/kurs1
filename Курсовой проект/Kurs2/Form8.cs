using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kurs2
{
    public partial class Form8 : Form
    {
        private SqlConnection connection; // Объект подключения к базе данных

        public Form8()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"); // Здесь указываем строку подключения к вашей базе данных
        }
        
        private void Form8_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "trading_companyDataSet.Сотрудники". При необходимости она может быть перемещена или удалена.
            this.сотрудникиTableAdapter.Fill(this.trading_companyDataSet.Сотрудники);
            string connectionString = "Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True";
            string query = "SELECT * FROM Заказы";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Заказы");
                dataGridView1.DataSource = ds.Tables["Заказы"];
            }
            // Загружаем данные в ComboBox1 из таблицы "Товары"
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Код_товара FROM Товары", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Код_товара"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Код_сотрудника FROM Сотрудники", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["Код_сотрудника"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // При выборе кода товара в ComboBox1 отображаем его название в Label2
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Наименование FROM Товары WHERE Код_товара = @Код_товара", connection);
                cmd.Parameters.AddWithValue("@Код_товара", comboBox1.SelectedItem.ToString());
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label2.Text = reader["Наименование"].ToString();
                }
                else
                {
                    label2.Text = "";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Наименование, Количество_на_складе, Цена_закупочная FROM Товары WHERE Код_товара = @Код_товара", connection);
                cmd.Parameters.AddWithValue("@Код_товара", comboBox1.SelectedItem.ToString());
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label2.Text = reader["Наименование"].ToString();
                    label3.Text = reader["Количество_на_складе"].ToString();
                    label4.Text = reader["Цена_закупочная"].ToString();
                }
                else
                {
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

            private void label8_Click(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(); // Создаем экземпляр новой формы
            form10.Show(); // Отображаем новую форму

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float availableQuantity = Convert.ToSingle(label3.Text); // Получаем доступное количество товара из label3
            float enteredQuantity = 0;
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                enteredQuantity = Convert.ToSingle(textBox2.Text); // Получаем введенное количество товара из textBox2
            }
            if (enteredQuantity > availableQuantity)
            {
                MessageBox.Show("Вы не можете указать количество товара больше, чем на складе!");
                textBox2.Text = availableQuantity.ToString(); // Устанавливаем в textBox2 максимально доступное количество товара
            }

            float currentQuantity = 0;
            if (float.TryParse(label4.Text.Replace(",", "."), out currentQuantity)) // Преобразуем значение в label4 в число, заменяя запятую на точку
            {
                float multipliedQuantity = currentQuantity * enteredQuantity; // Выполняем умножение

                textBox4.Text = multipliedQuantity.ToString("#,##0.00"); // Записываем результат в textBox4 с форматом "--,--"
            }
            else
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Значение в label4 не может быть преобразовано в число.");
            }
        }

            private void label15_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            float currentQuantity = 0;
            if (float.TryParse(label4.Text, out currentQuantity)) // Преобразуем значение в label4 в число
            {
                float enteredQuantity = 0;
                if (float.TryParse(textBox2.Text, out enteredQuantity)) // Преобразуем значение в textBox2 в число
                {
                    // Пропущено умножение
                }
                else
                {
                    // Выводим сообщение об ошибке
                    MessageBox.Show("Вы ввели некорректное значение в textBox2. Пожалуйста, введите число.");
                }
            }
            else
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Значение в label4 не может быть преобразовано в число.");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбран элемент в comboBox1
            if (comboBox1.SelectedItem != null)
            {
                // Получаем выбранный элемент в comboBox1
                string selectedProduct = comboBox1.SelectedItem.ToString();

                // Проверяем, что введено значение в textBox2
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    // Парсим значение из textBox2 в число
                    if (float.TryParse(textBox2.Text, out float enteredQuantity))
                    {
                        // Проверяем, что значение в label4 может быть преобразовано в число
                        if (float.TryParse(label4.Text, out float currentPrice))
                        {
                            // Вычисляем стоимость
                            float totalCost = enteredQuantity * currentPrice;

                            // Проверяем, что введено значение в textBox4
                            if (!string.IsNullOrEmpty(textBox4.Text))
                            {
                                // Парсим значение из textBox4 в число
                                if (float.TryParse(textBox4.Text, out float enteredTotalCost))
                                {
                                    // Добавляем данные в таблицу "Заказы"
                                    string sql = "INSERT INTO Заказы (Код_товара, Количество, Цена, Стоимость) VALUES (@Код_товара, @Количество, @Цена, @Стоимость)";
                                    using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
                                    {
                                        using (SqlCommand command = new SqlCommand(sql, connection))
                                        {
                                            // Добавляем параметры в команду
                                            command.Parameters.AddWithValue("@Код_товара", selectedProduct);
                                            command.Parameters.AddWithValue("@Количество", enteredQuantity);
                                            command.Parameters.AddWithValue("@Цена", currentPrice);
                                            command.Parameters.AddWithValue("@Стоимость", enteredTotalCost);

                                            // Открываем соединение
                                            connection.Open();

                                            // Выполняем команду
                                            command.ExecuteNonQuery();

                                            // Закрываем соединение
                                            connection.Close();
                                        }
                                    }

                                    // Выводим сообщение об успешном добавлении данных
                                    MessageBox.Show("Данные успешно добавлены в таблицу 'Заказы'.");
                                }
                                else
                                {
                                    // Выводим сообщение об ошибке
                                    MessageBox.Show("Вы ввели некорректное значение в textBox4. Пожалуйста, введите число.");
                                }
                            }
                            else
                            {
                                // Выводим сообщение об ошибке
                                MessageBox.Show("Значение в textBox4 не может быть пустым.");
                            }
                        }
                        else
                        {
                            // Выводим сообщение об ошибке
                            MessageBox.Show("Значение в label4 не может быть преобразовано в число.");
                        }
                    }
                    else
                    {
                        // Выводим сообщение об ошибке
                        MessageBox.Show("Вы ввели некорректное значение в textBox2. Пожалуйста, введите число.");
                    }
                }
                else
                {
                    // Выводим сообщение об ошибке
                    MessageBox.Show("Введите количество !");
                }
            }
            else
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Необходимо выбрать товар из списка.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Заказы", connection))
                {
                    // Создаем новый DataTable
                    DataTable dt = new DataTable();
                    // Заполняем DataTable данными из таблицы "Заказы"
                    adapter.Fill(dt);
                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли выделенные строки в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Если есть выделенные строки, то удаляем их из таблицы "Заказы"
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // Получаем значение в ячейке "Код_товара" текущей строки
                    int orderId = Convert.ToInt32(row.Cells["Код_товара"].Value);
                    // Удаляем строку из таблицы "Заказы" по "Код_товара"
                    DeleteOrder(orderId);
                }
            }
            else
            {
                // Если нет выделенных строк, то выводим сообщение
                MessageBox.Show("Выделите строку(и) для удаления.");
            }

            // Обновляем таблицу "Заказы" после удаления
            UpdateOrderTable();
        }

        // Метод для удаления заказа по "Код_заказа"
        private void DeleteOrder(int orderId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Заказы WHERE Код_товара = @orderId", connection))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Метод для удаления всех заказов
        private void DeleteAllOrders()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Заказы", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Метод для обновления таблицы "Заказы"
        private void UpdateOrderTable()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Заказы", connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Проверяем заполнение label10_Click
            if (string.IsNullOrEmpty(label10.Text))
            {
                MessageBox.Show("Пожалуйста, укажите дату!");
                return;
            }

            // Получаем значения из элементов управления
            string номерЧека = textBox1.Text;
            int кодСотрудника;
            if (comboBox3.SelectedValue != null && int.TryParse(comboBox3.SelectedValue.ToString(), out кодСотрудника))
            {
                DateTime дата = DateTime.Parse(label8.Text);
                bool дисконтнаяКарта = checkBox1.Checked;
                decimal скидка = 0m;
                if (дисконтнаяКарта)
                {
                    скидка = Convert.ToDecimal(textBox3.Text);
                }

                // Считываем значение стоимости из таблицы "Заказы"
                decimal стоимость = 0m;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        стоимость += Convert.ToDecimal(row.Cells["Стоимость"].Value);
                    }
                }

                // Вычисляем сумму к оплате с учетом скидки
                decimal суммаКОплате = 0m;
                if (дисконтнаяКарта)
                {
                    суммаКОплате = стоимость - (стоимость * скидка / 100);
                }
                else
                {
                    суммаКОплате = стоимость;
                }

                // Выполняем вставку данных в таблицу "Счета"
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Счета (Код_сотрудника, Дата, Дисконтная_карта, Скидка, Сумма_к_оплате) VALUES (@КодСотрудника, @Дата, @ДисконтнаяКарта, @Скидка, @СуммаКОплате)", connection))
                    {
                        command.Parameters.AddWithValue("@КодСотрудника", кодСотрудника);
                        command.Parameters.AddWithValue("@Дата", дата);
                        command.Parameters.AddWithValue("@ДисконтнаяКарта", дисконтнаяКарта);
                        command.Parameters.AddWithValue("@Скидка", скидка);
                        command.Parameters.AddWithValue("@СуммаКОплате", суммаКОплате);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Данные успешно сохранены в таблицу 'Счета'.");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника и убедитесь, что значение является числом");
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }

}
 

