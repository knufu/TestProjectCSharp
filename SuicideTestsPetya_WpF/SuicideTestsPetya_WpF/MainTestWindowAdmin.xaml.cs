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
    /// Логика взаимодействия для MainTestWindowAdmin.xaml
    /// </summary>
    public partial class MainTestWindowAdmin : Window
    {
        string Login;
        SqlConnection sqlConnection;
        MainTest mt;
        LeaderBoards leaderBoards;
        public MainTestWindowAdmin(SqlConnection sqlConnection, string Login)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
            this.Login = Login;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                mt.Close();
            }
            catch (Exception)
            {

            }
            mt = new MainTest(sqlConnection, Login);
            mt.Show();
        }
       

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

        AddTest Flex;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Flex.Close();
            }
            catch (Exception)
            {

            }
            Flex = new AddTest(sqlConnection);
            Flex.Show();
        }

        RemoveTest RFlex;
        private void LeaderBoardBtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RFlex.Close();
            }
            catch (Exception)
            {

            }
            RFlex = new RemoveTest(sqlConnection);
            RFlex.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Spravka spravka = new Spravka();
            spravka.Show();
        }
    }
}
