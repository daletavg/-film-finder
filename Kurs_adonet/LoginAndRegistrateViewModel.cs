using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kurs_adonet
{
    class LoginAndRegistrateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public string Login { set; get; } = "";
        public string Password { set; get; } = "";

        private DelegateCommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand(param=>LoginOnApp(param),param=>CanExecuteLogin());
                    
                }
                return _loginCommand;
            }
        }

        void LoginOnApp(object param)
        {
            var passwordBox = param as PasswordBox;
            if (passwordBox == null)
                return;
            var password = passwordBox.Password;
        }

        bool CanExecuteLogin()
        {
            return true;

        }
        
    }
}
