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
            LoginViewModel loginAndRegistrate = new LoginViewModel();
            RegistrateViewModel registrateViewModel = new RegistrateViewModel();
            AddFilmViewModel addFilmViewModel = new AddFilmViewModel();

            main.FilmFinder.AllFilms.NewFilm.DataContext = addFilmViewModel;
            main.LogControl.DataContext = loginAndRegistrate;
            main.RegControl.DataContext = registrateViewModel;
            main.Show();

        }
    }
}
