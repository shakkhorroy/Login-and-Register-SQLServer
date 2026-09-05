using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Login_and_Register
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        // Get the SQL Server connection string from App.config
        string conString = ConfigurationManager
            .ConnectionStrings["connString"]
            .ConnectionString;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string login = "SELECT * FROM tbl_users " +
                                   "WHERE username = @username " +
                                   "AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(login, con))
                    {
                        // Pass textbox values safely to SQL
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                new frmDashboard().Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Username and Password, Please Try Again",
                                    "Login Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );

                                txtUsername.Text = "";
                                txtPassword.Text = "";
                                txtUsername.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void clickRegister_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}