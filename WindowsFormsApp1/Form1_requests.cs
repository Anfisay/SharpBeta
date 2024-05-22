using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Xml;

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
                //приветттттттттт
                //прыветт
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

        private void buttonXmlWriter_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();//ПРОВЕРЬ
            d.Filter = "XML Files(*.xml*)|*.xml*";//
            d.ShowDialog();//
            if (d.FileName != null)//
            {
                XmlWriter xmlWriter = XmlWriter.Create(d.FileName);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Parent");

                List<string> listNames = new List<string>();
                for (int i = 0; i < dataGridView6.Columns.Count; i++)
                {
                    listNames.Add(dataGridView6.Columns[i].HeaderText);
                }


                for (int i = 0; i < dataGridView6.Rows.Count - 1; i++)
                {
                    xmlWriter.WriteStartElement("child");
                    xmlWriter.WriteAttributeString("id", dataGridView6.Rows[i].Cells[0].Value.ToString());
                    for (int j = 1; j < dataGridView6.Rows[i].Cells.Count; j++)
                    {
                        //xmlWriter.WriteStartElement(listNames[j]);
                        xmlWriter.WriteStartElement(dataGridView6.Columns[j].HeaderText);
                        xmlWriter.WriteString(dataGridView6.Rows[i].Cells[j].Value.ToString());
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();


                }
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
        }

        private void buttonXmlReader_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "XML Files(*.xml)|*.xml";
            if (d.ShowDialog() == DialogResult.OK)
            {
                XmlReader xmlReader = XmlReader.Create(d.FileName);
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlReader);
                xmlReader.Close();

                if (dataSet.Tables.Count > 0)
                {
                    dataGridView6.DataSource = dataSet.Tables[0];
                }
            }
        }


        private void buttonXmlDocumentExp_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "XML Files(*.xml*)|*.xml*";
            d.ShowDialog();
            if (d.FileName != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement rootElement = xmlDoc.CreateElement("Results");
                xmlDoc.AppendChild(rootElement);

                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.IsNewRow) continue;

                    XmlElement rowElement = xmlDoc.CreateElement("Row");
                    rootElement.AppendChild(rowElement);

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        XmlElement cellElement = xmlDoc.CreateElement(dataGridView6.Columns[i].Name);
                        cellElement.InnerText = row.Cells[i].Value?.ToString() ?? "";
                        rowElement.AppendChild(cellElement);
                    }
                }

                xmlDoc.Save(d.FileName);
            }
        }


        private void buttonXmlDocumentImp_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();//ПРОВЕРЬ
            d.Filter = "XML Files(*.xml*)|*.xml*";//
            d.ShowDialog();//
            if (d.FileName != null)//
            {
                DataTable dataTable = new DataTable();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(d.FileName);

                XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Results/Row");

                if (xmlNodeList.Count > 0)
                {

                    foreach (XmlNode node in xmlNodeList[0].ChildNodes)
                    {
                        dataTable.Columns.Add(node.Name);
                    }

                    foreach (XmlNode node in xmlNodeList)
                    {
                        DataRow dataRow = dataTable.NewRow();

                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            dataRow[childNode.Name] = childNode.InnerText;
                        }

                        dataTable.Rows.Add(dataRow);
                    }

                    dataGridView6.DataSource = dataTable;
                }
            }
        }


    }
}
