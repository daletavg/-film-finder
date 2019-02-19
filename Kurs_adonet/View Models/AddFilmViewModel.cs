using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Kurs_adonet
{
    class AddFilmViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FilmName { set; get; } = "Name";
        public string ProdusserText { set; get; } = "Produsser";
        public string ActorText { set; get; } = "Actor";
        public string GenerText { set; get; } = "Gener";
        public DateTime Date { set; get; }=DateTime.Now;

        public ObservableCollection<string> _listProdusser = new ObservableCollection<string>();
        public CollectionView ListProdusser
        {
            get { return new CollectionView(_listProdusser); }
        }
        public ObservableCollection<string> _listActor = new ObservableCollection<string>();
        public CollectionView ListActor
        {
            get { return new CollectionView(_listActor); }
        }
        public ObservableCollection<string> _listGenre = new ObservableCollection<string>();
        public CollectionView ListGners
        {
            get { return new CollectionView(_listGenre); }
        }

        private string _allProdussers;
        public string AllProdussers
        {
            set { _allProdussers = value; OnPropertyChanged(nameof(AllProdussers)); }
            get { return _allProdussers; }
        }

        private string _allActors;
        public string AllActors
        {
            set { _allActors = value; OnPropertyChanged(nameof(AllActors)); }
            get { return _allActors; }
        }

        private string _allGeners;
        public string AllGeners
        {
            set { _allGeners = value; OnPropertyChanged(nameof(AllGeners)); }
            get { return _allGeners; }
        }

        public AddFilmViewModel()
        {
            _listProdusser.Add("fefdrfer");
            _listProdusser.Add("kukur");
            _listProdusser.Add("iyjyu");
            _listProdusser.Add("uky");
            _listProdusser.Add("kr");
            AllProdussers = "fee";
        }

        private DelegateCommand _addProdusser;

        public ICommand AddProdusser
        {
            get
            {
                if (_addProdusser==null)
                {
                    _addProdusser = new DelegateCommand(param => AddProdusserToLable(), null);
                }
                
                return _addProdusser;
            }
        }

        private void AddProdusserToLable()
        {
            AllProdussers += ProdusserText + " ";
        }



        private DelegateCommand _addActors;

        public ICommand AddActor
        {
            get
            {
                if (_addActors == null)
                {
                    _addActors = new DelegateCommand(param => AddActorToLable(), null);
                }

                return _addActors;
            }
        }

        private void AddActorToLable()
        {
            AllActors += ActorText + " ";
        }


        private DelegateCommand _addGener;

        public ICommand AddGener
        {
            get
            {
                if (_addGener == null)
                {
                    _addGener = new DelegateCommand(param => AddGenerToLable(), null);
                }

                return _addGener;
            }
        }

        private void AddGenerToLable()
        {
            AllGeners += GenerText + " ";
        }
    }
}
