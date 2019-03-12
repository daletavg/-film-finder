using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
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


            LoginViewModel loginAndRegistrate = new LoginViewModel();
            RegistrateViewModel registrateViewModel = new RegistrateViewModel();

            RegistrControl registr = new RegistrControl();
            LoginControl loginControl = new LoginControl();
            loginControl.DataContext = loginAndRegistrate;
            registr.DataContext = registrateViewModel;

            main.MyFrame.Navigate(loginControl);
            
            AddFilmViewModel addFilmViewModel = new AddFilmViewModel();
            FilmsViewModel filmsViewModel = new FilmsViewModel();


            //AddLoadFilmClient addNewFilm = new AddLoadFilmClient();
            //int count = addNewFilm.GetFilmsCount();
            //List<FilmCardViewModel> filmCardViewModels = new List<FilmCardViewModel>();
            //for (int i = 0; i < count; i++)
            //{
            //    var film = addNewFilm.GetFilm(i);
            //    FilmCardViewModel card = new FilmCardViewModel(){FilmName = film.Name,Date = film.ReleaseDate,DescriptionFilm = film.Description};
            //    if (film.Image!=null)
            //    {
            //        //BitmapConverter bmBitmapConverter = new BitmapConverter();
            //        //card.PosterFilm = bmBitmapConverter.LoadImage(Encoding.UTF8.GetBytes(film.Image));
            //    }
            //    filmCardViewModels.Add(card);
            //}

            //filmsViewModel.FilmCards = new ObservableCollection<FilmCardViewModel>(filmCardViewModels);

           // main.FilmFinder.AllFilms.NewFilm.DataContext = addFilmViewModel;
            //main.FilmFinder.AllFilms.DataContext = filmsViewModel;
            main.LogControl.DataContext = loginAndRegistrate;
            main.RegControl.DataContext = registrateViewModel;
            main.Show();

        }
    }
}
