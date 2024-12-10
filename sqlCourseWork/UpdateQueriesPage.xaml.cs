using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace sqlCourseWork
{
    public partial class UpdateQueriesPage : Page
    {
        private string connectionString = "Server=ROCKET\\SQLEXPRESS;Database=SocialMedia;Trusted_Connection=True;";

        public UpdateQueriesPage()
        {
            InitializeComponent();
        }

   
        private void UpdateMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string messageId = MessageIdTextBox.Text;
            string newContent = NewMessageContentTextBox.Text;

            if (string.IsNullOrWhiteSpace(messageId) || string.IsNullOrWhiteSpace(newContent))
            {
                MessageBox.Show("Будь ласка, введіть ID повідомлення та новий контент.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
            
                DataTable messageData = GetData($"SELECT * FROM Messages WHERE MessageID = @MessageID", new SqlParameter("@MessageID", messageId));
                if (messageData.Rows.Count == 0)
                {
                    MessageBox.Show("Повідомлення не знайдено!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

           
                OldResultsDataGrid.ItemsSource = messageData.DefaultView;

             
                ExecuteProcedure("EXEC UpdateMessageContent @MessageID, @NewContent", new SqlParameter("@MessageID", messageId), new SqlParameter("@NewContent", newContent));

          
                DataTable updatedMessageData = GetData("SELECT * FROM Messages WHERE MessageID = @MessageID", new SqlParameter("@MessageID", messageId));
                UpdatedResultsDataGrid.ItemsSource = updatedMessageData.DefaultView;

                MessageBox.Show("Повідомлення оновлено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

  
        private void UpdateCommentButton_Click(object sender, RoutedEventArgs e)
        {
            string commentId = CommentIdTextBox.Text;
            string newContent = NewCommentContentTextBox.Text;

            if (string.IsNullOrWhiteSpace(commentId) || string.IsNullOrWhiteSpace(newContent))
            {
                MessageBox.Show("Будь ласка, введіть ID коментаря та новий контент.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
              
                DataTable commentData = GetData($"SELECT * FROM Comments WHERE CommentID = @CommentID", new SqlParameter("@CommentID", commentId));
                if (commentData.Rows.Count == 0)
                {
                    MessageBox.Show("Коментар не знайдений!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                OldResultsDataGrid.ItemsSource = commentData.DefaultView;

                
                ExecuteProcedure("EXEC UpdateCommentContent @CommentID, @NewContent", new SqlParameter("@CommentID", commentId), new SqlParameter("@NewContent", newContent));

                
                DataTable updatedCommentData = GetData("SELECT * FROM Comments WHERE CommentID = @CommentID", new SqlParameter("@CommentID", commentId));
                UpdatedResultsDataGrid.ItemsSource = updatedCommentData.DefaultView;

                MessageBox.Show("Коментар оновлено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void UpdatePostButton_Click(object sender, RoutedEventArgs e)
        {
            string postId = PostIdTextBox.Text;
            string newContent = NewPostContentTextBox.Text;

            if (string.IsNullOrWhiteSpace(postId) || string.IsNullOrWhiteSpace(newContent))
            {
                MessageBox.Show("Будь ласка, введіть ID поста та новий контент.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
               
                DataTable postData = GetData($"SELECT * FROM Posts WHERE PostID = @PostID", new SqlParameter("@PostID", postId));
                if (postData.Rows.Count == 0)
                {
                    MessageBox.Show("Пост не знайдений!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

              
                OldResultsDataGrid.ItemsSource = postData.DefaultView;

              
                ExecuteProcedure("EXEC UpdatePostContent @PostID, @NewContent", new SqlParameter("@PostID", postId), new SqlParameter("@NewContent", newContent));

            
                DataTable updatedPostData = GetData("SELECT * FROM Posts WHERE PostID = @PostID", new SqlParameter("@PostID", postId));
                UpdatedResultsDataGrid.ItemsSource = updatedPostData.DefaultView;

                MessageBox.Show("Пост оновлено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      
        private DataTable GetData(string query, SqlParameter parameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.Add(parameter);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

      
        private void ExecuteProcedure(string query, SqlParameter param1, SqlParameter param2)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(param1);
                    command.Parameters.Add(param2);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
