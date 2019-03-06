using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kurs_adonet.FilmsFinder;
using OperationContracts;
using IAddLoadFilm = Kurs_adonet.FilmsFinder.IAddLoadFilm;

//using Kurs_adonet.LoginAndRegistrate;


namespace Kurs_adonet
{
    class AddFilmViewModel : INotifyPropertyChanged
    {
        IAddLoadFilm addNewFilm = new AddLoadFilmClient();
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

        public string FilmName { set; get; } = "Name";
        public string ProdusserText { set; get; } = "Produsser";
        public string ActorText { set; get; } = "Actor";
        public string GenerText { set; get; } = "Gener";
        
        public DateTime Date { set; get; }=DateTime.Now;

        public ObservableCollection<string> _listProdusser { set; get; }
        public CollectionView ListProdusser
        {
            get { return new CollectionView(_listProdusser); }
        }
        public ObservableCollection<string> _listActor { set; get; }
        public CollectionView ListActor
        {
            get { return new CollectionView(_listActor); }
        }
        public ObservableCollection<string> _listGenre { set; get; }
        public CollectionView ListGners
        {
            get { return new CollectionView(_listGenre); }
        }

        public string DescriptionFilm { set; get; }

        private List<string> _produssersAtFilm=new List<string>();

        private string _allProdussers;
        public string AllProdussers
        {
            set
            {
                _allProdussers = value;
                _produssersAtFilm.Add(value);
                OnPropertyChanged(nameof(AllProdussers));
            }
            get { return _allProdussers; }
        }
        private List<string> _actorsAtFilm = new List<string>();
        private string _allActors;
        public string AllActors
        {
            set
            {
                _allActors = value;
                _actorsAtFilm.Add(value);
                OnPropertyChanged(nameof(AllActors));
            }
            get { return _allActors; }
        }
        private List<string> _genersAtFilm = new List<string>();
        private string _allGeners;
        public string AllGeners
        {
            set
            {
                _allGeners = value;
                _genersAtFilm.Add(value);
                OnPropertyChanged(nameof(AllGeners));
            }
            get { return _allGeners; }
        }

        public string _pathToImage;
        public string PathToimage
        {
            set
            {
                _pathToImage = value;
                OnPropertyChanged(nameof(PathToimage));
            }
            get { return _pathToImage; }
        }

        public AddFilmViewModel()
        {
           var tmp = addNewFilm.GetSpecific();
           _listProdusser = new ObservableCollection<string>(tmp.Produsers);
           _listActor = new ObservableCollection<string>(tmp.Actors);
           _listGenre = new ObservableCollection<string>(tmp.Geners);

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

        private DelegateCommand _newFilm;

        public ICommand NewFilm
        {
            get
            {
                if (_newFilm == null)
                {
                    _newFilm = new DelegateCommand(param => AddNewFilm(), null);
                }

                return _newFilm;
            }
        }

        private void AddNewFilm()
        {
            
            
            
            FilmContent content = new FilmContent();
            content.Image = GetByteImage();
            content.Actors = _actorsAtFilm.ToArray();
            content.Description = DescriptionFilm;
            content.Geners = _genersAtFilm.ToArray();
            content.Name = FilmName;
            content.Produsers = _produssersAtFilm.ToArray();
            content.ReleaseDate = Date.ToString();
            addNewFilm.AddNewFilm(content);
        }

        string GetByteImage()
        {
            BitmapConverter convert = new BitmapConverter();
            if (PathToimage == null) { }
            else if (PathToimage.Contains(".png"))
            {
                var tmp =convert.ImageSourceToBytes(new PngBitmapEncoder(), PosterFilm);
                var tmp2 = System.Text.Encoding.UTF8.GetString(tmp);
                var m = convert.LoadImage(Encoding.UTF8.GetBytes(tmp2));
                return tmp2;
            }
            else if (PathToimage.Contains(".jpg"))
            {
                var tmp = convert.ImageSourceToBytes(new JpegBitmapEncoder(), PosterFilm);
                var tmp2 = System.Text.Encoding.UTF8.GetString(tmp);
                return tmp2;
            }

            return null;
        }


    }
}
