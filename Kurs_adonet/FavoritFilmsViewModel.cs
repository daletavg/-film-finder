using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Kurs_adonet.Annotations;

namespace Kurs_adonet
{
    class FavoritFilmsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private ObservableCollection<FilmCardViewModel> _filmsCards;
        public ObservableCollection<FilmCardViewModel> FilmCards
        {
            set { _filmsCards = value; OnPropertyChanged(nameof(FilmCards)); }
            get { return _filmsCards; }
        }

        public void ShowAllFilms()
        {
            LoadingFilms loading = new LoadingFilms();
            FilmCards = new ObservableCollection<FilmCardViewModel>(loading.LoadFavoritFilms());
        }
    }
}