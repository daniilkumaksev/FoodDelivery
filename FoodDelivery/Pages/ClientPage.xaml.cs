using FoodDelivery.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodDelivery.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            RoleLabel.Content = UserProfileData.UserRole;
            SurnameLabel.Content = UserProfileData.UserSurname;
            NameLabel.Content = UserProfileData.UserName;
            PatronymicLabel.Content = UserProfileData.UserPatronymic;
        }
        private void AddOrderButton(object sender, RoutedEventArgs e)
        {
            CabinetFrame.Content = new ClientAddOrder();
        }
        private void HistoryOrderButton(object sender, RoutedEventArgs e)
        {
            CabinetFrame.Content = new ClientHistoryOrder();
        }
        private void DeleteAccountButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить аккаунт?", "Подтверждение удаления аккаунта", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                string sql = @"DELETE FROM [User] WHERE ID = @ID";
                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", UserProfileData.UserID);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Аккаунт удален!");

                NavigationService.Navigate(new Uri("Pages/Authorization.xaml", UriKind.Relative));
            }
        }
        private void ExitButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Authorization.xaml", UriKind.Relative));
        }
    }
}
