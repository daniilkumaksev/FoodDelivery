using FoodDelivery.Class;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CourierPage.xaml
    /// </summary>
    public partial class CourierPage : Page
    {
        public CourierPage()
        {
            InitializeComponent();
            RoleLabel.Content = UserProfileData.UserRole;
            SurnameLabel.Content = UserProfileData.UserSurname;
            NameLabel.Content = UserProfileData.UserName;
            PatronymicLabel.Content = UserProfileData.UserPatronymic;
        }
        private void OrderClientButton(object sender, RoutedEventArgs e)
        {
            CabinetFrame.Content = new CourierOrderClient();
        }
        private void ActiveOrderButton(object sender, RoutedEventArgs e)
        {
            CabinetFrame.Content = new CourierActiveOrder();
        }
        private void HistoryOrderButton(object sender, RoutedEventArgs e)
        {
            CabinetFrame.Content = new CourierHistoryOrder();
        }
        private void ExitButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Authorization.xaml", UriKind.Relative));
        }
    }
}
