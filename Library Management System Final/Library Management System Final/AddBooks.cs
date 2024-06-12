using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System_Final
{
    public partial class AddBooks : Form
    {

        string connectionString = "Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;Trust Server Certificate=True";

        public AddBooks()
        {
            InitializeComponent();
        }

        private void AddBooks_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBookName.Text != "" && txtBookAuthor.Text != "" && txtBookPrice.Text != "" && txtBookPub.Text != "" && txtBookQuan.Text != "")
            {

                String bname = txtBookName.Text;
                String bauthor = txtBookAuthor.Text;
                String publication = txtBookPub.Text;
                String pdate = dateTimePicker1.Text;
                Int64 price = Int64.Parse(txtBookPrice.Text);
                Int64 quan = Int64.Parse(txtBookQuan.Text);

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;Trust Server Certificate=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                cmd.CommandText = "insert into NewBook(bName,bAuthor,bPubl,bPdate,bPrice,bQuan) values ('" + bname + "' , '" + bauthor + "' , '" + publication + "' , '" + pdate + "' , " + price + " , " + quan + ")";
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtBookName.Clear();
                txtBookAuthor.Clear();
                txtBookPub.Clear();
                txtBookPrice.Clear();
                txtBookQuan.Clear();
            }
            else
            {

                MessageBox.Show("Empty Field Not Allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void ClearForm()
        {
            txtBookName.Clear();
            txtBookAuthor.Clear();
            txtBookPrice.Clear();
            txtBookQuan.Clear();
        }
    }
}

