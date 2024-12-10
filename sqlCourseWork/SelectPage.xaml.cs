using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace sqlCourseWork
{
    public partial class SelectPage : Page
    {
        private string connectionString = "Server=ROCKET\\SQLEXPRESS;Database=SocialMedia;Trusted_Connection=True;";


        public SelectPage()
        {
            InitializeComponent();
        }

      

        private void GetLikedPostsButton_Click(object sender, RoutedEventArgs e)
        {
            string userIdInput = UserIdTextBox.Text;

            if (string.IsNullOrWhiteSpace(userIdInput))
            {
                MessageBox.Show("Введіть ID користувача!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "EXEC GetLikedPostsByUser @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userIdInput);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        ResultsDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при виконанні запиту: {ex.Message}", "Помилка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        
        private void GetCommentsButton_Click(object sender, RoutedEventArgs e)
        {
            string postIdInput = PostIdTextBox.Text;

            if (string.IsNullOrWhiteSpace(postIdInput))
            {
                // MessageBox.Show("Введіть ID поста!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "EXEC GetCommentsByPostID @PostID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PostID", postIdInput);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        ResultsDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Помилка при виконанні запиту: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void GetMessagesButton_Click(object sender, RoutedEventArgs e)
        {
            string senderId = SenderIdTextBox.Text;
            string receiverId = ReceiverIdTextBox.Text;

            if (string.IsNullOrWhiteSpace(senderId) || string.IsNullOrWhiteSpace(receiverId))
            {
                MessageBox.Show("Введіть ID відправника та отримувача!");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "GetMessagesBetweenUsers";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID1", senderId);
                        command.Parameters.AddWithValue("@UserID2", receiverId);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        ResultsDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при виконанні запиту: {ex.Message}", "Помилка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        private void ExecuteQuery(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        ResultsDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Помилка при виконанні запиту: {ex.Message}");
            }
        }
    }
}