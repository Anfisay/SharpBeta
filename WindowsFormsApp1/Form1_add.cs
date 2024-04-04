using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // add fio
        private void buttonAddFio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxSurname.Text) || string.IsNullOrEmpty(this.textBoxName.Text) || string.IsNullOrEmpty(this.textBoxPatronym.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                string sql = "INSERT INTO tourists(lastname, firstname, patronym) VALUES(@lastname, @firstname, @patronym)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("lastname", this.textBoxSurname.Text);
                cmd.Parameters.AddWithValue("firstname", this.textBoxName.Text);
                cmd.Parameters.AddWithValue("patronym", this.textBoxPatronym.Text);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                this.textBoxSurname.Text = "";
                this.textBoxName.Text = "";
                this.textBoxPatronym.Text = "";

                loadTourists();
            }
        }

        // add Tour 
        private void buttonAddTour_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxTourName.Text) || string.IsNullOrEmpty(this.textBoxTourPrice.Text) || string.IsNullOrEmpty(this.textBoxTourInfo.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                string sql = "INSERT INTO tours(name, price, info) VALUES(@name, @price, @info)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("name", this.textBoxTourName.Text);
                cmd.Parameters.AddWithValue("price", Decimal.Parse(this.textBoxTourPrice.Text));
                cmd.Parameters.AddWithValue("info", this.textBoxTourInfo.Text);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                this.textBoxTourName.Text = "";
                this.textBoxTourPrice.Text = "";
                this.textBoxTourInfo.Text = "";

                loadTours();
            }
        }

        // add tourists info
        private void buttonAddTouristInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.touristId.Text) || string.IsNullOrEmpty(this.touristPassport.Text) || string.IsNullOrEmpty(this.touristCity.Text) || string.IsNullOrEmpty(this.touristPhone.Text) || string.IsNullOrEmpty(this.touristCountry.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                string sql = "INSERT INTO touristsinfo(idtourist, passport, city, country, phone, index) VALUES(@idtourist, @passport, @city, @country, @phone, @index)";
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
            }
        }

        // add season
        private void buttonAddSeason_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxIDTour.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                string sql = "INSERT INTO seasons(idtour, startDate, endDate, closeSeason, seats) VALUES(@idtour, @startDate, @endDate, @closeSeason, @seats)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

                DateTime dateStartTime = this.seasonDateOpen.Value.Date + this.seasonTimeOpen.Value.TimeOfDay;
                DateTime dateEndTime = this.seasonDateClose.Value.Date + this.seasonTimeClose.Value.TimeOfDay;

                cmd.Parameters.AddWithValue("idtour", Decimal.Parse(this.textBoxIDTour.Text));
                cmd.Parameters.AddWithValue("startDate", dateStartTime);
                cmd.Parameters.AddWithValue("endDate", dateStartTime);


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
                this.seasonDateOpen = null;
                this.seasonTimeOpen = null;
                this.seasonTimeClose = null;
                this.seasonDateClose = null;
                this.numSeats.Value = 0;

                loadSeasons();
            }
        }



        // общая кнопка добавления
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int id = this.tabControl1.SelectedIndex;

            if (id == 0)
            {
                this.groupBox1.Visible = true;
                this.groupBox1.Text = "Добавление нового туриста";
                this.buttonAddFio.Visible = true;
                this.buttonChangeFio.Visible = false;
            }
            else if (id == 1)
            {
                this.groupBox2.Visible = true;
                this.groupBox2.Text = "Добавление информации о туристе";
                this.buttonAddTouristsInfo.Visible = true;
                this.buttonChangeTouristsInfo.Visible = false;
            }
            else if (id == 2)
            {
                this.groupBox3.Visible = true;
                this.groupBox3.Text = "Добавление нового тура";
                this.buttonAddTour.Visible = true;
                this.buttonChangeTour.Visible = false;
            }
            else if (id == 3)
            {
                this.groupBox4.Visible = true;
                this.groupBox4.Text = "Добавление нового сезона";
                this.buttonAddSeason.Visible = true;
                this.buttonChangeSeason.Visible = false;
            }

            //для запроса
            if (id == 5)
            {
                this.groupBoxReq.Visible = true;
            }

        }
    }
}
