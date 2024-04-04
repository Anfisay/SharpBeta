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

            int id = this.tabControl1.SelectedIndex;
            int row = 0;

            if (id == 0) row = this.dataGridView1.CurrentCell.RowIndex;
            else if (id == 1) row = this.dataGridView2.CurrentCell.RowIndex;
            else if (id == 2) row = this.dataGridView3.CurrentCell.RowIndex;
            else if (id == 3) row = this.dataGridView4.CurrentCell.RowIndex;
            else if (id == 4) row = this.dataGridView5.CurrentCell.RowIndex;

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
                this.groupBox1.Text = "Изменение туриста";
                fillFormFio(row);
            }
            else if (id == 1)
            {
                this.groupBox2.Visible = true;
                this.buttonAddTouristsInfo.Visible = false;
                this.buttonChangeTouristsInfo.Visible = true;
                this.groupBox2.Text = "Изменение информации о туристе";
                fillFormTouristInfo(row);
            }
            else if (id == 2)
            {
                this.groupBox3.Visible = true;
                this.buttonAddTour.Visible = false;
                this.buttonChangeTour.Visible = true;
                this.labelTour.Text = this.dataGridView3[0, row].Value.ToString();
                this.groupBox3.Text = "Изменение тура";
                fillFormTour(row);
        }
            else if (id == 3)
            {
                this.groupBox4.Visible = true;
                this.buttonAddSeason.Visible = false;
                this.buttonChangeSeason.Visible = true;
                this.labelSeason.Text = this.dataGridView4[0, row].Value.ToString();
                this.groupBox4.Text = "Изменение сезона";
                fillFormSeason(row);
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
        // изменение инфы о туристах
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
            this.groupBox2.Visible = false;
        }

        // изменение инфы о турах
        private void buttonChangeTour_Click(object sender, EventArgs e)
        {

            string sql = "UPDATE tours SET name=@name, price=@price, info=@info WHERE id=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", Decimal.Parse(this.labelTour.Text));
            cmd.Parameters.AddWithValue("name", this.textBoxTourName.Text);
            cmd.Parameters.AddWithValue("price", Decimal.Parse(this.textBoxTourPrice.Text));
            cmd.Parameters.AddWithValue("info", this.textBoxTourInfo.Text);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            this.textBoxTourName.Text = "";
            this.textBoxTourPrice.Text = "";
            this.textBoxTourInfo.Text = "";

            loadTours();

            this.groupBox3.Visible = false;
        }

        private void buttonChangeSeason_Click(object sender, EventArgs e)
        {

            string sql = "UPDATE seasons SET idtour=@idtour, startDate=@startDate, endDate=@endDate, closeSeason=@closeSeason, seats=@seats WHERE id=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            DateTime dateStartTime = this.seasonDateOpen.Value.Date + this.seasonTimeOpen.Value.TimeOfDay;
            DateTime dateEndTime = this.seasonDateClose.Value.Date + this.seasonTimeClose.Value.TimeOfDay;
            
            cmd.Parameters.AddWithValue("id", Decimal.Parse(this.labelSeason.Text));
            cmd.Parameters.AddWithValue("idtour", Decimal.Parse(this.textBoxIDTour.Text));
            cmd.Parameters.AddWithValue("startDate", dateStartTime);
            cmd.Parameters.AddWithValue("endDate", dateEndTime);

            if (this.checkBoxSeason.Checked)
            {
                cmd.Parameters.AddWithValue("closeSeason", true);
        }
            else
            {
                cmd.Parameters.AddWithValue("closeSeason", false);
            }

            cmd.Parameters.AddWithValue("seats", Convert.ToInt32(Math.Round(numSeats.Value, 0)));
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            this.textBoxIDTour.Text = "";
            this.textBoxIDTour.Text = "";

            this.numSeats.Value = 0;

            loadSeasons();

            this.groupBox4.Visible = false;
        }


        // заполнение формы, чтобы изменить объект
        private void fillFormFio(int row)
        {
            this.textBoxSurname.Text = this.dataGridView1[2, row].Value.ToString();
            this.textBoxName.Text = this.dataGridView1[1, row].Value.ToString();
            this.textBoxPatronym.Text = this.dataGridView1[3, row].Value.ToString();
        }
        private void fillFormTouristInfo(int row)
        {
            this.touristId.Text = this.dataGridView2[0, row].Value.ToString();
            this.touristPassport.Text = this.dataGridView2[1, row].Value.ToString();
            this.touristCountry.Text = this.dataGridView2[2, row].Value.ToString();
            this.touristCity.Text = this.dataGridView2[3, row].Value.ToString();
            this.touristPhone.Text = this.dataGridView2[4, row].Value.ToString();
            this.touristIndex.Text = this.dataGridView2[5, row].Value.ToString();
        }
        private void fillFormTour(int row)
        {
            this.textBoxTourName.Text = this.dataGridView3[1, row].Value.ToString();
            this.textBoxTourPrice.Text = this.dataGridView3[2, row].Value.ToString();
            this.textBoxTourInfo.Text = this.dataGridView3[3, row].Value.ToString();
        }
        private void fillFormSeason(int row)
        {
            this.textBoxIDTour.Text = this.dataGridView4[1, row].Value.ToString();

            if (Convert.ToBoolean(this.dataGridView4[4, row].Value) == true)
            {
                this.checkBoxSeason.Checked = true;
            }
            else
            {
                this.checkBoxSeason.Checked = false;
            }

            string startData = this.dataGridView4[2, row].Value.ToString().Substring(0, 10);

            string startTime;

            if (this.dataGridView4[2, row].Value.ToString().Length == 16)
            {
                startTime = this.dataGridView4[2, row].Value.ToString().Substring(11, 5);
            }
            else
            {
                startTime = this.dataGridView4[2, row].Value.ToString().Substring(11, 4);
            }

            this.seasonDateOpen.Value = DateTime.Parse(startData);
            this.seasonTimeOpen.Value = DateTime.Parse(startTime);


            // ТО ЖЕ ТОЛЬКО ДЛЯ ВТОРОЙ ДАТЫ
            string endData = this.dataGridView4[3, row].Value.ToString().Substring(0, 10);

            string endTime;

            if (this.dataGridView4[3, row].Value.ToString().Length == 16)
            {
                endTime = this.dataGridView4[3, row].Value.ToString().Substring(11, 5);
            }
            else
            {
                endTime = this.dataGridView4[3, row].Value.ToString().Substring(11, 4);
            }

            this.seasonDateClose.Value = DateTime.Parse(endData);
            this.seasonTimeClose.Value = DateTime.Parse(endTime);
        }
    }
}
