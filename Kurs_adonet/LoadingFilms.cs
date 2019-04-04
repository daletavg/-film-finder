using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Kurs_adonet
{
    public class LoadingFilms
    {
        public static OperationContracts.IFilmFinderServer AddLoadFilm;
        
        public List<FilmCardViewModel> LoadFilmsAtClient()
        {
            int count = AddLoadFilm.GetFilmsCount();
            List<FilmCardViewModel> filmCardViewModels = new List<FilmCardViewModel>();
            for (int i = 0; i < count; i++)
            {
                var film = AddLoadFilm.GetFilm(i);
                
                FilmCardViewModel card = new FilmCardViewModel(AddLoadFilm) { FilmName = film.Name, Date = film.ReleaseDate, DescriptionFilm = film.Description };
                card.ListActor = new ObservableCollection<string>(film.Actors);
                card.ListProdusser = new ObservableCollection<string>(film.Produsers);
                card.ListGenre = new ObservableCollection<string>(film.Geners);
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
            return filmCardViewModels;
        }
     
    
    }
}
