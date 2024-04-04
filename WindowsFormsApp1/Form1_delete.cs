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
            // здесь нужно получить номер вкладки и номер строки
            // номер строки
            int row = this.dataGridView1.CurrentCell.RowIndex;
            int id = this.tabControl1.SelectedIndex;

            deleteRow(row, id);
        }

        private void deleteRow(int row, int id)
        {
            if (id == 0)
            {
                deleteTourist(row);
            }
            else if (id==1)
            {
                deleteTouristInfo(row);
            }
            // аналогично для остальных
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


        //что я добавляла
        private void deleteTouristInfo(int row)
        {
            // получаем индекс туриста
            int id = Convert.ToInt32(this.dataGridView1[0, row].Value.ToString());

            // затем удаляем из таблицы
            string sql = "DELETE FROM touristsInfo WHERE idtourist=@id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // перегружаем все таблицы, так как там связи и могут быть изменения
            loadAll();
        }
    }
}
