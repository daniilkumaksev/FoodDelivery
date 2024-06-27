using FoodDelivery.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для ClientAddOrder.xaml
    /// </summary>
    public partial class ClientAddOrder : Page
    {
        public List<Product> product { get; set; }
        public List<Basket> basket { get; set; }
        public ClientAddOrder()
        {
            InitializeComponent(); 
            product = new List<Product>();
            basket = new List<Basket>();
            CreateButton.Visibility = Visibility.Collapsed;
            LoadCategories();
        }
        private void LoadCategories()
        {
            using (var connection = new SqlConnection(Connect.ConnectionString))
            {                connection.Open();
                string query = @"SELECT DISTINCT Category FROM [Product]";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ComboBoxCategories.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }
        private void SelectCategory(object sender, SelectionChangedEventArgs e)
        {
           var select =  (dynamic)ComboBoxCategories.SelectedItem;
            if(select != null)
            {
                LoadMenuItems(select);
            }
        }
        private void LoadMenuItems(string category)
        {
            string sql = $@"SELECT ID, Category, Name, Price, Quantity FROM [Product] WHERE Category = '{category}'";

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
        private void AddProduct(object sender, EventArgs e)
        {
            Product selectedItem = (Product)ProductList.SelectedItem;

            if (selectedItem != null)
            {
                basket.Add(new Basket
                {
                    ID = selectedItem.ID,
                    Category = selectedItem.Category,
                    Name = selectedItem.Name,
                    Price = selectedItem.Price,
                });
                BasketList.Items.Add(selectedItem);
                CreateButton.Visibility = Visibility.Visible;
            }
        }
        private void DeleteProduct(object sender, EventArgs e)
        {
            Product selectedItem = (Product)BasketList.SelectedItem;

            if (selectedItem != null)
            {
                basket.Remove(new Basket
                {
                    ID = selectedItem.ID,
                    Category = selectedItem.Category,
                    Name = selectedItem.Name,
                    Price = selectedItem.Price,
                    Quantity = selectedItem.Quantity
                });
                BasketList.Items.Remove(selectedItem);
            }
        }
        private void CreateNewOrderButton(object sender, EventArgs e)
        {
            List<int> productIDs = new List<int>(); 

            using (SqlConnection connection = new SqlConnection(Connect.ConnectionString))
            {
                connection.Open();

                foreach (var item in basket)
                {
                    string query = "INSERT INTO [Order] (ProductID, ClientID, OrderStatus) " +
                                   "VALUES (@ProductID, @ClientID, @OrderStatus)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", item.ID);
                    command.Parameters.AddWithValue("@ClientID", UserProfileData.UserID);
                    command.Parameters.AddWithValue("@OrderStatus", "Новый");
                    command.ExecuteNonQuery();

                    productIDs.Add(item.ID);
                }

                basket.Clear(); 

                MessageBox.Show("Заказ создан!");
            }
        }
    }
}
