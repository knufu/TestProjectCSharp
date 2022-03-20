using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для RemoveTest.xaml
    /// </summary>
    public partial class RemoveTest : Window
    {
        SqlConnection sqlConnection;
        public RemoveTest(SqlConnection sqlConnection)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
            Update();
        }

        private void Update()
        {
            string sql = "SELECT Quest_ as 'Вопрос' FROM Tests;";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
            DataTable dt = new DataTable();
            Flex:
            try
            {
                adapter.Fill(dt);
            }
            catch (Exception)
            {
                sqlConnection.Close();
                sqlConnection.Open();
                goto Flex;
            }
            finally
            {
                QuestDataGrid.ItemsSource = dt.DefaultView;
            }
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object item = QuestDataGrid.SelectedItem;
            string ID = (QuestDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;


            string sql = $"delete from Tests where Quest_ like '{ID}'";
            using (SqlCommand command = new SqlCommand(sql, sqlConnection))
            {
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    reader.Read();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                reader.Close();
                Update();
            }


        }
    }
}
