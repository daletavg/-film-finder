using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using OperationContracts;


//using Kurs_adonet.LoginAndRegistrate;


namespace Kurs_adonet
{
    class AddFilmViewModel : INotifyPropertyChanged
    {
        OperationContracts.IAddLoadFilm _addNewFilm;

        public AddFilmViewModel(OperationContracts.IAddLoadFilm addNewFilm)
        {
            _addNewFilm = addNewFilm;
            var tmp = _addNewFilm.GetSpecific();
            _listProdusser = new ObservableCollection<string>(tmp.Produsers);
            _listActor = new ObservableCollection<string>(tmp.Actors);
            _listGenre = new ObservableCollection<string>(tmp.Geners);

        }

        private int _hours = 0;
        public int Hours
        {
            set { _hours = value;OnPropertyChanged(nameof(Hours));}
            get { return _hours; }
        }
        private int _minutes = 0;
        public int Minutes
        {
            set { _minutes = value;OnPropertyChanged(nameof(Minutes));}
            get { return _minutes; }
        }

        private int _seconds = 0;
        public int Seconds
        {
            set { _seconds = value;OnPropertyChanged(nameof(Seconds));}
            get { return _seconds; }
        }
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
            _produssersAtFilm.Add(ProdusserText);
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
            _actorsAtFilm.Add(ActorText);
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
            _genersAtFilm.Add(GenerText);
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

        private string _errorMessage;

        public string ErrorMessage
        {
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage));}
            get { return _errorMessage; }
        }

        

        public bool AddNewFilm()
        {
            if (PathToimage ==null || PathToimage=="")
            {
                ErrorMessage = "*Не указано изображение";
                return false;
            }
            else if(FilmName==""||AllActors==""||AllGeners==""||AllProdussers=="" || AllActors == null || AllGeners == null || AllProdussers == null)
            {
                ErrorMessage = "*Не заполнены все поля";
                return false;
            }
            
            
            FilmContent content = new FilmContent();
            content.Image = File.ReadAllBytes(PathToimage);
            content.ImageName = PathToimage.Remove(0, PathToimage.LastIndexOf("\\")+1);
            content.Actors = _actorsAtFilm.ToArray();
            content.Description = DescriptionFilm;
            content.Geners = _genersAtFilm.ToArray();
            content.Name = FilmName;
            content.Produsers = _produssersAtFilm.ToArray();
            content.ReleaseDate = Date.ToString();
            content.FilmTime = _hours + ":" + _minutes + ":" + _seconds;
            if (((UResult) _addNewFilm.AddNewFilm(content)) == UResult.FilmFailed)
            {
                ErrorMessage = "*Данный фильм уже существует";
                return false;
            }
            else
            {
                return true;
            }
            
            
        }

        public void CloseWindow()
        {
            AllProdussers = "";
            AllActors = "";
            AllGeners = "";
            FilmName = "";
            PathToimage = "";
            PosterFilm = null;
            _produssersAtFilm = new List<string>();
            _actorsAtFilm = new List<string>();
            _genersAtFilm = new List<string>();
        }
        


    }
}
