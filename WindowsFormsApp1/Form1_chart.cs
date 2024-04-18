using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        // сначала нужно получить все данные оплаты
        private void btnColumnChart_Click(object sender, EventArgs e)
        {
            // очень удобно конечно прыгать через миллион таблиц...

            List<int> trips = getTrips();
            List<decimal> costs = getCosts(trips);

            List<int> tourists = getTourists(trips);

            

            // Установим палитру
            columnChart.Palette = ChartColorPalette.Pastel;

            // Заголовок графика
            columnChart.Titles.Add("Сумма выкупа каждого туриста");

            // Добавляем последовательность
            for (int i = 0; i < tourists.Count; i++)
            {
                Series series = columnChart.Series.Add(tourists[i].ToString());

                // Добавляем точку
                series.Points.Add((double)costs[i]);
            }
        }

        // получаем все trip
        private List<int> getTrips()
        {
            List<int> trips = new List<int>();

            string sql = "SELECT idtrip FROM payment";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con); 
            cmd.Prepare();

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                trips.Add(reader.GetInt32(0));
            }
            reader.Close();

            return trips;
        }

        // получаем список туристов
        private List<int> getTourists(List<int> trips)
        {
            List<int> tourists = new List<int>();

            for (int i = 0; i < trips.Count; i++)
            {
                string sql = "SELECT idtourist, idseason FROM trips WHERE id=@id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", trips[i]);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                int tourist = 0;

                while (reader.Read())
                {
                    tourist = reader.GetInt32(0);

                    if (!(tourists.Contains(tourist)))
                    {
                        tourists.Add(reader.GetInt32(0));
                    }

                }

                reader.Close();
            }

            return tourists;
        }


        private List<decimal> getCosts(List<int> trips)
        {
            List<int> tourists = new List<int>();
            List<decimal> cost = new List<decimal>();

            for (int i = 0; i < trips.Count; i++)
            {
                string sql = "SELECT idtourist, idseason FROM trips WHERE id=@id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", trips[i]);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                int season = 0;
                int tourist = 0;

                while (reader.Read())
                {
                    tourist = reader.GetInt32(0);
                    season = reader.GetInt32(1);

                    if (!(tourists.Contains(tourist)))
                    {
                        tourists.Add(reader.GetInt32(0));
                        cost.Add(0);
                    }

                }

                reader.Close();

                // теперь берем сезон и добавляем его 
                sql = "SELECT idtour FROM seasons WHERE id=@id";
                cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", season);

                reader = cmd.ExecuteReader();
                int tour = 0;

                while (reader.Read())
                {
                    tour= reader.GetInt32(0);
                }

                reader.Close();

                sql = "SELECT price FROM tours WHERE id=@id";
                cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", tour);

                reader = cmd.ExecuteReader();

                decimal price = 0;

                while (reader.Read())
                {
                    price = reader.GetDecimal(0);
                }
                reader.Close();

                cost[tourists.IndexOf(tourist)] += price;

            }



            return cost;
        }



    }
}


