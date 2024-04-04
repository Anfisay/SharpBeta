using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Npgsql;//класс для работы с БД Postgres

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private void loadTourists()
        {
            DataTable dt = new DataTable();
            //формируем запрос к БД (выборка данных)
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM tourists", con);
            adap.Fill(dt);
            //отображаем данные в dataGridView1
            dataGridView1.DataSource = dt;
        }

        // загрузка данных на экран
        private void loadTourInfo()
        {
            DataTable dt = new DataTable();
            //формируем запрос к БД (выборка данных)
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM touristsInfo", con);
            adap.Fill(dt);
            //отображаем данные в dataGridView1
            dataGridView2.DataSource = dt;
        }

        private void loadTours()
        {
            DataTable dt = new DataTable();
            //формируем запрос к БД (выборка данных)
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM tours", con);
            adap.Fill(dt);
            //отображаем данные в dataGridView1
            dataGridView3.DataSource = dt;
        }

        private void loadSeasons()
        {
            DataTable dt = new DataTable();
            //формируем запрос к БД (выборка данных)
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM seasons", con);
            adap.Fill(dt);
            //отображаем данные в dataGridView1
            dataGridView4.DataSource = dt;
        }

        private void loadPayment()
        {
            DataTable dt = new DataTable();
            //формируем запрос к БД (выборка данных)
            NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM payment", con);
            adap.Fill(dt);
            //отображаем данные в dataGridView1
            dataGridView5.DataSource = dt;
        }

        // функция для загрузки всех таблиц
        private void loadAll()
        {
            loadTourists();
            loadTourInfo();
            loadSeasons();
            loadTours();
            loadPayment();
        }
    }
}
