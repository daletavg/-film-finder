using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_adonet
{
    public class FilmsViewModel : INotifyPropertyChanged
    {
        
        public ObservableCollection<FilmCardViewModel> FilmCards { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
