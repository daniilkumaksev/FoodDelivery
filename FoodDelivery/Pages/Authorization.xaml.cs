using ControlzEx.Theming;
using FoodDelivery.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodDelivery.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public static string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=DeliveryFoodDB;Trusted_Connection=True";
        public Authorization()
        {
            InitializeComponent();
        }
        private void LoginButton(object sender, EventArgs e)                               // Метод авторизации по нажатию кнопки "Войти"
        {
            string login = LogBox.Text;
            string password = PassBox.Password;
            if(login.Length < 1 && password.Length < 1)
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                AuthorizationUser(login, password);
            }
        }
        private async void AuthorizationUser(string login, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand($"SELECT ID, Login, Password, Role, Surname, Name, Patronymic FROM [User] WHERE Login = @Login AND Password = @Password", connection);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            int userID = reader.GetInt32(0);
                            string userLogin = reader.GetString(1);
                            string userPassword = reader.GetString(2);
                            string userRole = reader.GetString(3);
                            string userSurname = reader.GetString(4);
                            string userName = reader.GetString(5);
                            string userPatronymic = reader.GetString(6);
                            if (userRole == "Администратор")
                            {

                                UserProfileData.UserID = userID;
                                UserProfileData.UserRole = userRole;
                                UserProfileData.UserSurname = userSurname;
                                UserProfileData.UserName = userName;
                                UserProfileData.UserPatronymic = userPatronymic;
                                UserProfileData.UserLogin = userLogin;
                                UserProfileData.UserPassword = userPassword;

                                NavigationService.Navigate(new Uri("Pages/AdministratorPage.xaml", UriKind.Relative));
                            }
                            else if (userRole == "Курьер")
                            {
                                UserProfileData.UserID = userID;
                                UserProfileData.UserRole = userRole;
                                UserProfileData.UserSurname = userSurname;
                                UserProfileData.UserName = userName;
                                UserProfileData.UserPatronymic = userPatronymic;
                                UserProfileData.UserLogin = userLogin;
                                UserProfileData.UserPassword = userPassword;

                                NavigationService.Navigate(new Uri("Pages/CourierPage.xaml", UriKind.Relative));
                            }
                            else if (userRole == "Клиент")
                            {
                                UserProfileData.UserID = userID;
                                UserProfileData.UserRole = userRole;
                                UserProfileData.UserSurname = userSurname;
                                UserProfileData.UserName = userName;
                                UserProfileData.UserPatronymic = userPatronymic;
                                UserProfileData.UserLogin = userLogin;
                                UserProfileData.UserPassword = userPassword;

                                NavigationService.Navigate(new Uri("Pages/ClientPage.xaml", UriKind.Relative));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверные данные!");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RegButton(object sender, EventArgs e)                               // Метод авторизации по нажатию кнопки "Войти"
        {
            NavigationService.Navigate(new Uri("Pages/Registration.xaml", UriKind.Relative));
        }
    }
    public static class Connect
    {
        public static string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=DeliveryFoodDB;Trusted_Connection=True";

    }
}
