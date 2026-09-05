using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Login_and_Register
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        // Get connection string from App.config
        string conString = ConfigurationManager
            .ConnectionStrings["connString"]
            .ConnectionString;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Check if any field is empty
            if (txtUsername.Text == "" ||
                txtPassword.Text == "" ||
                txtConPassword.Text == "")
            {
                MessageBox.Show(
                    "Username and Password fields cannot be empty",
                    "Registration Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            // Check whether passwords match
            if (txtPassword.Text != txtConPassword.Text)
            {
                MessageBox.Show(
                    "Password does not match. Please re-enter.",
                    "Registration Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();

                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string register =
                        "INSERT INTO tbl_users (username, password) " +
                        "VALUES (@username, @password)";

                    using (SqlCommand cmd = new SqlCommand(register, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@username",
                            txtUsername.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@password",
                            txtPassword.Text
                        );

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Clear fields after successful registration
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtUsername.Focus();

                MessageBox.Show(
                    "Your Account has been Successfully Created",
                    "Registration Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (SqlException ex)
            {
                // Username already exists
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show(
                        "Username already exists. Please choose another username.",
                        "Registration Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Database Error: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtUsername.Focus();
        }

        private void clickLogin_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
            Application.Exit();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
