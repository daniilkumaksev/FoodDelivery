using FoodDelivery.Class;
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
    /// Логика взаимодействия для AdminPersonal.xaml
    /// </summary>
    public partial class AdminPersonal : Page
    {
        private string ID;
        public List<Personal> personal { get; set; }

        public AdminPersonal()
        {
            InitializeComponent();
            AddBut.Visibility = Visibility.Collapsed;
            EditBut.Visibility = Visibility.Collapsed;
            SeparatBorder.Visibility = Visibility.Collapsed;
            LabelPanel.Visibility = Visibility.Collapsed;
            BoxPanel.Visibility = Visibility.Collapsed;
            PersonalList.SetValue(Grid.RowSpanProperty, 3);
            LoadMenuItems();
        }
        private void LoadMenuItems()
        {
            try
            {
                string sql = @"SELECT ID, Surname, Name, Patronymic, Role, Login, Password FROM [User]";

                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    personal = new List<Personal>();
                    while (reader.Read())
                    {
                        personal.Add(new Personal
                        {
                            ID = reader.GetInt32(0),
                            Surname = reader.GetString(1),
                            Name = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            Role = reader.GetString(4),
                            Login = reader.GetString(5),
                            Password = reader.GetString(6)
                        });
                    }
                    reader.Close();
                    connection.Close();
                }
                PersonalList.ItemsSource = personal;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeletePersonal(object sender, EventArgs e)
        {
            Personal selectedItem = (Personal)PersonalList.SelectedItem;
            if (selectedItem != null)
            {
                string sql = @"DELETE FROM [User] WHERE ID = @ID";
                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", selectedItem.ID);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                LoadMenuItems();
                MessageBox.Show("Пользователь удален!");
            }
        }
        private void EditPersonal(object sender, EventArgs e)
        {
            Personal selectedItem = (Personal)PersonalList.SelectedItem;

            if (selectedItem != null)
            {
                SurnameTextBox.Text = selectedItem.Surname;
                NameTextBox.Text = selectedItem.Name;
                PatronymicTextBox.Text = selectedItem.Patronymic;
                RoleTextBox.Text = selectedItem.Role;
                LoginTextBox.Text = selectedItem.Login;
                PasswordTextBox.Password = selectedItem.Password;

                ID = selectedItem.ID.ToString();

                SeparatBorder.Visibility = Visibility.Visible;
                LabelPanel.Visibility = Visibility.Visible;
                BoxPanel.Visibility = Visibility.Visible;
                PersonalList.SetValue(Grid.RowSpanProperty, 2);

                AddBut.Visibility = Visibility.Collapsed;
                EditBut.Visibility = Visibility.Visible;
            }
        }
        private void AddPersonal(object sender, EventArgs e)
        {
            SurnameTextBox.Text = null;
            NameTextBox.Text = null;
            PatronymicTextBox.Text = null;
            RoleTextBox.Text = null;
            LoginTextBox.Text = null;
            PasswordTextBox.Password = null;

            ID = null;

            SeparatBorder.Visibility = Visibility.Visible;
            LabelPanel.Visibility = Visibility.Visible;
            BoxPanel.Visibility = Visibility.Visible;
            PersonalList.SetValue(Grid.RowSpanProperty, 2);

            AddBut.Visibility = Visibility.Visible;
            EditBut.Visibility = Visibility.Collapsed;
        }
        private void SaveButton(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SurnameTextBox.Text) ||
                    string.IsNullOrEmpty(NameTextBox.Text) ||
                    string.IsNullOrEmpty(PatronymicTextBox.Text) ||
                    string.IsNullOrEmpty(RoleTextBox.Text) ||
                    string.IsNullOrEmpty(LoginTextBox.Text) ||
                    string.IsNullOrEmpty(PasswordTextBox.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!");
                    return;
                }
                int id = int.Parse(ID);
                string surname = SurnameTextBox.Text;
                string name = NameTextBox.Text;
                string patronymic = PatronymicTextBox.Text;
                string role = RoleTextBox.Text;
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Password;

                string sql = @"UPDATE [User] SET Surname = @Surname, Name = @Name, Patronymic = @Patronymic, Role = @Role, Login = @Login, Password = @Password WHERE ID = @ID";

                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Patronymic", patronymic);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Данные сотрудника изменены!");
                LoadMenuItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddButton(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SurnameTextBox.Text) ||
                    string.IsNullOrEmpty(NameTextBox.Text) ||
                    string.IsNullOrEmpty(PatronymicTextBox.Text) ||
                    string.IsNullOrEmpty(RoleTextBox.Text) ||
                    string.IsNullOrEmpty(LoginTextBox.Text) ||
                    string.IsNullOrEmpty(PasswordTextBox.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!");
                    return;
                }
                string surname = SurnameTextBox.Text;
                string name = NameTextBox.Text;
                string patronymic = PatronymicTextBox.Text;
                string role = RoleTextBox.Text;
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Password;

                string sql = @"INSERT INTO [User] (Surname, Name, Patronymic, Role, Login, Password) VALUES (@Surname, @Name, @Patronymic, @Role, @Login, @Password)";
                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Patronymic", patronymic);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                LoadMenuItems();
                MessageBox.Show("Сотрудник добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
