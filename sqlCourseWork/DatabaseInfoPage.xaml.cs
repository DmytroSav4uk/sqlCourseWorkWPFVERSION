using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace sqlCourseWork
{
    public partial class DatabaseInfoPage : Page
    {
        private string connectionString = "Server=ROCKET\\SQLEXPRESS;Database=SocialMedia;Trusted_Connection=True;";

        public DatabaseInfoPage()
        {
            InitializeComponent();
        }

        private void ShowTablesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT name FROM sys.tables ORDER BY name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        TablesListBox.Items.Clear();
                        while (reader.Read())
                        {
                            TablesListBox.Items.Add(reader["name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void TablesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TablesListBox.SelectedItem == null) return;

            string selectedTable = TablesListBox.SelectedItem.ToString();
            string query = $"SELECT * FROM {selectedTable}";

            ExecuteQuery(query);
        }

        private void ShowProceduresButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT name, create_date, modify_date FROM sys.procedures ORDER BY name";
            ExecuteQuery(query);
        }

        private void ShowTriggersButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT name, parent_id, create_date FROM sys.triggers ORDER BY name";
            ExecuteQuery(query);
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
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }


    }
}