using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuicideTestsPetya_WpF
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    /// 
    
    public partial class Registration : Window
    {
        SqlConnection sqlConnection;
        public Registration(SqlConnection sqlConnection)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
        }

        private void Registration1_Click(object sender, RoutedEventArgs e)
        {
            string Name = NameBox.Text;
            string Login = LoginBox.Text;
            string Password = PasswordBox.Password;
            Register(Name,Login,Password);
            
        }

        private void Register(string Name, string Login, string Password)
        {
            using (SqlCommand command = new SqlCommand($"insert into Users (name_,login_,password_) values('{Name}','{Login}','{Password}');", sqlConnection))
            {
                try
                {
                    command.ExecuteReader();
                    MessageBox.Show("Регистрация прошла успешно");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ошибка: "+ ex.Message );
                }
                
            }
        }
    }
}
