using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string req1 = null;
        string req2 = null;

        private void load_tables()
        {
            loadTourists();
            loadTourInfo();
            loadSeasons();
            loadPayment();
            loadTours();
        }
        
        private void buttonDoReq1_Click(object sender, EventArgs e)
        {
            try
            {
                req1 = this.textBoxReq.Text;

                DataTable dataTable = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand(req1, con);

                //создание таблицы с результатом запроса из бд
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dataTable);
                dataGridView6.DataSource = dataTable;


                this.textBoxReq.Text = "";
                MessageBox.Show("Запрос успешно выполнен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения запроса: " + ex.Message);
            }
        }
        private void buttonDoReq2_Click(object sender, EventArgs e)
        {
            try
            {
                req2 = richTextReq.Text;

                DataTable dataTable = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand(req2, con);

                //создание таблицы с результатом запроса из бд
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dataTable);
                dataGridView6.DataSource = dataTable;

                this.richTextReq.Text = "";

                //обновление всех таблиц в других вкладках
                load_tables();

                MessageBox.Show("Запрос успешно выполнен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения запроса: " + ex.Message);
            }
        }

    }
}
