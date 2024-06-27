using FoodDelivery.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
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
    /// Логика взаимодействия для CourierHistoryOrder.xaml
    /// </summary>
    public partial class CourierHistoryOrder : Page
    {
        public CourierHistoryOrder()
        {
            InitializeComponent();
            LoadOrders();
        }
        private void LoadOrders()
        {
            using (var connection = new SqlConnection(Connect.ConnectionString))
            {
                connection.Open();

                string query = @"
    SELECT 
        o.ID,
o.OrderStatus,
        p.Name, 
        p.Price, 
        cr.Surname, 
        cr.Name,
        cr.Patronymic
    FROM [Order] o
    JOIN [Product] p ON o.ProductID = p.ID
    JOIN [User] cr ON o.ClientID = cr.ID
    WHERE o.OrderStatus = @OrderStatus AND o.CourierID = @CourierID";

                using (var command = new SqlCommand(query, connection))
                {
                    // Добавить параметр для ClientID
                    command.Parameters.Add("@OrderStatus", SqlDbType.VarChar).Value = "Готов";
                    command.Parameters.Add("@CourierID", SqlDbType.Int).Value = UserProfileData.UserID;

                    using (var reader = command.ExecuteReader())
                    {
                        var orders = new List<dynamic>();
                        while (reader.Read())
                        {
                            dynamic order = new ExpandoObject();
                            order.OrderID = reader.GetInt32(0);
                            order.OrderStatus = reader.GetString(1);
                            order.ProductName = reader.GetString(2);
                            order.Price = reader.GetString(3);
                            order.CourierSurname = reader.GetString(4);
                            order.CourierName = reader.GetString(5);
                            order.CourierPatronymic = reader.GetString(6);
                            orders.Add(order);
                        }
                        BasketList.ItemsSource = orders;
                    }
                }
            }
        }
    }
}
