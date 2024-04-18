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
        // че происходит если нажимаем на кнопку удаления
        private void delete_Click(object sender, EventArgs e)
        {
            int id = this.tabControl1.SelectedIndex;
            int row = 0;

            if (id == 0) row = this.dataGridView1.CurrentCell.RowIndex;
            else if (id == 1) row = this.dataGridView2.CurrentCell.RowIndex;
            else if (id == 2) row = this.dataGridView3.CurrentCell.RowIndex;
            else if (id == 3) row = this.dataGridView4.CurrentCell.RowIndex;
            else if (id == 4) row = this.dataGridView5.CurrentCell.RowIndex;

            deleteRow(row, id);
        }

        private void deleteRow(int row, int id)
        {
            if (id == 0) { deleteTourist(row); }

            else if (id == 1) { deleteTouristInfo(row); }

            else if (id == 2) { deleteTour(row); }

            else if (id == 3) { deleteSeason(row); }

            else if (id == 4) { deletePayment(row); }

        }

        private void deleteTourist(int row)
        {
            // получаем индекс туриста
            int id = Convert.ToInt32(this.dataGridView1[0, row].Value.ToString());

            // затем удаляем из таблицы
            string sql = "DELETE FROM tourists WHERE id=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // перегружаем все таблицы, так как там связи и могут быть изменения
            loadAll();
        }

        private void deleteTouristInfo(int row)
        {
            // получаем индекс туриста
            int id = Convert.ToInt32(this.dataGridView2[0, row].Value.ToString());

            // затем удаляем из таблицы
            string sql = "DELETE FROM touristsinfo WHERE idtourist=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // перегружаем все таблицы, так как там связи и могут быть изменения
            loadAll();
        }

        private void deleteTour(int row)
        {
            // получаем индекс тура
            int id = Convert.ToInt32(this.dataGridView3[0, row].Value.ToString());

            // затем удаляем из таблицы
            string sql = "DELETE FROM tours WHERE id=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // перегружаем все таблицы, так как там связи и могут быть изменения
            loadAll();
        }

        private void deleteSeason(int row)
        {
            // получаем индекс тура
            int id = Convert.ToInt32(this.dataGridView4[0, row].Value.ToString());

            // затем удаляем из таблицы
            string sql = "DELETE FROM seasons WHERE id=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // перегружаем все таблицы, так как там связи и могут быть изменения
            loadAll();
        }

        private void deletePayment(int row)
        {
            // получаем индекс платежа
            int id = Convert.ToInt32(this.dataGridView5[0, row].Value.ToString());

            // затем удаляем из таблицы
            string sql = "DELETE FROM payment WHERE id=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // перегружаем все таблицы, так как там связи и могут быть изменения
            loadAll();
        }
    }
}
