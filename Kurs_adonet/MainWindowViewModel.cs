using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Kurs_adonet.Annotations;

namespace Kurs_adonet
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<object> ListUsersControl { set; get; }

        private object _openControl;
        public object OpenControl
        {
            set { _openControl = value; OnPropertyChanged(nameof(OpenControl));}
            get { return _openControl; }
        }

        public void AddControls(List<object> usersControl)
        {
            ListUsersControl = new ObservableCollection<object>(usersControl);
            OpenControl = ListUsersControl[1];
        }

        public void OpenRegistrateControl()
        {
            foreach (var o in ListUsersControl)
            {
                if (o is RegistrControl)
                {
                    OpenControl = o;
                }
            }
        }
        public void OpenLoginControl()
        {
            foreach (var o in ListUsersControl)
            {
                if (o is LoginControl)
                {
                    OpenControl = o;
                }
            }
        }














        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
