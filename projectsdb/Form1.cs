using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace projectsdb
{
    public partial class Form1 : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\projectsdb\DB\Products.mdf;Integrated Security=True;Connect Timeout=30");
        SqlConnection sqlFri = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\projectsdb\DB\Dostavka.mdf;Integrated Security=True;Connect Timeout=30");
        int ProductID = 0;
        int DostavkaID = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                try
                {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                if (button1.Text == "Save")
                {
                    SqlCommand sqlCmd = new SqlCommand("ProductAddOrEdit", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@ProductID", 0);
                    sqlCmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Gruppid", comboBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Hind", textBox2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Calorii", textBox3.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Saved successfull");
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("ProductAddOrEdit", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlCmd.Parameters.AddWithValue("@ProductID", ProductID);
                    sqlCmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Gruppid", comboBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Hind", textBox2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Calorii", textBox3.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Updated successfull");
                }
                Reset();
                FillDataGridView();
            }
            catch (Exception ex) // vse owibki
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlCon.Close();
            }
        }
            else if (radioButton2.Checked == true)
            {

                try
                {
                    if (sqlFri.State == ConnectionState.Closed)
                        sqlFri.Open();
                    if (button1.Text == "Save")
                    {
                        SqlCommand sqlCmd = new SqlCommand("DostavkaAddOrEdit", sqlFri);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@mode", "Add");
                        sqlCmd.Parameters.AddWithValue("@DostavkaID", 0);
                        sqlCmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Gruppid", comboBox1.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Hind", textBox2.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Firma", textBox3.Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Saved successfull");
                    }
                    else
                    {
                        SqlCommand sqlCmd = new SqlCommand("DostavkaAddOrEdit", sqlFri);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                        sqlCmd.Parameters.AddWithValue("@DostavkaID", DostavkaID);
                        sqlCmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Gruppid", comboBox1.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Hind", textBox2.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Firma", textBox3.Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Updated successfull");
                    }
                    Reset();
                    FillDataGridView();
                }
                catch (Exception ex) // vse owibki
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
                finally
                {
                    sqlFri.Close();
                }
            }
        }
        void FillDataGridView()
        {
            if (radioButton1.Checked == true)
            {
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("ProductViewOrSearch", sqlCon);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@ProductName", textBox4.Text.Trim());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;
                    dataGridView1.Columns[0].Visible = false;

                    sqlCon.Close();
                }
            }
            else if (radioButton2.Checked == true)
            {
                if (sqlFri.State == ConnectionState.Closed)
                    sqlFri.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("DostavkaViewOrSearch", sqlFri);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@DostavkaName", textBox4.Text.Trim());
                DataTable dtbls = new DataTable();
                sqlDa.Fill(dtbls);
                dataGridView1.DataSource = dtbls;
                dataGridView1.Columns[0].Visible = false;

                sqlFri.Close();
            }
        }
        void Reset()
        {
            if (radioButton1.Checked == true)
            {
                comboBox1.Text = textBox1.Text = textBox2.Text = textBox3.Text = "";
                button1.Text = "Save";
                ProductID = 0;
                button2.Enabled = false;
            }
            else if (radioButton2.Checked == true)
            {
                comboBox1.Text = textBox1.Text = textBox2.Text = textBox3.Text = "";
                button1.Text = "Save";
                DostavkaID = 0;
                button2.Enabled = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("ProductDeletion", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ProductID", ProductID);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Deteled successfull");
                    Reset();
                    FillDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
            }
            else if (radioButton2.Checked == true)
                try
                {
                    if (sqlFri.State == ConnectionState.Closed)
                        sqlFri.Open();
                    SqlCommand sqlCmd = new SqlCommand("DostavkaDeletion", sqlFri);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@DostavkaID", DostavkaID);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Deteled successfull");
                    Reset();
                    FillDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (dataGridView1.CurrentRow.Index != -1)
                {
                    ProductID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    button1.Text = "Update";
                    button2.Enabled = true;
                }
            }
            else if (radioButton2.Checked == true)
            {
                if (dataGridView1.CurrentRow.Index != -2)
                {
                    DostavkaID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    button1.Text = "Update";
                    button2.Enabled = true;
                }
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridView();
            label1.Text = "Название продукта:";
            label2.Text = "Группа продукта:";
            label3.Text = "Цена за 500 штук:";
            label4.Text = "Название фирмы:";
            label7.Text = "Привоз товаров:";
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridView();
            label1.Text = "Название продукта:";
            label2.Text = "Группа продукта:";
            label3.Text = "Цена за штуку:";
            label4.Text = "Калорий в продукте:";
            label7.Text = "Добавление товаров:";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string text = textBox5.Text;
            comboBox1.Items.Add(text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dostavkaDataSet.tbl_Firmas' table. You can move, or remove it, as needed.
            this.tbl_FirmasTableAdapter.Fill(this.dostavkaDataSet.tbl_Firmas);
            // TODO: This line of code loads data into the 'productsDataSet.tbl_Products' table. You can move, or remove it, as needed.
            this.tbl_ProductsTableAdapter.Fill(this.productsDataSet.tbl_Products);
        }
    }
}

