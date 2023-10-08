using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kurs2;
namespace Kurs2

{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT Товары.[Код_товара], Товары.Наименование, (Товары.[Цена_закупочная]*Товары.[Наценка,%]+Товары.[Цена_закупочная]) AS[Отпускная_цена], Товары.[Срок_реализации] FROM Товары";
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView6.DataSource = dataTable;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-8OFF3I4\\SQLEXPRESS02;Initial Catalog=Trading_company;Integrated Security=True";
            string query = "SELECT Код_товара, Наименование, Цена_реализации, Срок_реализации FROM Товары";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView6.DataSource = dataTable;
            }

            PrintDocument printDoc = new PrintDocument();
            DataGridViewPrinter dgvPrinter = new DataGridViewPrinter(this.dataGridView6);
            printDoc.PrintPage += new PrintPageEventHandler(dgvPrinter.PrintDocument_PrintPage);
            printDoc.Print();
        }


    }
}
