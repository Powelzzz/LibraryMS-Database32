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
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
            }

            private void pictureBox2_Click(object sender, EventArgs e)
            {

            }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {

            }

            private void label1_Click(object sender, EventArgs e)
            {

            }

            private void Form1_Load(object sender, EventArgs e)
            {
            // Hide the password by displaying asterisks
            txtPassword.UseSystemPasswordChar = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

            {

            }
        }

        string connectionString = "Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;";

        private void button1_Click(object sender, EventArgs e)
            {
            string username = txtUsername.Text;
            string password = txtPassword.Text; // Get the password entered by the user

            // Query the database to retrieve the hashed password for the entered username
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Password FROM Users WHERE Username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string storedPassword = result.ToString();

                        // Verify the entered password against the stored hashed password
                        if (VerifyPassword(password, storedPassword))
                        {
                            // Passwords match, login successful
                            this.Hide();
                            Dashboard dsa = new Dashboard();
                            dsa.Show();
                        }
                        else
                        {
                            // Display error message if passwords don't match
                            MessageBox.Show("Wrong Username and/or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Display error message if username not found
                        MessageBox.Show("Wrong Username and/or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Display any exception that occurs during database access
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private bool VerifyPassword(string userInputPassword, string storedPassword)
        {
            // No need to hash the user input password here, as it's already hashed
            return userInputPassword == storedPassword;
        }
    


            private void button2_Click(object sender, EventArgs e)
            {
                Registration registration = new Registration();
                registration.Show();
            }

            private void button3_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
            // Toggle the UseSystemPasswordChar property based on the checkbox state
            txtPassword.UseSystemPasswordChar = !checkBox1.Checked;
        }
        }
    }
