﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using OperationContracts;

namespace Kurs_adonet
{
    public class FilmCardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> ListProdusser { set; get; }

        public ObservableCollection<string> ListActor { set; get; }


        public ObservableCollection<string> ListGenre { set; get; }

        private ISetRaiting _setRaiting;

        public FilmCardViewModel(ISetRaiting setRaiting)
        {
            _setRaiting = setRaiting;
           
        }

        public string _middleRaiting="0";

        public string MiddleRaiting
        {
            set { _middleRaiting = value; OnPropertyChanged(nameof(MiddleRaiting)); }
            get { return _middleRaiting; }
        }

        public string Actors
        {
            get
            {
                string tmpActors="";
                foreach (var i in ListActor)
                {
                    tmpActors += i + ",";
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
                    tmpGeners += i + ",";
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
                    tmpProdussers += i + ",";
                }

                return tmpProdussers;
            }
        }

        private int _raiting;
        public int Raiting
        {
            set { _raiting = value;_setRaiting.SetRaiting(value,FilmName);
                MiddleRaiting = _setRaiting.GetRaitingOfFilm(FilmName).ToString();
            }
            get { return _raiting; }
        }

        public string _filmName;
        public string FilmName
        {
            set { _filmName = value; MiddleRaiting = _setRaiting.GetRaitingOfFilm(FilmName).ToString(); }
            get { return _filmName; } }
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
