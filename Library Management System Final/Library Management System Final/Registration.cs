using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Library_Management_System_Final
{
    public partial class Registration : Form
    {
        string connectionString = "Data Source=LAPTOP-SEOOO4JR\\SQLEXPRESS;Initial Catalog=Librar;Integrated Security=True;";



        public Registration()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = EncryptPassword(txtPassword.Text);

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Check if the username already exists in the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Username", username);
                    int existingUsersCount = (int)checkCommand.ExecuteScalar();

                    if (existingUsersCount > 0)
                    {
                        MessageBox.Show("User with the same username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method if the user already exists
                    }

                    // If the user does not exist, proceed with registration
                    string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@Password", password);
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User registered successfully.");
                        this.Close();
                        Form1 loginForm = new Form1();
                        loginForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to register user.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

            private void button2_Click(object sender, EventArgs e)
        {

        }

        private string EncryptPassword(string password)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                return Convert.ToBase64String(data);
            }
        }

        private string DecryptPassword(string encryptedPassword)
        {
            // Decrypting passwords typically involves using reversible encryption algorithms,
            // but hashing algorithms like MD5 used here are not suitable for decryption.
            // Decrypting a hashed password is not a common practice.
            throw new NotImplementedException("Decrypting hashed passwords is not supported.");
        }

        private bool VerifyPassword(string userInputPassword, string storedPassword)
        {
            string hashedUserInputPassword = EncryptPassword(userInputPassword);
            return hashedUserInputPassword == storedPassword;
        }
    }
}


