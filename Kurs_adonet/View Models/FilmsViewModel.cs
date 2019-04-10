using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kurs_adonet
{
    public class FilmsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FilmCardViewModel> _filmsCards;
        public ObservableCollection<FilmCardViewModel> FilmCards
        {
            set { _filmsCards = value; OnPropertyChanged(nameof(FilmCards)); }
            get { return _filmsCards; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowAllFilms()
        {
            LoadingFilms loading = new LoadingFilms();
            FilmCards = new ObservableCollection<FilmCardViewModel>(loading.LoadFilmsAtClient());
        }
        private DelegateCommand _upload;

        public ICommand Upload
        {
            get
            {
                if (_upload == null)
                {
                    _upload = new DelegateCommand(param => ShowAllFilms(), null);
                }

                return _upload;
            }
        }
    }
}
