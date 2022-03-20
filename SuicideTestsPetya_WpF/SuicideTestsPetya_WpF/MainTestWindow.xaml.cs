using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для MainTestWindow.xaml
    /// </summary>
    public partial class MainTestWindow : Window
    {
        SqlConnection sqlConnection;
        public string Login;
        public MainTestWindow(SqlConnection sqlConnection, string Login)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
            this.Login = Login;
        }
        LeaderBoards leaderBoards;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                leaderBoards.Close();
            }
            catch (Exception)
            {

            }
            leaderBoards = new LeaderBoards(sqlConnection);
            leaderBoards.Show();
        }
        MainTest mt;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                mt.Close();
            }
            catch (Exception)
            {

            }
            mt = new MainTest(sqlConnection,Login);
            mt.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Spravka spravka = new Spravka();
            spravka.Show();
        }
    }
}
