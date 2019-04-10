using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using OperationContracts;

namespace Kurs_adonet
{
    public class FilmCardViewModel : INotifyPropertyChanged
    {
        public OperationContracts.IAddLoadFilm AddLoadFilm;
        public ObservableCollection<string> ListProdusser { set; get; }

        public ObservableCollection<string> ListActor { set; get; }


        public ObservableCollection<string> ListGenre { set; get; }

        private ISetRaiting _setRaiting;

        public FilmCardViewModel(ISetRaiting setRaiting)
        {
            _setRaiting = setRaiting;
        }

        public string _middleRaiting = "0";

        public string MiddleRaiting
        {
            set { _middleRaiting = value; OnPropertyChanged(nameof(MiddleRaiting)); }
            get { return _middleRaiting; }
        }

        public string Actors
        {
            get
            {
                string tmpActors = "";
                foreach (var i in ListActor)
                {
                    tmpActors += i + "/";
                }

                return tmpActors;
            }
        }
        public string Geners
        {
            get
            {
                string tmpGeners = "";
                foreach (var i in ListGenre)
                {
                    tmpGeners += i + "/";
                }

                return tmpGeners;
            }
        }
        public string Produssers
        {
            get
            {
                string tmpProdussers = "";
                foreach (var i in ListProdusser)
                {
                    tmpProdussers += i + "/";
                }

                return tmpProdussers;
            }
        }

        private int _raiting;

        public void InitRaiting(int r)
        {
            _raiting = r;
        }
        public int Raiting
        {
            set
            {
                _raiting = value; _setRaiting.SetRaiting(value, FilmName);
                MiddleRaiting = _setRaiting.GetRaitingOfFilm(FilmName).ToString();
            }
            get { return _raiting; }
        }

        public string _filmName;
        public string FilmName
        {
            set { _filmName = value; MiddleRaiting = _setRaiting.GetRaitingOfFilm(FilmName).ToString(); }
            get { return _filmName; }
        }
        public string DescriptionFilm { set; get; }
        public string _date;

        public string Date
        {
            set { _date = DateTime.Parse(value).ToShortDateString(); OnPropertyChanged(nameof(Date)); }
            get { return _date; }
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
        private bool _isFavorit;
        public bool IsFavorit
        {
            get { return _isFavorit; }
            set
            {
                _isFavorit = value;
                AddLoadFilm.SetFavorit(FilmName, value);
                OnPropertyChanged(nameof(IsFavorit));
            }
        }

        private string _filmTime;

        public string FilmTime
        {
            set
            {
                _filmTime = value;
                OnPropertyChanged(nameof(FilmTime));
            }
            get { return _filmTime; }
        }

        private string _message="";

        public string Message
        {
            set
            {
                _message = value; OnPropertyChanged(nameof(Message));

            }
            get { return _message; }
        }

        private DelegateCommand _send;

        public ICommand SendMessage
        {
            get
            {
                if (_send == null)
                {
                    _send = new DelegateCommand(param=> SendMessageToServer(),null);
                }

                return _send;
            }
        }

        void SendMessageToServer()
        {
            if (Message == "")
            {
                return;
            }
            ((IComments)AddLoadFilm).AddComment(FilmName,Message);
            LookComment();
        }

        private ObservableCollection<MessageViewModel> _comments;
        public ObservableCollection<MessageViewModel> Comments
        {
            set { _comments = value;OnPropertyChanged(nameof(Comments));}
            get { return _comments; }
        }

        public void LookComment()
        {
            int count = ((IComments) AddLoadFilm).GetCountComments(FilmName);
            if (count == 0)
            {
                _comments = new ObservableCollection<MessageViewModel>();
                return; 
            }
            ObservableCollection < MessageViewModel > newComments = new ObservableCollection<MessageViewModel>();
            for (int i = 0; i < count; i++)
            {
                var comment = ((IComments) AddLoadFilm).GetComments(i, FilmName);
                MessageViewModel msg = new MessageViewModel(){Message = comment.Message,NickName = comment.NickName.Login,UserImage = new ImageConverter().ByteToBitmapImage(comment.NickName.UserImage) };
                newComments.Add(msg);
            }

            Comments = newComments;
        }
    }
}
