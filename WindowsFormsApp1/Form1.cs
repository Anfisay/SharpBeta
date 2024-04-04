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

        //подключение к БД
        private NpgsqlConnection con;
        private string connString =
        "Host=127.0.0.1;Username=postgres;Password=Zerg6161;Database=TourFirm";

        public Form1()
        {
            InitializeComponent();
            con = new NpgsqlConnection(connString);
            con.Open();
            loadTourists();//делаем выборку данных из таблицы tourists
            loadTourInfo();
            loadTours();
            loadSeasons();
            loadPayment();


            this.MinimumSize = new Size(1000, 600);

            // скрываем кнопки удаления и изменения
            // они будут доступны только после выбора конкретной записи
            //this.buttonChange.Visible = false;
            //this.buttonDelete.Visible = false;

            // скрываем формы
            this.groupBox1.Visible = false;
            this.groupBox2.Visible = false;
            this.groupBox3.Visible = false;
            this.groupBox4.Visible = false;

            this.groupBoxReq.Visible = false;

            // чтобы можно было выбрать сразу строчку при клике на ячейку
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {

        }

        // обработчики кнопки закрыть
        private void buttonCloseFio_Click(object sender, EventArgs e) { this.groupBox1.Visible = false; }
        private void buttonCloseTouristsInfo_Click(object sender, EventArgs e) { this.groupBox2.Visible = false; }
        private void buttonCloseTour_Click(object sender, EventArgs e) { this.groupBox3.Visible = false; }


        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void seasonTimeOpen_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        // показываем кнопки удаления и изменения
        private void showButtons()
        {
            this.buttonChange.Visible = true;
            this.buttonDelete.Visible = true;
        }



        // обработчики для кликанья строчек таблицы
        // пока закомментим, кажется, это не нужно
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { /*showButtons();*/ }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { /*showButtons();*/ }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) { /*showButtons();*/ }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }





        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        }
        }

