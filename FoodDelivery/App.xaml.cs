using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FoodDelivery
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {public static void SaveSelectedTheme(string themeName)
        {
            try
            {
                // Получаем текущую конфигурацию
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Проверяем, существует ли уже такой ключ
                if (config.AppSettings.Settings["Theme"] != null)
                {
                    // Если ключ существует, обновляем его значение
                    config.AppSettings.Settings["Theme"].Value = themeName;
                }
                else
                {
                    // Если ключ не существует, добавляем его
                    config.AppSettings.Settings.Add("Theme", themeName);
                }

                // Сохраняем изменения в конфигурации
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                // Обработка ошибок при сохранении конфигурации
                Console.WriteLine($"Ошибка при сохранении темы: {ex.Message}");
            }
        }
        protected override void OnStartup(StartupEventArgs e) //  Метод вызывается при запуске приложения и устанавливает язык приложения в соответствии с настройками
        {
            base.OnStartup(e);
            string savedTheme = ConfigurationManager.AppSettings["Theme"];
            if (!string.IsNullOrEmpty(savedTheme))
            {
                ThemeManager.Current.ChangeTheme(Application.Current, savedTheme);
            }
        }
    }
}
