using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kurs_adonet
{
    public class FilmCardViewModel : INotifyPropertyChanged
    {
        

        public string FilmName { set; get; }
        public string DescriptionFilm { set; get; }
        public string Date { set; get; }
        private ImageSource _posterImage;
        public ImageSource PosterFilm
        {
            set { _posterImage = value; OnPropertyChanged(nameof(PosterFilm)); }
            get { return _posterImage; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
