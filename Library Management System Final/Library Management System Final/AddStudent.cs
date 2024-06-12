using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Library_Management_System_Final
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
            LoadStudents();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveinfo_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtDepartment.Text != "" && txtSemester.Text != "" && txtContact.Text != "" && txtEmail.Text != "")
            {
                string name = txtName.Text;
                string dep = txtDepartment.Text;
                string sem = txtSemester.Text;
                Int64 mobile = Int64.Parse(txtContact.Text);
                string email = txtEmail.Text;

                string newId = GenerateNewStudentId(); // Generate new student ID

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                cmd.CommandText = "insert into NewStudent(sname, enroll, dep, sem, contact, email) values ('" + name + "', '" + newId + "', '" + dep + "', '" + sem + "', " + mobile + ", '" + email + "')";


                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudents(); // Reload students in the DataGridView
            }
            else
            {
                MessageBox.Show("Please Fill Empty Fields", "Suggest", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                string name = txtName.Text;
                string enroll = txtEnrollment.Text;
                string dep = txtDepartment.Text;
                string sem = txtSemester.Text;
                long mobile = long.Parse(txtContact.Text);
                string email = txtEmail.Text;
                string newId = GenerateNewStudentId();

                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE NewStudent SET sname = @Name, dep = @Dep, sem = @Sem, contact = @Contact, email = @Email WHERE enroll = @Enroll", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Enroll", enroll);
                        cmd.Parameters.AddWithValue("@Dep", dep);
                        cmd.Parameters.AddWithValue("@Sem", sem);
                        cmd.Parameters.AddWithValue("@Contact", mobile);
                        cmd.Parameters.AddWithValue("@Email", email);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student Information Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("No student found with the provided enrollment number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudents()
        {
            dataGridView1.Columns.Clear(); // Clear existing columns

            // Define columns
            dataGridView1.Columns.Add("NameColumn", "Name");
            dataGridView1.Columns.Add("EnrollmentColumn", "Enrollment");
            dataGridView1.Columns.Add("DepartmentColumn", "Department");
            dataGridView1.Columns.Add("SemesterColumn", "Semester");
            dataGridView1.Columns.Add("ContactColumn", "Contact");
            dataGridView1.Columns.Add("EmailColumn", "Email");

            using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM NewStudent", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Add rows
                        dataGridView1.Rows.Add(reader["sname"], reader["enroll"], reader["dep"], reader["sem"], reader["contact"], reader["email"]);
                    }
                }
            }
        }



        private bool ValidateFields()
        {
            return !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtEnrollment.Text) &&
                   !string.IsNullOrEmpty(txtDepartment.Text) &&
                   !string.IsNullOrEmpty(txtSemester.Text) &&
                   !string.IsNullOrEmpty(txtContact.Text) &&
                   !string.IsNullOrEmpty(txtEmail.Text);
        }

        private string GenerateNewStudentId()
        {
            string newId = "C-001";

            using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 enroll FROM NewStudent ORDER BY enroll DESC", con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string lastId = result.ToString();
                        int id = int.Parse(lastId.Substring(2)) + 1;
                        newId = "C-" + id.ToString("D3");
                    }
                }
            }

            return newId;
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEnrollment.Text))
            {
                string enroll = txtEnrollment.Text;

                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM NewStudent WHERE enroll = @Enroll", con))
                    {
                        cmd.Parameters.AddWithValue("@Enroll", enroll);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student Information Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("No student found with the provided enrollment number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter the enrollment number of the student to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the DataGridViewRow corresponding to the clicked cell
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate text boxes with data from the selected row
                txtName.Text = row.Cells["NameColumn"].Value.ToString();
                txtEnrollment.Text = row.Cells["EnrollmentColumn"].Value.ToString();
                txtDepartment.Text = row.Cells["DepartmentColumn"].Value.ToString();
                txtSemester.Text = row.Cells["SemesterColumn"].Value.ToString();
                txtContact.Text = row.Cells["ContactColumn"].Value.ToString();
                txtEmail.Text = row.Cells["EmailColumn"].Value.ToString();
            }
        }
    }

}



