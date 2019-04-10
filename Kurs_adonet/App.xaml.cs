using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using OperationContracts;



namespace Kurs_adonet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {



            IFilmFinderServer serverFilmFinder = new FilmFinderServer();
            LoadingFilms.AddLoadFilm = serverFilmFinder;

            MainWindow main = new MainWindow();

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            LoginViewModel loginAndRegistrate = new LoginViewModel(mainWindowViewModel.OpenRegistrateControl,mainWindowViewModel.OpenFilmFinderControl,serverFilmFinder);
            RegistrateViewModel registrateViewModel = new RegistrateViewModel(mainWindowViewModel.OpenLoginControl,serverFilmFinder);
            FilmFinderViewModel filmFinderViewModel = new FilmFinderViewModel(serverFilmFinder);
            SettingViewModel settingViewModel = new SettingViewModel(serverFilmFinder);

            RegistrControl registrControl = new RegistrControl();
            LoginControl loginControl = new LoginControl();
            FilmFinder filmFinderControl = new FilmFinder();

            loginAndRegistrate.LoadUser += filmFinderViewModel.LoadUser;
            settingViewModel.LoadUser += filmFinderViewModel.LoadUser;
            filmFinderControl.SettingControl.DataContext = settingViewModel;
            loginControl.DataContext = loginAndRegistrate;
            registrControl.DataContext = registrateViewModel;
            filmFinderControl.DataContext = filmFinderViewModel;
            
            
            AddFilmViewModel addFilmViewModel = new AddFilmViewModel(serverFilmFinder);
            FilmsViewModel filmsViewModel = new FilmsViewModel();

            filmFinderControl.AllFilms.NewFilm.DataContext = addFilmViewModel;
            filmFinderControl.AllFilms.DataContext = filmsViewModel;
            //filmFinderControl.ChatControl.
            ChatViewModel  chatViewModel = new ChatViewModel(serverFilmFinder);
            filmFinderControl.ChatControl.DataContext = chatViewModel;
            loginControl.DataContext = loginAndRegistrate;
            registrControl.DataContext = registrateViewModel;

            List<IUsingControl> usersControl = new List<IUsingControl>();
            usersControl.Add(registrControl);
            usersControl.Add(loginControl);
            usersControl.Add(filmFinderControl);


            mainWindowViewModel.AddControls(usersControl);
            
            main.DataContext = mainWindowViewModel;
            main.Show();

        }
    }
}
