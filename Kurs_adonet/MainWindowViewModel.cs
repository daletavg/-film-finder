using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Kurs_adonet.Annotations;

namespace Kurs_adonet
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {

       

        private ObservableCollection<IUsingControl> ListUsersControl { set; get; }



        private object _openControl;
        public object OpenControl
        {
            set { _openControl = value;
              
                OnPropertyChanged(nameof(OpenControl)) ;}
            get { return _openControl; }
        }

      

        public void OpenRegistrateControl()
        {
            foreach (var o in ListUsersControl)
            {
                if (o.ThisControl==CurrentControl.RegistrateControl)
                {
                    OpenControl = o;
                }
            }
        }
        public void OpenLoginControl()
        {
            foreach (var o in ListUsersControl)
            {
                if (o.ThisControl == CurrentControl.LoginControl)
                {
                    OpenControl = o;
                }
            }
        }









        public void AddControls(List<IUsingControl> usersControl)
        {
            ListUsersControl = new ObservableCollection<IUsingControl>(usersControl);
            OpenControl = ListUsersControl[2];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
