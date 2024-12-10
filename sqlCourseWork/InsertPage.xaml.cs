using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace sqlCourseWork
{
    public partial class InsertPage : Page
    {
        private string connectionString = "Server=ROCKET\\SQLEXPRESS;Database=SocialMedia;Trusted_Connection=True;";

        public InsertPage()
        {
            InitializeComponent();
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string senderId = SenderIdTextBox.Text;
            string receiverId = ReceiverIdTextBox.Text;
            string messageContent = MessageTextBox.Text;

            if (string.IsNullOrWhiteSpace(senderId) || string.IsNullOrWhiteSpace(receiverId) || string.IsNullOrWhiteSpace(messageContent))
            {
                MessageBox.Show("Всі поля мають бути заповнені!");
                return;
            }

            string query = $@"
                INSERT INTO Messages (SenderID, ReceiverID, Content) 
                VALUES (@SenderID, @ReceiverID, @Content)";

            ExecuteNonQuery(query, new SqlParameter("@SenderID", senderId), new SqlParameter("@ReceiverID", receiverId), new SqlParameter("@Content", messageContent));
        }

        private void AddLikeButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = UserIdLikeTextBox.Text;
            string postId = PostIdLikeTextBox.Text;

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(postId))
            {
                MessageBox.Show("User ID та Post ID мають бути заповнені!");
                return;
            }

            string query = $@"
                INSERT INTO Likes (UserID, PostID) 
                VALUES (@UserID, @PostID)";

            ExecuteNonQuery(query, new SqlParameter("@UserID", userId), new SqlParameter("@PostID", postId));
        }

        private void CreatePostButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = UserIdPostTextBox.Text;
            string content = PostContentTextBox.Text;

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("User ID та зміст поста мають бути заповнені!");
                return;
            }

            string query = $@"
                INSERT INTO Posts (UserID, Content) 
                VALUES (@UserID, @Content)";

            ExecuteNonQuery(query, new SqlParameter("@UserID", userId), new SqlParameter("@Content", content));
        }

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = UserIdCommentTextBox.Text;
            string postId = PostIdCommentTextBox.Text;
            string commentContent = CommentContentTextBox.Text;

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(postId) || string.IsNullOrWhiteSpace(commentContent))
            {
                MessageBox.Show("Всі поля мають бути заповнені!");
                return;
            }

            string query = $@"
                INSERT INTO Comments (UserID, PostID, Content) 
                VALUES (@UserID, @PostID, @Content)";

            ExecuteNonQuery(query, new SqlParameter("@UserID", userId), new SqlParameter("@PostID", postId), new SqlParameter("@Content", commentContent));
        }

        private void ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters);
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"Операція успішна. Змінено рядків: {rowsAffected}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при виконанні запиту:\n{ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
