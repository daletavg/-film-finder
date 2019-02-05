using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kurs_adonet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow main = new MainWindow();
            LoginAndRegistrateViewModel loginAndRegistrate = new LoginAndRegistrateViewModel();
            main.DataContext = loginAndRegistrate;
            main.Show();
        }
    }
}
