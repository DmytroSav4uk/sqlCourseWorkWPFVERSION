using System.Windows;

namespace sqlCourseWork
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SelectPage()); // За замовчуванням відкривається SelectPage
        }

        private void SelectPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SelectPage());
        }

        private void InsertPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new InsertPage());
        }
        
        private void DatabaseInfoPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DatabaseInfoPage());
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UpdateQueriesPage());
        }
    }
}