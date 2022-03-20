using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace SuicideTestsPetya_WpF
{
    /// <summary>
    /// Логика взаимодействия для AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        SqlConnection sqlConnection;
        public AddTest(SqlConnection sqlConnection)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((AnswerCount.SelectedIndex + 2) >= (CorrectA.SelectedIndex + 1)) {
                string sql = $"insert into Tests(Quest_,AnswerCount,Answer1,Answer2,Answer3,Answer4,Correct) values ('{QuestText.Text}',{AnswerCount.SelectedIndex + 2},'{A1.Text}','{A2.Text}','{A3.Text}','{A4.Text}',{CorrectA.SelectedIndex + 1});";
                Debug.WriteLine(sql);
                using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        reader.Read();
                        MessageBox.Show("Тест успешно добавлен.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    reader.Close();
                }
            }
            else
            {
                MessageBox.Show("Ошибка выбора правильного ответа");
            }
        }

        private void AnswerCount_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {

                if (AnswerCount.SelectedIndex + 2 == 2)
                {
                    A3.Text = "";
                    A3.IsEnabled = false;
                    A4.Text = "";
                    A4.IsEnabled = false;
                }

                if (AnswerCount.SelectedIndex + 2 == 3)
                {
                    A3.IsEnabled = true;
                    A4.Text = "";
                    A4.IsEnabled = false;
                }

                if (AnswerCount.SelectedIndex + 2 == 4)
                {
                    A3.IsEnabled = true;
                    A4.IsEnabled = true;
                }
            }
        }
    }
}
