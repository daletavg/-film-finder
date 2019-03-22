﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs_adonet
{
    public class MessageViewModel:INotifyPropertyChanged
    {
        string _nickName;
        string _message;

        public event PropertyChangedEventHandler PropertyChanged;

        public string NickName
        {
            set{ _nickName = value; OnPropertyChanged(nameof(NickName)); }
            get { return _nickName; } }
        public string Message
        {
            set { _message = value;OnPropertyChanged(nameof(Message)); }
            get { return _message; }
        }

        
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
