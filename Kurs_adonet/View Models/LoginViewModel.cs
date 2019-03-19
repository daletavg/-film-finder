using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Kurs_adonet.FilmsFinder;
using OperationContracts;
using ILoginRegisterUser = Kurs_adonet.FilmsFinder.ILoginRegisterUser;

namespace Kurs_adonet
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public Action _openFilmFinder;
        public LoginViewModel(Action openRegistrate,Action openFilmFinder)
        {
            OpenRegistrate = new DelegateCommand(param=>openRegistrate(),null);
            _openFilmFinder = openFilmFinder;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            set { _errorMessage = value;OnPropertyChanged(nameof(ErrorMessage)); }
            get { return _errorMessage; }
        }

        ILoginRegisterUser loginRegister = new LoginRegisterUserClient();

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
            UResult result = (UResult)loginRegister.CheckUserOnDB(Login, password);
            if (result == UResult.Access)
            {
                _openFilmFinder();
            }
            else if(result == UResult.PasswordFailed)
            {
                ErrorMessage = "*Неправильный пароль";
            }
            else if (result == UResult.UserFailed)
            {
                ErrorMessage = "*Такого пользователя нет";
            }

        }

        bool CanExecuteLogin()
        {
            return true;

        }

   

        public ICommand OpenRegistrate { set; get; }

    }
}
