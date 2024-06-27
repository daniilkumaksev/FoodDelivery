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
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        public AdministratorPage()
        {
            InitializeComponent();
            RoleLabel.Content = UserProfileData.UserRole;
            SurnameLabel.Content = UserProfileData.UserSurname;
            NameLabel.Content = UserProfileData.UserName;
            PatronymicLabel.Content = UserProfileData.UserPatronymic;
        }
        private void ExitButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Authorization.xaml", UriKind.Relative));
        }
        private void AdminProductButton(object sender, RoutedEventArgs e)
        {
            FrameCabinet.Content = new AdminProduct();
        }
        private void AdminPersonalButton(object sender, RoutedEventArgs e)
        {
            FrameCabinet.Content = new AdminPersonal();
        }
    }
}
