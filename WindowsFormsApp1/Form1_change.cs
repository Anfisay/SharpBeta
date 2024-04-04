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
        private void buttonChange_Click(object sender, EventArgs e)
        {
            // здесь нужно получить номер вкладки и номер строки
            // номер строки
            int row = this.dataGridView1.CurrentCell.RowIndex;
            int id = this.tabControl1.SelectedIndex;

            showChangeForm(row, id);
        }

        // показываем форму для изменения
        private void showChangeForm(int row, int id)
        {
            if (id == 0)
            {
                this.groupBox1.Visible = true;
                this.buttonAddFio.Visible = false;
                this.buttonChangeFio.Visible = true;
                this.labelTouristId.Text = this.dataGridView1[0, row].Value.ToString();
                this.groupBox1.Text = "Изменение записи";
                fillFormFio(row);
            }
            // TODO аналогично делаем для остального
            if (id == 1)
            {
                this.groupBox2.Visible = true;
                this.buttonAddTouristsInfo.Visible = false;
                this.buttonChangeTouristsInfo.Visible = true;
                this.touristId.Text = this.dataGridView2[0, row].Value.ToString();
                this.groupBox2.Text = "Изменение записи";
                fillFormTouristInfo(row);
            }
        }

        // изменение фио
        private void buttonChangeFio_Click(object sender, EventArgs e)
        {
            // изменение фио
            if (string.IsNullOrEmpty(this.textBoxSurname.Text) || string.IsNullOrEmpty(this.textBoxName.Text) || string.IsNullOrEmpty(this.textBoxPatronym.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                string sql = "UPDATE tourists SET firstname=@firstname,lastname=@lastname, patronym=@patronym WHERE id=@id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("lastname", this.textBoxSurname.Text);
                cmd.Parameters.AddWithValue("firstname", this.textBoxName.Text);
                cmd.Parameters.AddWithValue("patronym", this.textBoxPatronym.Text);
                cmd.Parameters.AddWithValue("id", Decimal.Parse(this.labelTouristId.Text));
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                this.textBoxSurname.Text = "";
                this.textBoxName.Text = "";
                this.textBoxPatronym.Text = "";

                //скрываем форму
                this.groupBox1.Visible = false;

                loadTourists();
            }

        }
        // из
        private void buttonChangeTouristsInfo_Click(object sender, EventArgs e)
        {
            
            string sql = "UPDATE touristsinfo SET idtourist=@idtourist,passport=@passport, city=@city, country=@country, phone=@phone, index=@index WHERE idtourist=@idtourist";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("idtourist", Decimal.Parse(this.touristId.Text));
            cmd.Parameters.AddWithValue("passport", this.touristPassport.Text);
            cmd.Parameters.AddWithValue("city", this.touristCity.Text);
            cmd.Parameters.AddWithValue("country", this.touristCountry.Text);
            cmd.Parameters.AddWithValue("phone", this.touristPhone.Text);
            cmd.Parameters.AddWithValue("index", Decimal.Parse(this.touristIndex.Text));
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            this.touristId.Text = "";
            this.touristPassport.Text = "";
            this.touristCity.Text = "";
            this.touristCountry.Text = "";
            this.touristPhone.Text = "";
            this.touristIndex.Text = "";

            loadTourInfo();

            //скрываем форму
            this.groupBox1.Visible = false;
            

        }

        // изменение инфы о туристах



        // заполнение формы, чтобы изменить объект
        private void fillFormFio(int row)
        {
            this.textBoxSurname.Text = this.dataGridView1[2, row].Value.ToString();
            this.textBoxName.Text = this.dataGridView1[1, row].Value.ToString();
            this.textBoxPatronym.Text = this.dataGridView1[3, row].Value.ToString();
        }
        private void fillFormTouristInfo(int row)
        {
            this.touristPassport.Text = this.dataGridView2[1, row].Value.ToString();
            this.touristCountry.Text = this.dataGridView2[2, row].Value.ToString();
            this.touristCity.Text = this.dataGridView2[3, row].Value.ToString();
            this.touristPhone.Text = this.dataGridView2[4, row].Value.ToString();
            this.touristIndex.Text = this.dataGridView2[5, row].Value.ToString();
        }
    }
}
