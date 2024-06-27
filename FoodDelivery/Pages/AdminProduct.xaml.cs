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
using System.Xml.Linq;
using ListBox = System.Windows.Controls.ListBox;

namespace FoodDelivery.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminProduct.xaml
    /// </summary>
    public partial class AdminProduct : Page
    {
        private string ID;
        public List<Product> product { get; set; }

        public AdminProduct()
        {
            InitializeComponent();
            AddBut.Visibility = Visibility.Collapsed;
            SaveBut.Visibility = Visibility.Collapsed;
            SeparatBorder.Visibility = Visibility.Collapsed;
            LabelPanel.Visibility = Visibility.Collapsed;
            BoxPanel.Visibility = Visibility.Collapsed;
            ProductList.SetValue(Grid.RowSpanProperty, 3);

            LoadMenuItems();
        }
        private void LoadMenuItems()
        {
            string sql = @"SELECT ID, Category, Name, Price, Quantity FROM [Product]";

            using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                product = new List<Product>();
                while (reader.Read())
                {
                    product.Add(new Product
                    {
                        ID = reader.GetInt32(0),
                        Category = reader.GetString(1),
                        Name = reader.GetString(2),
                        Price = reader.GetString(3),
                        Quantity = reader.GetString(4)
                    });
                }
                reader.Close();
                connection.Close();
            }
            ProductList.ItemsSource = product;
        }
        private void DeleteProduct(object sender, EventArgs e)
        {
            Product selectedItem = (Product)ProductList.SelectedItem;
            if (selectedItem != null)
            {
                string sql = @"DELETE FROM [Product] WHERE ID = @ID";
                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", selectedItem.ID);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Продукт удален!");
                LoadMenuItems();
            }
        }
        private void EditProduct(object sender, EventArgs e)
        {
            Product selectedItem = (Product)ProductList.SelectedItem;

            if (selectedItem != null)
            {
                CategoryTextBox.Text = selectedItem.Category;
                NameTextBox.Text = selectedItem.Name;
                PriceTextBox.Text = selectedItem.Price;
                QuantityTextBox.Text = selectedItem.Quantity;

                ID = selectedItem.ID.ToString();

                SeparatBorder.Visibility = Visibility.Visible;
                LabelPanel.Visibility = Visibility.Visible;
                BoxPanel.Visibility = Visibility.Visible;
                ProductList.SetValue(Grid.RowSpanProperty, 2);

                AddBut.Visibility = Visibility.Collapsed;
                SaveBut.Visibility = Visibility.Visible;
            }
        }
        private void AddProduct(object sender, EventArgs e)
        {
            CategoryTextBox.Text = null;
            NameTextBox.Text = null;
            PriceTextBox.Text = null;
            QuantityTextBox.Text = null;

            ID = null;

            SeparatBorder.Visibility = Visibility.Visible;
            LabelPanel.Visibility = Visibility.Visible;
            BoxPanel.Visibility = Visibility.Visible;
            ProductList.SetValue(Grid.RowSpanProperty, 2);

            AddBut.Visibility = Visibility.Visible;
            SaveBut.Visibility = Visibility.Collapsed;
        }
        private void SaveButton(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CategoryTextBox.Text) ||
                    string.IsNullOrEmpty(NameTextBox.Text) ||
                    string.IsNullOrEmpty(PriceTextBox.Text) ||
                    string.IsNullOrEmpty(QuantityTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!");
                    return;
                }
                int id = int.Parse(ID);
                string category = CategoryTextBox.Text;
                string name = NameTextBox.Text;
                string price = PriceTextBox.Text;
                string quantity = QuantityTextBox.Text;

                string sql = @"UPDATE [Product] SET Category = @Category, Name = @Name, Price = @Price, Quantity = @Quantity WHERE ID = @ID";

                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Продукт изменен!");

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
                if (string.IsNullOrEmpty(CategoryTextBox.Text) ||
                    string.IsNullOrEmpty(NameTextBox.Text) ||
                    string.IsNullOrEmpty(PriceTextBox.Text) ||
                    string.IsNullOrEmpty(QuantityTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!");
                    return;
                }
                string category = CategoryTextBox.Text;
                string name = NameTextBox.Text;
                string price = PriceTextBox.Text;
                string quantity = QuantityTextBox.Text;

                string sql = @"INSERT INTO [Product] (Category, Name, Price, Quantity) VALUES (@Category, @Name, @Price, @Quantity)";
                using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                LoadMenuItems();
                MessageBox.Show("Продукт добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
