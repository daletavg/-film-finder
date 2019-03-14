﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Kurs_adonet.FilmsFinder;


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

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            
            LoginViewModel loginAndRegistrate = new LoginViewModel(mainWindowViewModel.OpenRegistrateControl);
            RegistrateViewModel registrateViewModel = new RegistrateViewModel(mainWindowViewModel.OpenLoginControl);

            RegistrControl registrControl = new RegistrControl();
            LoginControl loginControl = new LoginControl();
            FilmFinder filmFinderControl = new FilmFinder();

            loginControl.DataContext = loginAndRegistrate;
            registrControl.DataContext = registrateViewModel;

            //main.MyFrame.Navigate(loginControl);
            
            AddFilmViewModel addFilmViewModel = new AddFilmViewModel();
            FilmsViewModel filmsViewModel = new FilmsViewModel();


            AddLoadFilmClient addNewFilm = new AddLoadFilmClient();
            int count = addNewFilm.GetFilmsCount();
            List<FilmCardViewModel> filmCardViewModels = new List<FilmCardViewModel>();
            for (int i = 0; i < count; i++)
            {
                var film = addNewFilm.GetFilm(i);
                FilmCardViewModel card = new FilmCardViewModel() { FilmName = film.Name, Date = film.ReleaseDate, DescriptionFilm = film.Description };
                if (film.Image != null)
                {
                    byte[] myByte = Encoding.Default.GetBytes(film.Image);
                    using (MemoryStream ms = new MemoryStream(myByte, 0, myByte.Length))
                    {
                        ms.Write(myByte, 0, myByte.Length);
                        var imageSource = new BitmapImage();
                        imageSource.BeginInit();
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.StreamSource = ms;
                        imageSource.EndInit();
                        card.PosterFilm = imageSource;
                    }
                }
                filmCardViewModels.Add(card);
            }

            filmsViewModel.FilmCards = new ObservableCollection<FilmCardViewModel>(filmCardViewModels);

            filmFinderControl.AllFilms.NewFilm.DataContext = addFilmViewModel;
            filmFinderControl.AllFilms.DataContext = filmsViewModel;
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
