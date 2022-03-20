using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace SuicideTestsPetya_WpF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public SqlConnection sqlConnection;
        public MainWindow()
        {
            InitializeComponent();
            Connection();
        }

        public void Connection()
        {
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = @"Data Source=DESKTOP-LV8R0GL\SQLEXPRESS;Initial Catalog=PetyaTests;Integrated Security=True";
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        Registration reg;
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reg.Close();
            }
            catch (Exception)
            {

            }
            reg = new Registration(sqlConnection);
            reg.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string Login = LoginBox.Text;
            string Password = PasswordBox1.Password;
            Join(Login, Password);
        }

        private void Join(string Login, string Password)
        {
            using (SqlCommand command = new SqlCommand($"SELECT * FROM Users where login_ like '{Login}';", sqlConnection))
            {
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        Debug.Write(reader.GetValue(4).ToString());
                        if (reader.GetValue(2).ToString() == Password)
                        {
                            if (reader.GetValue(4).ToString() == false.ToString())
                            {
                                new MainTestWindow(sqlConnection,Login).Show();
                                this.Close();
                            }
                            else
                            {
                                new MainTestWindowAdmin(sqlConnection, Login).Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bad Password");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bad Login");
                    }
                    reader.Close();
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}
