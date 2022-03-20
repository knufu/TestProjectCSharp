using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SuicideTestsPetya_WpF
{
    /// <summary>
    /// Логика взаимодействия для MainTest.xaml
    /// </summary>
    public partial class MainTest : Window
    {
        SqlConnection sqlConnection;
        public string Login;
        public MainTest(SqlConnection sqlConnection, string Login)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
            this.Login = Login;
            GetTest();
            if (maxcount == 0)
            {
                MessageBox.Show("Тесты не найдены.");
            }
            else
            {
                GetNext();
            }
        }


        SqlDataReader reader;
        int counter = 0;
        int maxcount;
        private void GetTest()
        {
            using (SqlCommand command = new SqlCommand($"SELECT * FROM Tests", sqlConnection))
            {

                try
                {
                    reader = command.ExecuteReader();
                    MaxCountLabel.Content = (maxcount = reader.Cast<object>().Count());
                    reader.Close();
                    reader = command.ExecuteReader();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            
        }

        private void MaxCountIncr()
        {
            CountLabel.Content = (counter = counter + 1).ToString() + "/";
        }

        int correct = 0;
        int points = 0;
        private void GetNext()
        {
            if (counter < maxcount)
            {
                reader.Read();
                MaxCountIncr();
                QuestTextBlock.Text = reader.GetValue(0).ToString();
                Answer1.Text = reader.GetValue(2).ToString();
                Answer2.Text = reader.GetValue(3).ToString();
                correct = Convert.ToInt32(reader.GetValue(6));
                if (Convert.ToInt32(reader.GetValue(1)) == 3)
                {
                    Answer3.Visibility = Visibility.Visible;
                    Answer3.Text = reader.GetValue(4).ToString();
                }
                else
                if (Convert.ToInt32(reader.GetValue(1)) == 4)
                {
                    Answer3.Visibility = Visibility.Visible;
                    Answer3.Text = reader.GetValue(4).ToString();
                    Answer4.Visibility = Visibility.Visible;
                    Answer4.Text = reader.GetValue(5).ToString();
                }
                else
                {
                    Answer3.Visibility = Visibility.Hidden;
                    Answer4.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                reader.Close();
                EndTest();
                this.Close();
                
                
            }
        }

        private void EndTest()
        {
            double Level = points / maxcount * 100;

            string flex = null;
            if(Level < 10)
            {
                flex = "Noob";
            }else
            if(Level > 10 && Level < 50)
            {
                flex = "Medium";
            }else
            if(Level > 50 && Level < 75)
            {
                flex = "Hard";
            }else
            if(Level > 75 && Level < 100)
            {
                flex = "Pro";
            }else
            if(Level == 100)
            {
                flex = "Expert!";
            }

            MessageBox.Show("Тест окончен.\nПравильных ответов: " + points + " из " + maxcount + "\nВаш уровень: " + flex);
            using (SqlCommand command = new SqlCommand($"update Users set points_ = points_ + {points} from Users where login_ like '{Login}'", sqlConnection))
            {
                SqlDataReader freader = command.ExecuteReader();
            }
        }

        private void Answer2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correct == 2)
            {
                points += 1;
                MessageBox.Show("Ответ верный!");
            }
            else
            {
                MessageBox.Show("Ответ неверный :(");
            }
            GetNext();
        }

        private void Answer1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correct == 1)
            {
                points += 1; 
                MessageBox.Show("Ответ верный!");
            }
            else
            {
                MessageBox.Show("Ответ неверный :(");
            }
            GetNext();
        }

        private void Answer3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correct == 3)
            {
                points += 1;
                MessageBox.Show("Ответ верный!");
            }
            else
            {
                MessageBox.Show("Ответ неверный :(");
            }
            GetNext();
        }

        private void Answer4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (correct == 3)
            {
                points += 1;
                MessageBox.Show("Ответ верный!");
            }
            else
            {
                MessageBox.Show("Ответ неверный :(");
            }
            GetNext();
        }
    }
}
