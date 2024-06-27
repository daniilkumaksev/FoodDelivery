using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void RegButton(object sender, EventArgs e)                               // Метод авторизации по нажатию кнопки "Войти"
        {
            string surname = SurnameTextBox.Text; 
            string name = NameTextBox.Text;
            string patronymic = PatronymicTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PassBox.Password;
            if (surname.Length < 1 && name.Length < 1 && patronymic.Length < 1 && login.Length < 1 && password.Length < 1)
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                RegistrationUser(surname,  name,  patronymic, login, password);
            }
        }
        private void RegistrationUser(string surname, string name, string patronymic, string login, string password)
        {
            try
            {
                string sql = @"INSERT INTO [User] (Surname, Name, Patronymic, Login, Password, Role) VALUES (@Surname, @Name, @Patronymic, @Login, @Password, @Role)";
                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Patronymic", patronymic);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Role", "Клиент");

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                MessageBox.Show("Вы зарегистрированы!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BackButton(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Authorization.xaml", UriKind.Relative));
        }
    }
}
