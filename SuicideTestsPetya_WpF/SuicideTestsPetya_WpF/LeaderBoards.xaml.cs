using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для LeaderBoards.xaml
    /// </summary>
    public partial class LeaderBoards : Window
    {
        SqlConnection sqlConnection;
        public LeaderBoards(SqlConnection sqlConnection)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
            Update(sqlConnection);
        }

        private void Update(SqlConnection cn)
        {
            string sql = "SELECT TOP 10 login_ as 'User',points_ as 'Points' FROM Users where points_ > 0 ORDER BY points_ desc";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            Flex:
            try
            {
               
                adapter.Fill(dt);
            }
            catch (Exception)
            {
                cn.Close();
                cn.Open();
                goto Flex;
            }
            finally
            {
                LeaderBoardGrid.ItemsSource = dt.DefaultView;
            }
        }
    }
}
