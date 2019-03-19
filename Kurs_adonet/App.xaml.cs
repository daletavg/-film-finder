using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
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
            
            LoginViewModel loginAndRegistrate = new LoginViewModel(mainWindowViewModel.OpenRegistrateControl,mainWindowViewModel.OpenFilmFinderControl);
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
                    byte[] myByte = film.Image;
                    using (MemoryStream ms = new MemoryStream(myByte))
                    {
                        var bmp = Bitmap.FromStream(ms);



                        
                        ms.Seek(0, System.IO.SeekOrigin.Begin);

                        var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        card.PosterFilm = bitmap;
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
