using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using Authorization = FoodDelivery.Pages.Authorization;

namespace FoodDelivery
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Authorization();
        }
        private void LightThemeButton(object sender, EventArgs e)
        {
            Theme("Light.Blue");
        }
        private void DarkThemeButton(object sender, EventArgs e)
        {
            Theme("Dark.Blue");
        }
        private void Theme(string selectedTheme)
        {
            DoubleAnimation opacityAnimation1 = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.1));
            opacityAnimation1.Completed += (s, args) =>
            {
                DoubleAnimation opacityAnimation2 = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.1));
                Application.Current.MainWindow.BeginAnimation(UIElement.OpacityProperty, opacityAnimation2);

                ThemeManager.Current.ChangeTheme(Application.Current, selectedTheme);
                Application.Current?.MainWindow?.Activate();
                App.SaveSelectedTheme(selectedTheme);

            };
            Application.Current.MainWindow.BeginAnimation(UIElement.OpacityProperty, opacityAnimation1);
        }
    }
}
